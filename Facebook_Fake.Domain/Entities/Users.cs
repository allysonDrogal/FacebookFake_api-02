
namespace Facebook_Fake.Domain.Entities
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
        public string Password { get; set; } = string.Empty;
        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


    }
}
