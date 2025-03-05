using design_patterns.Strategy;

namespace design_patterns.Strategy;
public class DNS : IStrategy
{

    public void ExecuteStrategy()
    {
        Console.WriteLine("DNS Work");
    }
}