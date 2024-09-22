using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models;

public class TodoService(AppDbContext context)
{
    private readonly AppDbContext _context = context;
    public async Task<IEnumerable<Todo>> GetAllAsync(Guid userId)
    {
        var todos = await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        return todos;
    }

    public async Task<Todo> GetByIdAsync(Guid id, Guid userId)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        return todo ?? throw new Exception("Todo not found");
    }

    public async Task<Todo> CreateAsync(Todo todo, Guid userId)
    {
        todo.Id = Guid.NewGuid();
        todo.UserId = userId;
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> UpdateAsync(Todo todo, Guid userId)
    {
        var existingTodo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id && t.UserId == userId) ?? throw new Exception("Cannot update Todo!");

        existingTodo.Title = todo.Title;
        existingTodo.Body = todo.Body;
        existingTodo.UpdatedAt = DateTime.UtcNow;
        existingTodo.IsComplete = todo.IsComplete;

        await _context.SaveChangesAsync();

        return existingTodo;
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId) ?? throw new Exception("Cannot delete todo!");
        await _context.SaveChangesAsync();
    }
}