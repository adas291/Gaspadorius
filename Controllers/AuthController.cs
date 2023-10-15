using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gaspadorius.Auth;
using Gaspadorius.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Gaspadorius.Models;
using Gaspadorius.Auth.Dtos;
namespace Gaspadorius.Controllers;


[ApiController, Route("api")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserManager _userManager;
    private readonly JwtTokenService _jwtTokenService;


    public AuthController(UserManager userManager, JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto registerUserDto)
    {
        System.Console.WriteLine("tring register");
        UserDto? user = await UserManager.FindUser(registerUserDto.Username);

        if (user != null)
            return BadRequest("User already registered");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password, 10);

        var userId = (int?)await UserManager.CreateUser(registerUserDto, passwordHash);

        if (userId == null)
            return BadRequest("Could not create new user");

        var addedToRole = await UserManager.AddToRoleAsync((long)userId, (int)RoleEnum.RegisteredUser) >= 1;

        if (addedToRole)
            return CreatedAtAction(nameof(Register), new SuccessfulRegisterDto((int)userId, registerUserDto.Username, registerUserDto.Email));
            // return Ok();
        else
            return BadRequest("Problem occured while registering new user");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        UserDto? user = await UserManager.FindUser(loginUserDto.Username);

        if (user == null)
        {
            return NotFound("user is null");
        }

        var roles = await UserManager.FindUserRoles(user.UserId);

        if (BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
        {
            var accessToken = _jwtTokenService.CreateAccessToken(user, roles);

            // return Ok(new SuccessfulLoginDto(accessToken));
            return Ok(accessToken);
        }

        return BadRequest("Passwords dont match");

        // if (userId == null)
        //     return BadRequest("Could not create new user");

        // var addToRole = await UserManager.AddToRoleAsync((long)userId, Roles.RegisteredUser);

        // if (addToRole == 1)
        //     return Ok("registered " + userId);

    }
}