using CRUD_Api.DTO;
using CRUD_Api.Models;

namespace CRUD_Api.Service.Interfaces;

public interface IUserService
{ 
    Task<List<User>> GetAll();
    Task<List<User>> AddUser(UserAddDto user);
    Task<List<User>> UpdateUser(int id, UserUpdateDto user);
    Task<List<User>> DeleteUser(int id);
}