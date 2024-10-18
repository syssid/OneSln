using GlobalExceptionHandling.Data;
using GlobalExceptionHandling.DTOs.Request;
using GlobalExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }
        [HttpGet("all-customer")] 
        public IActionResult GetAllCustomerAsync()
        {
           var customers =  _context.Customer.ToList();

            return Ok(customers);
        }
        [HttpGet("customer-by-id")]
        public IActionResult GetCustomerByID(Guid ID)
        {
           var customer = _context.Customer.FirstOrDefault(x => x.ID == ID);
            if (customer == null) return NotFound();
            else
            return Ok(customer);
        }
        [HttpPost("add-new-customer")]
        public IActionResult AddNewCustomer(CustomerRequestDTO customerDTO)
        {
            var customerModal = new Customer
            {
                ID = Guid.NewGuid(),
                Name = customerDTO.Name,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email,
                CreatedOn = DateTime.Now
            };
            _context.Customer.Add(customerModal);
            _context.SaveChanges();
            return Ok(customerModal);
        }

        [HttpPut("edit-customer")]
        public IActionResult EditCustomer(Guid ID, CustomerRequestDTO customerDTO)
        {
            var customer = _context.Customer.FirstOrDefault(x => x.ID == ID);
            if (customer == null) return NotFound();

            customer.Name = customerDTO.Name;
            customer.Phone = customerDTO.Phone;
            customer.Email = customerDTO.Email;
            customer.CreatedOn = customerDTO.CreatedOn;

            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpDelete("delete-customer")]
        public async Task<IActionResult> DeleteCustomer(Guid ID)
        {
            var custmer =  await _context.Customer.FindAsync(ID);
            if (custmer != null)
            {
                _context.Customer.Remove(custmer);
                _context.SaveChanges();
            }
            return NoContent();
        }
    }
}
