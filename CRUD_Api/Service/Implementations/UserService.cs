using System.Text.RegularExpressions;
using AutoMapper;
using CRUD_Api.DTO;
using CRUD_Api.Models;
using CRUD_Api.Service.Interfaces;

namespace CRUD_Api.Service.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private List<User> _users = new List<User>();

    public UserService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public Task<List<User>> GetAll()
    {
        UpdateIndexes();
        return Task.Run(() => _users);
    }

    /// <summary>
    /// В задании не сказано, но для уникальности пользователей (как в реальной жизни)
    /// уникальными ключами будут являться - id и email
    /// </summary>
    /// <param name="userAdd"></param>
    /// <returns></returns>
    public Task<List<User>> AddUser(UserAddDto userAdd)
    {
        // проверка, что почты еще не существует в нашей базе
        var userExist = _users.FirstOrDefault(x => x.Email == userAdd.Email);
        if (userExist != null)
        {
            // пользователь с данной почтой уже существует
            return Task.Run(() => new List<User>());
        }
        // мапим нашу Dto к объекту класса User
        User user = _mapper.Map<User>(userAdd);
        // тк это новый пользователь, то мы должны увеличить его индекс
        user.Id = _users.Count + 1;
        if (UserIsValid(user))
        {
            _users.Add(user);
            UpdateIndexes();
            return Task.Run(() => _users);

        }

        // возвращаем пустой список, если пользователь был невалиден
        // следующий уровень (уровень Представления) обработает это
        return Task.Run(() => new List<User>());
    }

    /// <summary>
    /// В задании не сказано, но для уникальности пользователей (как в реальной жизни)
    /// уникальными ключами будут являться - id и email
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userUpdate"></param>
    /// <returns></returns>
    public Task<List<User>> UpdateUser(int id, UserUpdateDto userUpdate)
    {
        // проверка, что почта принадлежит уже другому человеку
        var userExist = _users.FirstOrDefault(x => x.Email == userUpdate.Email && id != x.Id);
        if (userExist != null)
        {
            // пользователь с данной почтой уже существует
            return Task.Run(() => new List<User>());
        }
        
        for (int i = 0; i < _users.Count; ++i)
        {
            if (id == _users[i].Id)
            {
                User user = _mapper.Map<User>(userUpdate);
                user.Id = id;
                if (UserIsValid(user))
                {
                    _users[i] = user;
                    return Task.Run(() => _users);

                }
                // пользователь не валиден
                return Task.Run(() => new List<User>());
            }
        }

        // Не существует пользователя с данным индексом
        return Task.Run(() => new List<User>());

    }

    public Task<List<User>> DeleteUser(int id)
    {
        var deleteUser = _users.FirstOrDefault(u => u.Id == id);
        // пользователя нет в нашей базе
        if (deleteUser == null)
        {
            return Task.Run(() => new List<User>());
        }

        _users.Remove(deleteUser);
        UpdateIndexes();
        return Task.Run(() => _users);
    }
    
    /// <summary>
    /// Обновление индексов, тк мы не используем базу данных (не написано в задании)
    /// приходится как-то сохранять уникальность пользователей
    /// </summary>
    private void UpdateIndexes()
    {
        for (int i = 0; i < _users.Count; ++i)
        {
            _users[i].Id = i + 1;
        }
    }

    // регулярка на проверку email пользователя
    private Regex emailRegex = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
    /// <summary>
    /// Проверка на валидность пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private bool UserIsValid(User user)
    {
        return emailRegex.IsMatch(user.Email)
               && user.Age >= 1
               && user.Age <= 100
               && user.FirstName.Length <= 30
               && user.LastName.Length <= 30;
    }
}