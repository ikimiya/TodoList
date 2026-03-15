using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Tags
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskTags> TaskTags { get; set; } = new List<TaskTags>();



    }
}
