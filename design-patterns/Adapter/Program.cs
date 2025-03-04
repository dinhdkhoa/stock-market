
using design_patterns.Adapter;
using design_patterns.Adapter.DataProcessor;
using design_patterns.Facade;

internal partial class Program
{
    public static void Main(string[] args)
    {
        INetworkClient networkClient = new NetworkClient();
        networkClient.SendRequest("123123");
        
        IDataProcessor dataProcessor = new DataProcessor();
        dataProcessor.SendRequest("asdasdasd", "12345asdasd");

        INetworkClient networkAdapter = new NetworkAdapter(dataProcessor);
        networkAdapter.SendRequest("123123");
    }
}