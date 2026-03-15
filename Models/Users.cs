using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
