namespace MyTelegram.Abstractions;

public interface IMtpMessage : IMayHaveMemoryOwner
{
    string ClientIp { get; set; }
    string ConnectionId { get; set; }
    ConnectionType ConnectionType { get; set; }
    int DcId { get; set; }
}