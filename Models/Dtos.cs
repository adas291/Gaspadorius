using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Models.Dto;


public record PropertyDto(
    int Id,
    string Title,
    string Address,
    float Size,
    int FkOwner,
    int FkCity
);

public record CreatePropertyDto(
    string Title,
    string Address,
    float Size,
    int FkOwner,
    int FkCity
);

public record UpdatePropertyDto(
    string Title,
    string Address,
    float Size,
    int FkOwner,
    int FkCity
);



// public record UserDto(
//     int Id,
//     string? Username,
//     string Surname,
//     string? Email,
//     string Phone,
//     string? PasswordHash
// );

// public record CreateUserDto(
//     [Required] string Username,
//     [Required, EmailAddress] string Email,
//     [Required] string Password,
//     [Phone] string Phone
// );

// public record SuccessfulLoginDto(string AccessToken);