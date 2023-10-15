
using System.Formats.Tar;
using Dapper;
using Microsoft.Data.Sqlite;
using Gaspadorius.Models;

namespace Gaspadorius.Repos;

public static class AgreementRepo
{
    public static int Create(AgreemenDto agreement)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        conn.Open();

        var query =
        $@"
            INSERT INTO Agreement(Title, FkAgreementType, FkTenant, FkObject, PriceInCents, StartDate, Comments)
            VALUES(@Title, @FkAgreementType, @FkTenant, @FkObject, @PriceInCents, @StartDate, @Comments)
        ";

        var parameters = new
        {
            agreement.Title,
            agreement.FkAgreementType,
            agreement.UserId,
            agreement.FkObject,
            agreement.PriceInCents,
            StartDate = agreement.StartDate.ToString("yyyy-MM-dd"),
            agreement.Comments
        };

        return conn.Execute(query, parameters);
    }


    public static async Task<AgreemenDto?> Get(int id)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        conn.Open();
        var query = $@"SELECT * FROM Agreement WHERE id=@id";

        return await conn.QueryFirstOrDefaultAsync<AgreemenDto>(query, new { id });
    }
    public static async Task<AgreemenDto?> GetFull(int cityId, int propertyId, int agreementId)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        conn.Open();
        var query = $@"SELECT * FROM Agreement a
                        WHERE 
                            id=@agreementId AND
                            a.FkCity = @cityId AND
                            a.FkProperty = @propertyId";

        return await conn.QueryFirstOrDefaultAsync<AgreemenDto>(query, new { cityId, propertyId, agreementId});
    }


    public static int Delete(int Id)
    {
        var query = "DELETE FROM Agreement WHERE Id=@Id";
        using var conn = new SqliteConnection("Data source=data.db");
        return conn.Execute(query, new { Id });
    }

    public static int UpdateStatus(AgreementStatus status)
    {
        var query = @"
                INSERT INTO AgreementHistory(FkAgreement, FkStatus, Date)
                VALUES(@Id, @FkAgreementStatus, @Date)";

        using var conn = new SqliteConnection("Data source=data.db");
        var parameters = new
        {
            status.Id,
            status.FkAgreementStatus,
            status.Comments,
            Date = DateTime.Now.ToString()
        };

        return conn.Execute(query, parameters);
    }


    public static bool IsValid(string city, int leaseObjectId, int agreementId)
    {
        var fkCity = CityRepo.GetCityId(city) ?? throw new Exception("City not found");

        var query = @"SELECT * FROM agreement a WHERE a.Id = @agreementId AND a.FkCity = @fkCity AND a.FkObject = @leaseObjectId";

        using var conn = new SqliteConnection("data source=data.db");
        conn.Open();
        return conn.QueryFirstOrDefault(query, new { leaseObjectId, fkCity, agreementId }) is not null;

    }

    public static async Task<List<AgreementStatus>> GetAgreementHistoryAsync(int agreementId)
    {
        using var conn = new SqliteConnection("Data source=data.db");
        conn.Open();

        var query = $@"
                        SELECT * from AgreementHistory ah
                        WHERE ah.FkAgreement = @agreementId
                    ";

        var result = await conn.QueryAsync<AgreementStatus>(query, new { agreementId });
        return result.ToList();

        // return conn.Query<AgreementModel>(query, new { id }).ToList();   
    }

}