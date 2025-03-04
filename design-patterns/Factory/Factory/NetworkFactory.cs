using design_patterns.Factory.NetworkUtility;

namespace design_patterns.Factory.Factory;

public class NetworkFactory
{
    public INetwork GetDNS()
    {
        return new DNS();
    }
    public INetwork GetARP()
    {
        return new ARP();
    }
    public INetwork GetPing()
    {
        return new Ping();
    }
}