using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace TodoList.Models
{
    public class TaskTags
    {
        public int TaskId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Tasks Task { get; set; } = null!;
        public int TagId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Tags Tag { get; set; } = null!;

    }
}
