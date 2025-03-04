namespace design_patterns.Adapter.DataProcessor;

public class DataProcessor : IDataProcessor
{
    public void SendRequest(string ip, string ApiKey)
    {
        Console.WriteLine($"Sending request to with APikey : {ip}");
    }

    public bool DataAvailable()
    {
        return true;
    }
}