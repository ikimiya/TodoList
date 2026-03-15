namespace TodoList.Models
{
    public class TaskTags
    {
        public int TaskId { get; set; }
        public Tasks Task { get; set; } = null!;
        public int TagId { get; set; }
        public Tags Tag { get; set; } = null!;

    }
}
