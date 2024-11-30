using Microsoft.AspNetCore.Identity;


namespace Domain.Model
{
    public class User : IdentityUser
    {
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
