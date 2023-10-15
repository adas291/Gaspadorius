using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Gaspadorius.Models;
using Gaspadorius.Repos;
using Microsoft.AspNetCore.Authorization;
using Gaspadorius.Auth;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Gaspadorius;

[Route("api/Agreement")]
[Authorize]
public class AgreementController : Controller
{

    readonly IAuthorizationService _authorizationService;
    public AgreementController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost("Create")]
    [Authorize(Roles.Admin)]
    public string Create(AgreemenDto model)
    {
        try
        {
            var successful = AgreementRepo.Create(model) == 1;
            if (successful == false) throw new Exception("error while inserting");
        }
        catch (Exception ex)
        {
            return $"Error occured: {ex.Message}";
        }
        return "created successfully";

    }

    [HttpGet("/City/{city}/Property/{propertyId}/Agreement/{agreementId}")]
    [Authorize]
    public async Task<ActionResult<AgreemenDto>> Get(string city, int propertyId, int agreementId)
    {

        System.Console.WriteLine("in controller");

        int cityId = Models.CityMapper.GetCityId(city);
        var agreement = await AgreementRepo.GetFull(cityId, propertyId, agreementId);
        if (agreement == null)
        {
            return NotFound();
        }

        var authResult = await _authorizationService.AuthorizeAsync(User, agreement, PolicyNames.ResourceOwner);

        if (authResult.Succeeded)
        {
            return Ok(agreement);
        }

        return Forbid();

    }

    [HttpDelete("Delete")]
    [Authorize(Roles.Admin)]
    public string Delete(int id)
    {
        if (AgreementRepo.Delete(id) == 1)
            return "deleted successfully";
        else
            return "error while processing delete requiest";
    }

    [HttpPost("UpdateStatus")]
    [Authorize(Roles.Admin)]
    public IActionResult UpdateStatus(AgreementStatus status)
    {
        System.Console.WriteLine(status.FkAgreementStatus);
        if (AgreementRepo.UpdateStatus(status) == 1)
        {
            return Ok("Agreement status updated");
        };
        return BadRequest("Status was not update");
    }


}