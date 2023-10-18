namespace Gaspadorius.Models;

using System.ComponentModel.DataAnnotations;
using Gaspadorius.Auth;

public class AgreemenDto : IUserOwnedResource
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public int UserId { get; set; }
    [Required]
    public int FkProperty { get; set; }
    [Required]
    public int FkAgreementType { get; set; }
    [Required]
    public int PriceInCents { get; set; }
    public string Comments { get; set; } = string.Empty;
    [Required]
    public DateTime StartDate { get; set; }
    [Required]public int FkCity { get; set; }

}
