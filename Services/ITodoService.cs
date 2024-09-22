namespace TodoApp.Models;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllAsync(Guid userId);
    Task<Todo> GetByIdAsync(Guid id, Guid userId);
    Task<Todo> CreateAsync(Todo todo, Guid userId);
    Task<Todo> UpdateAsync(Todo todo, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
}