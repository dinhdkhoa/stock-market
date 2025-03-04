
using design_patterns.Facade;

internal partial class Program
{
    public static void Main(string[] args)
    {
        NetworkFacade network = new NetworkFacade("192.168.0.1", "UDP");
        
        network.SendTransmission();
    }
}