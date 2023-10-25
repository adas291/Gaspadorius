using System.Runtime.InteropServices.ObjectiveC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Gaspadorius.Models;
using Gaspadorius.Repos;
using Gaspadorius.Auth;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Gaspadorius.Controllers;



[Route("api/[controller]/")]
[Authorize]
public class UserController : ControllerBase
{

    readonly IAuthorizationService _authorizationService;
    public UserController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<List<UserDto>> GetAllUsers()
    {
        var res = await UserRepo.GetAllUsersAsync();
        return res;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await UserRepo.GetUserAsync(id);
        System.Console.WriteLine(id);
        
        if(user is null)
        {
            return NotFound();
        }

        var authoResult = await _authorizationService.AuthorizeAsync(User, user, PolicyNames.ResourceOwner);

        if(authoResult.Succeeded)
        {
            return Ok(user);
        }
        return Forbid();
    }


    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(UserDto user)
    {
        var successful = await Repos.UserRepo.CreateAsync(user) == 1;

        if (successful)
        {
            return Ok("Created successfully");
        }
        return BadRequest("New user was not created due error");
    }


    [HttpPatch("Update")]
    public async Task<IActionResult> Update(UserDto user)
    {
        var successful = await Repos.UserRepo.Update(user) == 1;
        if (successful)
        {
            return Ok("Updated successfully");
        }

        return BadRequest("New user was not created due error");
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var successful= await Repos.UserRepo.Delete(id) == 1;
        if (successful)
        {
            return Ok("Deleted successfully");
        }
        return BadRequest("New user was not created due error");
    }

}
