namespace design_patterns.Facade;

public class Packet
{
    public string IP  { get; set; }
    public int Port  { get; set; }

    public Packet(string ip)
    {
        this.IP = ip;
    }

    public void PacketBuild()
    {
        Console.WriteLine("Packet built");
    }
}