using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
    }
}
