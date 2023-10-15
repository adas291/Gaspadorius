
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Gaspadorius.Auth.Dtos;

// public record RegisterUserDto([Required] string UserName, [Required, EmailAddress] string Email, [Required] string Password);
// public record LoginUserDto([Required] string Username, [Required] string Password);
// public record ValidateUserDto(int Id, [Required] string Username, [Required] string PasswordHash);
// public class UserDto
// {
//     public UserDto() { }

//     public int Id { get; set; }
//     public string Name { get; set; }
//     public string Surname { get; set; }
//     public string Email { get; set; }
//     public string Phone { get; set; }
//     public string Username { get; set; }
//     public string PasswordHash { get; set; }
//     public override string ToString()
//     {
//         return $"{Id}, {Username}, {PasswordHash}";
//     }
// }