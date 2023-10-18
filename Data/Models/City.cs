using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Data.Models;

public class City
{
    public int Id { get; set; }
    [Required, MaxLength(30)]
    public string Name { get; set; }
}