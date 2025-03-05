using System.Net;
using design_patterns.Adapter.DataProcessor;
using design_patterns.Factory.NetworkUtility;

namespace design_patterns.Adapter;

public class NetworkAdapter : INetworkClient
{
    private readonly IDataProcessor _dataProcessor;
    public NetworkAdapter(IDataProcessor dataProcessor)
    {
        _dataProcessor = dataProcessor;
    }
    public void SendRequest(string ip)
    {
        var apiKey = "23123123";
        _dataProcessor.SendRequest(ip, apiKey);
    }
}