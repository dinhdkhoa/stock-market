namespace design_patterns.ChainOfResponsibility;

public interface IChain
{
    void SendRequest(NetworkModel request);
    void SetNext(IChain next);
}