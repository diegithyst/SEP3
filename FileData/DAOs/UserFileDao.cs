using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        foreach (var u in _context.Users)
        {
            if (u.username.Equals(user.username))
            {
                throw new Exception("Username already taken!");
            }
        }

        int userId = 1;
        if (_context.Users.Any())
        {
            userId = _context.Users.Max(u => u.id);
            userId++;
        }

        user.id = userId;
        
        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.id == id);
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchUserParameters)
    {
        IEnumerable<User> users = _context.Users.AsEnumerable();
        if (searchUserParameters.UsernameContains != null)
        {
            users = _context.Users.Where(u =>
                u.username.Contains(searchUserParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
}