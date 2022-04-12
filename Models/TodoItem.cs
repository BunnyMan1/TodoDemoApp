namespace Todo.Models;

public record TodoItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; } = false;
}