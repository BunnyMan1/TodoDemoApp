using Todo.Models;
using Todo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Todo.DTOs;

namespace Todo.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoRepository _todo;

    public TodoController(ILogger<TodoController> logger,
    ITodoRepository todo)
    {
        _logger = logger;
        _todo = todo;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] TodoCreateDTO Data)
    {
        var toCreateItem = new TodoItem
        {
            Title = Data.Title.Trim(),
            UserId = Data.UserId,
        };

        // Insert into DB
        var createdItem = await _todo.Create(toCreateItem);

        // Return the created Todo
        return StatusCode(201, createdItem);
    }

    [HttpPut("{todo_id}")]
    public async Task<ActionResult> UpdateTodo([FromRoute] int todo_id,
    [FromBody] TodoUpdateDTO Data)
    {
        var existingItem = await _todo.GetById(todo_id);

        if (existingItem is null)
            return NotFound();

        var toUpdateItem = existingItem with
        {
            Title = Data.Title is null ? existingItem.Title : Data.Title.Trim(),
            IsComplete = !Data.IsComplete.HasValue ? existingItem.IsComplete : Data.IsComplete.Value,
        };

        await _todo.Update(toUpdateItem);

        return NoContent();
    }

    [HttpDelete("{todo_id}")]
    public async Task<ActionResult> DeleteTodo([FromRoute] int todo_id)
    {
        var existingItem = await _todo.GetById(todo_id);

        if (existingItem is null)
            return NotFound();

        await _todo.Delete(todo_id);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAllTodos()
    {
        var allTodo = await _todo.GetAll();
        return Ok(allTodo);
    }
}
