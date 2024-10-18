namespace GlobalExceptionHandling.DTOs.Request
{
    public class CustomerRequestDTO
    {
        public Guid ID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
