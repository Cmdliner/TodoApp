namespace TodoApp.Services;

public interface IUserService
{
    Task<User> RegisterAsync(string username, string email, string password);
    Task<User> AuthenticateAsync(string email, string password);
    Task<User> GetByIdAsync(Guid id);
}