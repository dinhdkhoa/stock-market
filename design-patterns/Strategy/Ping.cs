using design_patterns.Strategy;

namespace design_patterns.Strategy;
public class Ping : IStrategy
{

    public void ExecuteStrategy()
    {
        Console.WriteLine("Ping !!");
    }
}