namespace design_patterns.Adapter;

public interface INetworkClient
{
    void SendRequest(string ip);
}