using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Categories
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; }
        public Users User { get; set; } = null!;


    }
}
