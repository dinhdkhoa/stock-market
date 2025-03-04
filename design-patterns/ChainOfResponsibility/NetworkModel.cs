namespace design_patterns.ChainOfResponsibility;

public class NetworkModel
{
    public string IP  { get; set; }
    public bool success  { get; set; }

    public NetworkModel(string ip , bool success)
    {
        this.IP  = ip;
        this.success = success;
    }
}