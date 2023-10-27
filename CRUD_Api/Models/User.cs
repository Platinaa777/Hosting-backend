using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace CRUD_Api.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}