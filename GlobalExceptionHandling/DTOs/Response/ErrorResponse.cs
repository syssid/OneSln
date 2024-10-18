namespace GlobalExceptionHandling.DTOs.Response
{
    public record ErrorResponse
    {
        public required bool Success { get; set; }
        public  string? Message { get; set; }
    }
}