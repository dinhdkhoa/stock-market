namespace design_patterns.Facade;

public class Socket
{
    public string Ip  { get; set; }
    public int Port  { get; set; }

    public Socket(string ip)
    {
        this.Ip = ip;
    }
    
    public void Connect()
    {
        Console.WriteLine("Socket connected");
    }
}