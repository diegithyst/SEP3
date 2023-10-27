using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        User? existing = await _userDao.GetByUsernameAsync(userToCreate.username);
        if (existing != null)
        {
            throw new Exception("Username already taken!");
        }

        ValidateData(userToCreate);

        User toCreate = new User
        {
            username = userToCreate.username,
            password = userToCreate.password
        };
        User created = await _userDao.CreateAsync(toCreate);
        return created;
    }

    private static void ValidateData(UserCreationDTO userToCreate)
    {
        string username = userToCreate.username;

        if (username.Length < 3)
        {
            throw new Exception("Username must be at least 3 characters long!");
        }

        if (username.Length > 15)
        {
            throw new Exception("Username must be less than 16 characters long!");
        }
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchUserParameters)
    {
        return _userDao.GetAsync(searchUserParameters);
    }
}