using System.Collections.Immutable;
using Dapper;
using Microsoft.Data.Sqlite;
using Gaspadorius.Models;
using Gaspadorius.Models.Dto;

namespace Gaspadorius.Repos;

public static class PropertyRepo
{
    public static async Task<Property?> GetProperty(int id)
    {
        var query = "SELECT * FROM Property";

        if (id != null)
        {
            query += " WHERE id=@id";
        }
        var conn = new SqliteConnection("Data source=data.db");

        return await conn.QueryFirstOrDefaultAsync<Property>(query, new { id });
    }

    public static async Task<List<Property>> GetAllProperties()
    {
        var query = "SELECT * FROM Property";
        var conn = new SqliteConnection("Data source=data.db");
        var result = await conn.QueryAsync<Property>(query);
        return result.ToList();
    }

    public static int CreateProperty(Property leaseObject)
    {
        string query = "INSERT INTO Property (Title, Address, Size, FkCity, FkOwner) VALUES (@Title, @Address, @Size, @FkCity, @FkOwner)";

        using var conn = new SqliteConnection("Data source=data.db");
        var parameters = new
        {
            leaseObject.Title,
            leaseObject.Address,
            leaseObject.FkCity,
            leaseObject.UserId,
            leaseObject.Size
        };

        return conn.Execute(query, parameters);
    }

    public static int Delete(int Id)
    {
        var query = "DELETE FROM Property WHERE Id=@Id";
        using var conn = new SqliteConnection("Data source=data.db");
        return conn.Execute(query, new { Id });
    }

    public static int Update(Property leaseObject)
    {

        string query = @"
                            UPDATE Property
                            SET Title = @Title, Address = @Address, Size = @Size, FkCity = @FkCity, FkOwner = @FkOwner
                            WHERE Id = @Id;
                        ";

        using var conn = new SqliteConnection("Data source=data.db");
        return conn.Execute(query, leaseObject);
    }


}