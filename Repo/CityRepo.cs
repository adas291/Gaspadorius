using Dapper;
using Microsoft.Data.Sqlite;

namespace Gaspadorius.Repos;

public static class CityRepo
{
    public static int? GetCityId(string name)
    {
        var query = "select id from city where name=@name";
        using var conn = new SqliteConnection("data source=data.db");
        return conn.QueryFirstOrDefault<int?>(query, new { name });
    }
}