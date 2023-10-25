
using System.Net.Http.Headers;
using Gaspadorius.Repos;
using Microsoft.AspNetCore.Mvc;
using Gaspadorius.Models;
using Microsoft.AspNetCore.Authorization;
using Gaspadorius.Auth;
using Gaspadorius.Models.Dto;

namespace Gaspadorius.Controllers;

[Route("api/[controller]")]
[Authorize]
public class PropertyController : Controller
{
    readonly IAuthorizationService _authorizationService;
    public PropertyController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<List<Property>> GetAllProperties()
    {
        return await PropertyRepo.GetAllProperties();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<PropertyDto>> GetProperty(int id)
    {


        // foreach (var claim in User.Claims)
        // {
        //     Console.WriteLine($"{claim.Type}: {claim.Value}");
        // }


        var result = await Repos.PropertyRepo.GetProperty(id);

        if (result == null)
        {
            System.Console.WriteLine("not found fr");
            return NotFound();
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, result, PolicyNames.ResourceOwner);


        if (!authorizationResult.Succeeded)
        {
            System.Console.WriteLine("Not authorized");
            return Forbid();
        }

        return result.AsDto();
    }

    [HttpPost("Create")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Create(Property leaseObject)
    {
        Repos.PropertyRepo.CreateProperty(leaseObject);
        return Ok("Record created successfully");
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Delete(int Id)
    {
        try
        {
            if (Repos.PropertyRepo.Delete(Id) == 1)
                return Ok($"Lease object {Id} removed successfully");
            else return BadRequest("Not removed");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error occured while processing delete request: {ex.Message}");
        }

        return BadRequest($"Object was not deleted");
    }

    [HttpPatch("Update")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Update([FromBody]Property leaseObject)
    {
        System.Console.WriteLine(leaseObject.Id);
        System.Console.WriteLine(leaseObject.Title);
        if (PropertyRepo.Update(leaseObject) == 1)
        {
            return Ok("Record created successfully");
        }
        return BadRequest("Row was not updated");
    }

}