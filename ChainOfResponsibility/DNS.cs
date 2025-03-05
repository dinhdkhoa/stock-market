using design_patterns.ChainOfResponsibility;

namespace design_patterns.ChainOfResponsibility;
public class DNS : IChain
{
    private IChain _nextChain;

    public void SendRequest(NetworkModel request)
    {
        if (!request.success)
        {
            Console.WriteLine("DNS failed! Sending ARP.");
            _nextChain.SendRequest(request);
        }  else
        {
            Console.WriteLine("DNS sent!");

        }    }

    public void SetNext(IChain next)
    {
        _nextChain = next;
    }
}