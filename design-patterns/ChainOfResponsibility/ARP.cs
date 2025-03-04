using design_patterns.ChainOfResponsibility;

namespace design_patterns.ChainOfResponsibility;

public class ARP : IChain
{
    private IChain _nextChain;

    public void SendRequest(NetworkModel request)
    {
        if (!request.success)
        {
            Console.WriteLine("ARP failed!");
            if (_nextChain != null)
            {
                _nextChain.SendRequest(request);
            }
            else
            {
                Console.WriteLine("No next chain! Program terminated!");

            }
        }
        else
        {
            Console.WriteLine("ARP sent!");

        }    
    }

    public void SetNext(IChain next)
    {
        _nextChain = next;
    }
}