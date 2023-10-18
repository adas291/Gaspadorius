using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Gaspadorius.Models;
using Gaspadorius.Repos.Maintenance;
using Gaspadorius.Repos;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ObjectiveC;
using System.Xml;
using Gaspadorius.Auth;
using System.Reflection.Metadata.Ecma335;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Gaspadorius.DataContext;
using Gaspadorius.Data.Models;
using Gaspadorius.Models.Dto;
using System.Collections.Immutable;

public class ExtraController : Controller
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IAuthorizationService _authorizationService;
    private readonly Gaspadorius.DataContext.UserContext _userContext;

    public ExtraController(UserContext userContext, JwtTokenService jwtTokenService, IAuthorizationService authorizationService)
    {
        _jwtTokenService = jwtTokenService;
        _authorizationService = authorizationService;
        _userContext = userContext;
    }

    [HttpGet("/insert/{id}")]
    public IActionResult InsertRow(int id)
    {
        City city = new City() { Id = id, Name = "Kaunas" };
        var entity = _userContext.Cities.Add(city);
        _userContext.SaveChanges();
        return Ok(entity.Entity);
    }
    [HttpGet("greet"), AllowAnonymous]
    public IActionResult Greet()
    {
        return Ok(new { message = "hello from backend apie" });
    }

    [HttpGet("list")]
    public IActionResult ListCities()
    {
        return Ok(_userContext.Cities.ToList<City>());
    }

    [HttpGet("phone")]
    [Authorize]
    public async Task<IActionResult> PhoneTest()
    {
        var phone = User.FindFirstValue(JwtRegisteredClaimNames.PhoneNumber);
        // foreach (var claim in User.Claims)
        // {
        //     Console.WriteLine($"{claim.Type}: {claim.Value}");
        // }

        var auth = await _authorizationService.AuthorizeAsync(User, phone, PolicyNames.HasPhone);

        if(auth.Succeeded)
        {
            return Ok("nice you have phone");
        }

        return BadRequest("you are not alowed phone club");
    }

    [HttpGet("api/testService")]
    public IActionResult TestService()
    {
        var result = _jwtTokenService != null;
        if(result){
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("bendras")]
    [Authorize]
    public IActionResult Bendras()
    {
        return Ok("Tu esi mldc");
    }

    [HttpGet("registered")]
    [Authorize(Roles="Registered user")]
    public IActionResult RegisteredPage()
    {
        return Ok("You are registered noice");
    }

    [HttpGet("admin")]
    [Authorize(Roles="Admin")]
    public IActionResult AdminPage()
    {
        return Ok("hello admin");
    }

    [HttpGet("api/test")]
    [Authorize]
    public string Test()
    {
        return string.Join('\n', User.Claims);
        // var service = new JwtTokenService(_configuration);
        // var token = service.CreateAccessToken("adomas", "1234", new List<string>() { "labasl", "ate" });
        // return $"Jwt token: {token}";
        // return _configuration["MySetting:LOL"] ?? "notfound";

    }

    [HttpGet("testToken")]
    [Authorize(Roles = "Admin")]
    public IActionResult ViewToken()
    {

        var result = "You are authorized\n";
        var userClaims = User.Claims;

        if(userClaims != null && userClaims.Any())
        {
            // var userData = userClaims.ToDictionary(claim => claim.Type);
            foreach (var item in userClaims)
            {
                System.Console.WriteLine(item.Type);
                result += $"{item.Type}: {item.Value}\n";
            }
            return Ok(result);
        }

        return BadRequest("no claims");

        // var service = new JwtTokenService(_configuration);
        // var token = service.CreateAccessToken("adomas", "1234", new List<string>() { "admin", "user" });
        // return $"Jwt token: {token}";
    }
    [HttpGet("api/{city}/maintenances")]
    public List<MaintenanceModel> GetMaint(string city)
    {
        var id = CityRepo.GetCityId(city);

        if (id is not null)
        {
            return MaintenanceRepo.Get(id);
        }
        return null;
        // return BadRequest("city not found");
    }

    [HttpGet("/api/city/{city}/leaseObject/{leaseObjectId}/agreementId/{agreementId}")]
    // public List<LeaseObject> GetMaintience(int userId, int objectId)
    public async Task<ActionResult<List<AgreementStatus>>> GetAgreementHistory(string city, int leaseObjectId, int agreementId)
    {
        if (AgreementRepo.IsValid(city, leaseObjectId, agreementId) == false)
        {
            return BadRequest("Agreement was not found");
        }
        
        var result = await AgreementRepo.GetAgreementHistoryAsync(agreementId);
        
        return result;
    }


}