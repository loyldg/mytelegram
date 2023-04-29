namespace MyTelegram.MTProto;

public interface IMtpMessage //: IDisposable
{
    string? ClientIp { get; set; }
    string ConnectionId { get; set; }
}
