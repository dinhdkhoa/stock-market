namespace design_patterns.Strategy;

public class Context
{
    IStrategy strategy;

    public Context(IStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void Execute()
    {
        strategy.ExecuteStrategy();
    }
}