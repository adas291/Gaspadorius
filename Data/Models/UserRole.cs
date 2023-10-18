using Microsoft.EntityFrameworkCore;

namespace Gaspadorius.Data.Models;

public class UserRoles : DbContext
{
    public User User { get; set; }
}