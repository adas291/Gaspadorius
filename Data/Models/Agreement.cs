using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Data.Models;

public class Agreement
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Title { get; set; }
    [Required]
    public User FkTenant { get; set; }
    [Required]
    public City City{ get; set; }
}