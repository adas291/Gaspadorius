using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Data.Models;


public class Role
{
    public int Id { get; set; }
    
    [Required, MaxLength(15)]
    public string Name { get; set; }
}