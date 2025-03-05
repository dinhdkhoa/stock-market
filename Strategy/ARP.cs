using design_patterns.Strategy;

namespace design_patterns.Strategy;

public class ARP : IStrategy
{
    public void ExecuteStrategy()
    {
        Console.WriteLine("ARP Strategy");
    }
}