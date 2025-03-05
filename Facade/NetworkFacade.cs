namespace design_patterns.Facade;

public class NetworkFacade
{
    private Packet _packet;
    private Transmission _transmission;
    private Socket _socket;

    public NetworkFacade(string ip, string protocol)
    {
        _socket = new Socket(ip);
        _packet = new Packet(ip);
        _transmission = new Transmission(protocol);
    }

    private void NetworkConnect()
    {
        _socket.Connect();
        _packet.PacketBuild();
    }

    public void SendTransmission()
    {
        NetworkConnect();
        _transmission.SendTransmission();
    }
}