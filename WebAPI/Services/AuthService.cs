using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;
using FileData;
using FileData.DAOs;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User>
    {
        new User
        {
            password = "test1",
            username = "test1",
            securityLevel = 4
        },
        new User
        {
            password = "password",
            username = "jknr",
            securityLevel = 2
        }
    };
    private IUserDao _userDao = new UserFileDao(new FileContext());

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await _userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.password.Equals(password))
        {
            throw new Exception("password mismatch");
        }

        return existingUser;
    }
    

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.username))
        {
            throw new ValidationException("username cannot be null");
        }

        if (string.IsNullOrEmpty(user.password))
        {
            throw new ValidationException("password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        return Task.CompletedTask;
    }
}