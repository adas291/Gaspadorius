namespace Gaspadorius.Auth;
using Microsoft.AspNetCore.Authorization;

public interface IUserHasPhone
{
    public string Phone { get; set; }
}

public interface IUserOwnedResource
{
    public int UserId { get; set; }
}
