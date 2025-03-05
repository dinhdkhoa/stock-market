using design_patterns.Factory.Factory;
using design_patterns.Factory.NetworkUtility;

namespace design_patterns.Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        //Before Factory
        // Ping ping = new Ping();
        // DNS dns = new DNS();
        // ARP arp = new ARP();
        
        //With Factory
        NetworkFactory factory = new NetworkFactory();
        
        var ping = factory.GetPing();
        var dns = factory.GetDNS();
        var arp = factory.GetARP();
        
        ping.SendRequest("''", 1);
        dns.SendRequest("''", 1);
        arp.SendRequest("''", 1);
    }
}
