using Dapper;
using Microsoft.Data.Sqlite;
using Gaspadorius.Models;

namespace Gaspadorius.Repos.Maintenance;

public static class MaintenanceRepo
{

    public static List<MaintenanceModel> Get(int? FkCity = 0, int? id = null)
    {
        var query = @$"SELECT * FROM Maintenance";

        if(id != null)
        {
            query += " WHERE id=@id";
        }

        if(FkCity is not null )
        {
            query = @$"
                        SELECT *
                        FROM Property lo
                        LEFT JOIN Maintenance m ON lo.Id = m.FkObject
                        WHERE lo.FkCity = @FkCity
                    ";
        }

        using var conn = new SqliteConnection("Data source=data.db");
        return conn.Query<MaintenanceModel>(query, new {FkCity, id}).ToList();
    }
    public static int Create(MaintenanceModel model)
    {
        var query = @"
                    INSERT INTO 'Maintenance'
                    (Id, Date, EventName, FkObject)
                    VALUES (@Id, @Date, @EventName, @FkObject);
                ";

        using var conn = new SqliteConnection("data source=data.db");

        var param = new
        {
            model.Id,
            model.Date,
            model.EventName,
            model.FkObject
        };

        return conn.Execute(query, param);
    }

    public static int Update(MaintenanceModel model)
    {
        var query = @"
        UPDATE Maintenance
        SET Date = @Date, EventName = @EventName, FkObject = @FkObject
        WHERE Id = @Id;
    ";

        using var conn = new SqliteConnection("data source=data.db");

        var param = new
        {
            model.Id,
            model.Date,
            model.EventName,
            model.FkObject
        };

        return conn.Execute(query, param);
    }


    public static int Delete(int Id)
    {
        var query = "DELETE FROM Maintenance WHERE Id=@Id";
        using var conn = new SqliteConnection("Data source=data.db");
        return conn.Execute(query, new { Id });
    }
}