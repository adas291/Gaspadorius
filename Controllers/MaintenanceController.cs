using System.Net.Http.Headers;
using Dapper;
using Gaspadorius.Repos.Maintenance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Gaspadorius.Models;
using Gaspadorius.Repos;

namespace Gaspadorius.Controllers;

[Route("api/[controller]")]
public class MaintenanceController : Controller
{
    // [Route("/{city}/default")]
    // public IActionResult GetByCity(string city)
    // {
    //     return Ok("reached");
    // }

    [HttpGet]
    public List<MaintenanceModel> Get(int? id)
    {
        return MaintenanceRepo.Get(id);
    }


    [HttpPost("Create")]
    public IActionResult Create(MaintenanceModel model)
    {
        return MaintenanceRepo.Create(model) == 1 ? Ok("created") : BadRequest("error");
    }

    [HttpPost("Update")]
    public IActionResult Update(MaintenanceModel model)
    {
        return MaintenanceRepo.Update(model) == 1 ? Ok("created") : BadRequest("error");
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int Id)
    {
        return MaintenanceRepo.Delete(Id) == 1 ? Ok() : BadRequest();
    }
}