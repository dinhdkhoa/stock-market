namespace design_patterns.Factory.NetworkUtility;

public class Ping
{
    public void SendRequest(string ip, int timeSent)
    {
        Console.WriteLine($"Ping request to {ip} for {timeSent} times.");
    }
}