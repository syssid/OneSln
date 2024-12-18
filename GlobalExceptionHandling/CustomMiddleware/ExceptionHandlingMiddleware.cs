﻿using GlobalExceptionHandling.DTOs.Response;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GlobalExceptionHandling.CustomMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var actionVerb = httpContext.Request.Method;
                var apiUrl = httpContext.Request.Scheme + "://" + httpContext.Request.Host + httpContext.Request.Path;

                if (actionVerb == HttpMethods.Post || actionVerb == HttpMethods.Put)
                {
                    httpContext.Request.EnableBuffering();

                    using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
                    {
                        var bodyContent = await reader.ReadToEndAsync();

                        httpContext.Request.Body.Position = 0;
                        Console.WriteLine(bodyContent);
                    }
                }
                    await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse
            {
                Success = false
            };
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal server error!";
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }

    }
}
