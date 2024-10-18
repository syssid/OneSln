using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalExceptionHandling.Models
{
    public class Customer
    {
        [Key]
        public Guid ID { get; set; }
        public required string  Name { get; set; }
        public required string  Email { get; set; }
        public required string  Phone { get; set; }
        public required DateTime  CreatedOn { get; set; }
    }
}
