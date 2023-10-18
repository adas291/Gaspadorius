using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace Gaspadorius.Data.Models;

public class User : IdentityUser<int>
{
    public string? PersonalData{ get; set; }
}