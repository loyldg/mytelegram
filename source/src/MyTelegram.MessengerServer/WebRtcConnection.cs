namespace MyTelegram.MessengerServer;

public record WebRtcConnection
{
    public string Ip { get; set; } = default!;
    public string Ipv6 { get; set; } = default!;
    public int Port { get; set; }
    public bool Turn { get; set; }
    public bool Stun { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}