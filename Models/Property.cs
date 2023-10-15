namespace Gaspadorius.Models;
using Gaspadorius.Auth;

public class Property : IUserOwnedResource
{
    public Property() { }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Size { get; set; }
    public int FkCity { get; set; }
    public int UserId { get; set; }
}