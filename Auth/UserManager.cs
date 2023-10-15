
namespace Gaspadorius.Auth;

using Dapper;
using Microsoft.Data.Sqlite;
using Gaspadorius.Models;
using Gaspadorius.Models.Dto;

public class UserManager
{

    public UserManager() { }
    public static async Task<UserDto?> FindUser(int id)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        var query = @"SELECT * FROM User u
                      WHERE u.Id = @id;
                    ";
        return await conn.QueryFirstOrDefaultAsync<UserDto>(query, new { id });
    }


    public static async Task<UserDto?> FindUser(string name)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        var query = @"SELECT * FROM User u
                      WHERE u.Username = @name;
                    ";
        return await conn.QueryFirstOrDefaultAsync<UserDto?>(query, new { name });
    }

    public static async Task<List<string>> FindUserRoles(int fkUser)
    {
        using var conn = new SqliteConnection("Data Source=data.db");
        var query = @"SELECT r.RoleName 
                  FROM UserRole ur
                  INNER JOIN Role r ON ur.FkRole = r.Id
                  WHERE ur.FkUser = @FkUser;
                ";

        var roles = await conn.QueryAsync<string>(query, new { FkUser = fkUser });
        return roles.ToList();
    }


    public static async Task<long?> CreateUser(CreateUserDto user , string PasswordHash)
    {
        using var conn = new SqliteConnection("Data source=data.db");

        var query = @"INSERT INTO User (Username, Name, Surname, Phone, Email, PasswordHash)
                      VALUES(@Username, @Name, @Surname, @Phone,  @Email, @PasswordHash);
                      SELECT last_insert_rowid();
                    ";

        return (long?)await conn.ExecuteScalarAsync(query, new { user.Username, user.Name, user.Surname, user.Phone, user.Email, PasswordHash });
    }

    public static async Task<int> AddToRoleAsync(long FkUser, int FkRole)
    {
        var query = @"INSERT INTO UserRole(FkUser, FkRole)
                      VALUES(@FkUser, @FkRole)";

        using var conn = new SqliteConnection(Config.ConnectionString);
        var result = await conn.ExecuteAsync(query, new { FkUser, FkRole });
        return result;
    }

}