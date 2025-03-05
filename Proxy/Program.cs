using design_patterns.Factory.Factory;
using design_patterns.Factory.NetworkUtility;
using design_patterns.Proxy;

namespace design_patterns.Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        SuperSecretDbProxy dbProxy = new SuperSecretDbProxy("testDb", "testPassword");
        dbProxy.DisplayDbName(); //wrong pw
        SuperSecretDbProxy dbProxy2 = new SuperSecretDbProxy("testDb", "Password");
        dbProxy2.DisplayDbName(); //wrong pw
    }
}
