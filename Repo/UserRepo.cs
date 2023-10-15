using Microsoft.Data.Sqlite;
using Gaspadorius.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace Gaspadorius.Repos;

public static class UserRepo
{

    public static async Task<List<UserDto>> GetAllUsersAsync()
    {
        var query = "SELECT * FROM USER";
        var conn = new SqliteConnection("Data source=data.db");
        var result = await conn.QueryAsync<UserDto>(query);
        return result.ToList();
    }
    public static async Task<UserDto?> GetUserAsync(int id)
    {
        var query = "SELECT * FROM USER WHERE UserId=@id";
        var conn = new SqliteConnection("Data source=data.db");
        
        return await conn.QueryFirstOrDefaultAsync<UserDto>(query, new { id });
    }

    public static async Task<int> CreateAsync(UserDto user)
    {
        string query = "INSERT INTO User (Name, Surname, Phone, Email) VALUES (@Name, @Surname, @Phone, @Email)";
        using var conn = new SqliteConnection("Date source=data.db");

        var parameters = new
        {
            user.Name,
            user.Surname,
            user.Phone,
            user.Email
        };

        return await conn.ExecuteAsync(query, parameters);
    }

    public static async Task<int> Delete(int Id)
    {
        var query = "DELETE FROM User WHERE UserId=@Id";
        using var conn = new SqliteConnection("Data source=data.db");
        return await conn.ExecuteAsync(query, new { Id });
    }

    public static async Task<int> Update(UserDto user)
    {

        string query = @"
                            UPDATE User
                            SET Name = @Name, Surname = @Surname, Phone = @Phone, Email = @Email
                            WHERE UserId = @UserId
                        ";

        using var conn = new SqliteConnection("Data source=data.db");

        var parameters = new
        {
            user.UserId,
            user.Name,
            user.Surname,
            user.Phone,
            user.Email
        };

        return await conn.ExecuteAsync(query, parameters);
    }

    public static List<Property> GetMaintienceHistory(int objectId, int userId)
    {
        var query = @"SELECT * FROM LeaseObject lo
                    WHERE lo.FkOwner=@userId AND lo.Id = @objectId";
        using var conn = new SqliteConnection("Data source=data.db");
        conn.Open();

        var userObjects = conn.Query<Property>(query, new { objectId, userId }).ToList();

        return userObjects;
    }
}