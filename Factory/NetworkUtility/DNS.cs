namespace design_patterns.Factory.NetworkUtility;

public class DNS : INetwork
{
    public void SendRequest(string ip, int timeSent)
    {
        Console.WriteLine($"Ping request to {ip} for {timeSent} times.");
    }
}