namespace design_patterns.Proxy;

public class SuperSecretDb : ISuperSecretDb
{
    private string _databaseName;

    public SuperSecretDb(string databaseName)
    {
        _databaseName = databaseName;
    }

    public void DisplayDbName()
    {
        Console.WriteLine($"Database name: {_databaseName}");
    }
}