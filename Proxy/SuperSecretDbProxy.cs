namespace design_patterns.Proxy;

public class SuperSecretDbProxy
{
    private SuperSecretDb _superSecretDb;
    private string dbName;
    private string password;

    public SuperSecretDbProxy(string dbName, string password)
    {
        this.dbName = dbName;
        this.password = password;
    }

    public void DisplayDbName()
    {
        if (password.Equals("Password"))
        {
            if (_superSecretDb == null)
            {
                _superSecretDb = new SuperSecretDb(dbName);
            }
            _superSecretDb.DisplayDbName();
            return;
        }
        Console.WriteLine("Wrong password! Not allowed.");
    }
    
}