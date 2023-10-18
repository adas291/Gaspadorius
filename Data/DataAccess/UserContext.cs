using Gaspadorius.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaspadorius.DataContext;


public class UserContext : DbContext
{
    public UserContext(DbContextOptions options) : base(options) {}

    public DbSet<User> Users{ get; set; }
    public DbSet<City> Cities{ get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Agreement> Agreementst{ get; set; }
}
