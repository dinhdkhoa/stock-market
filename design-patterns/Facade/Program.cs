
using design_patterns.Facade;

internal class Program
{
    public static void Main(string[] args)
    {
        NetworkFacade network = new NetworkFacade("192.168.0.1", "UDP");
        
        network.SendTransmission();
    }
}