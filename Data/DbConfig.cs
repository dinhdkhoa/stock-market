namespace stock_market.Data;

public static class DbConfig
{
    public static string GetDbPath()
    {
        var sqliteFolder = Path.Combine(Directory.GetCurrentDirectory(), "Database");
        if (!Directory.Exists(sqliteFolder))
        {
            Directory.CreateDirectory(sqliteFolder);
        }

        return Path.Combine(sqliteFolder, "sqlite.db");
    }
}