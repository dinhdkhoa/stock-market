namespace design_patterns.Adapter;

public class NetworkClient : INetworkClient
{
    public void SendRequest(string ip)
    {
        Console.WriteLine($"Sending request to {ip}");
    }
}