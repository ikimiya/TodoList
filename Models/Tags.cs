using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoList.Models
{
    public class Tags
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        [ValidateNever]
        public ICollection<TaskTags> TaskTags { get; set; } = new List<TaskTags>();



    }
}
