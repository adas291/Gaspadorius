using System.ComponentModel.DataAnnotations;
using Gaspadorius.Auth;

namespace Gaspadorius.Models;

public class UserDto : IUserHasPhone, IUserOwnedResource
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash{ get; set; } = string.Empty;
}



public class CreateUserDto
{

    public CreateUserDto(){ }
    public int Id{ get; set; }
    [Required] public string Username { get; set; }

    [Required, EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public string? Phone { get; set; }

    public string? Name { get; set; }
    public string? Surname{ get; set; }
}

public record SuccessfulRegisterDto(int Id, string Username, string Email);
public record LoginUserDto(string Username, string Password);