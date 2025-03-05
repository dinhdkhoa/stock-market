namespace design_patterns.Factory.NetworkUtility;

public interface INetwork
{
    void SendRequest(string ip, int timeSent);
}