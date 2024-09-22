using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models;

public class UserService(AppDbContext context): IUserService
{
    private readonly AppDbContext _context = context;

    public async Task<User> RegisterAsync(string username, string email, string password)
    {
        if(await _context.Users.AnyAsync(user => user.Email == email))
        {
            throw new Exception("User with this email already exists");
        }
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Username = username,
            Password = password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }
    public async Task<User> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        return user ?? throw new Exception("User not found!!");
    }
}