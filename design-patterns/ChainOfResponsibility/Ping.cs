using design_patterns.ChainOfResponsibility;

namespace design_patterns.ChainOfResponsibility;
public class Ping : IChain
    
{
    private IChain _nextChain;
    public void SendRequest(NetworkModel request)
    {
        if (!request.success)
        {
            Console.WriteLine("Ping failed! Sending DNS.");
            _nextChain.SendRequest(request);
        }else
        {
            Console.WriteLine("Ping sent!");
        }  
    }

    public void SetNext(IChain next)
    {
        _nextChain = next;
    }
}