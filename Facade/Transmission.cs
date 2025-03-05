namespace design_patterns.Facade;

public class Transmission
{
    public string Name  { get; set; }
    public int Port  { get; set; }

    public Transmission(string protocolname)
    {
        this.Name = protocolname;
    }

    public void SendTransmission()
    {
        Console.WriteLine("Transmission sent");
    }
}