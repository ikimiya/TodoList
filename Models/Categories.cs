using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoList.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; } = null!;


    }
}
