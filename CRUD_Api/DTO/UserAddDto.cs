using System.ComponentModel.DataAnnotations;

namespace CRUD_Api.DTO;

public class UserAddDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}