using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoList.Models
{
    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }


    public class Tasks
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; } = Status.Pending;

        [Required]
        public Priority Priority { get; set; } = Priority.Low;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foeign key to Users
        public int UserId { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; } = null!;

        public int CategoryId { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Categories Category { get; set; } = null!;

        [JsonIgnore]
        [ValidateNever]
        public ICollection<TaskTags> TaskTags { get; set; } = new List<TaskTags>();


    }
}
