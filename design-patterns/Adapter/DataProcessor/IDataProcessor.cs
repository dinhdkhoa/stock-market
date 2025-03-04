namespace design_patterns.Adapter.DataProcessor;

public interface IDataProcessor
{

    void SendRequest(string ip, string ApiKey);
    bool DataAvailable();
}