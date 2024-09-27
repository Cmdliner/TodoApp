using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoContoller(ITodoService service) : ControllerBase
{
    private readonly ITodoService _todoService = service;

    [HttpPost("create")]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoModel model)
    {
        try
        {
            var todo = new Todo { Title = model.Title, Body = model.Body };
            var newTodo = await _todoService.CreateAsync(todo, new Guid("fff3c174-c550-47ab-b42b-57e46552060b"));
            return CreatedAtAction(nameof(CreateTodo), new { message = "Todo created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTodos()
    {
        try
        {
            var todos = await _todoService.GetAllAsync(new Guid("fff3c174-c550-47ab-b42b-57e46552060b"));
            return Ok(todos);

        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}

public class CreateTodoModel
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}