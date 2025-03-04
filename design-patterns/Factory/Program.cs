using design_patterns.Factory.NetworkUtility;

namespace design_patterns.Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        Ping ping = new Ping();
        DNS dns = new DNS();
        ARP arp = new ARP();
    }
}