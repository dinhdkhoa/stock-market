using design_patterns.Factory.Factory;
using design_patterns.Factory.NetworkUtility;
using design_patterns.Proxy;
using design_patterns.Strategy;
using ARP = design_patterns.Strategy.ARP;
using DNS = design_patterns.Strategy.DNS;
using Ping = design_patterns.Strategy.Ping;

namespace design_patterns.ChainOfResponsibility;

internal class Program
{
    public static void Main(string[] args)
    {
        IChain ping = new Ping(); //should be named as a verb:  IChain ping = new SendPing();
        IChain dns = new DNS();
        IChain arp = new ARP();
        ping.SetNext(dns);
        dns.SetNext(arp);
        
        NetworkModel networkModel = new NetworkModel("127.0.0.1", false);
        ping.SendRequest(networkModel);
    }
}
