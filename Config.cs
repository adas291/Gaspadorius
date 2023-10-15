namespace Gaspadorius;

public static class Config
{
    private static IConfiguration _configuration;
    public static void ConfigureServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string ConnectionString => _configuration["ConnectionStrings:Default"] ?? throw new Exception("dbstring not found");

}