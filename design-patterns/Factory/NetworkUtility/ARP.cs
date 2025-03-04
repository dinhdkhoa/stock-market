namespace design_patterns.Factory.NetworkUtility;

public class ARP
{
    public void SendRequest(string ip, int timeSent)
    {
        Console.WriteLine($"Ping request to {ip} for {timeSent} times.");
    }
}