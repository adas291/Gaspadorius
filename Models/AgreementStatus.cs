using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Models;

public class AgreementStatus
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int FkAgreementStatus{ get; set; }
    [Required]
    public DateTime Date { get; set; }
    public string? Comments { get; set; } 
}