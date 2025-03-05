using design_patterns.Factory.Factory;
using design_patterns.Factory.NetworkUtility;
using design_patterns.Proxy;
using design_patterns.Strategy;
using ARP = design_patterns.Strategy.ARP;
using DNS = design_patterns.Strategy.DNS;
using Ping = design_patterns.Strategy.Ping;

namespace design_patterns.Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        Context context = new Context(new DNS());
        Context context2 = new Context(new ARP());
        Context context3 = new Context(new Ping());
        
        context.Execute();
        context2.Execute();
        context3.Execute();
    }
}
