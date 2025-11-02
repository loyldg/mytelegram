namespace MyTelegram.Abstractions;

/// <summary>
/// 
/// </summary>
/// <param name="AuthKeyId"></param>
/// <param name="MsgKey">int128,length=16</param>
/// <param name="EncryptedData"></param>
/// <param name="ConnectionId"></param>
/// <param name="ConnectionType"></param>
/// <param name="ClientIp"></param>
/// <param name="RequestId"></param>
/// <param name="Date"></param>
public record EncryptedMessage(long AuthKeyId,
    ReadOnlyMemory<byte> MsgKey,
    ReadOnlyMemory<byte> EncryptedData,
    string ConnectionId,
    ConnectionType ConnectionType,
    int DcId,
    string ClientIp,
    Guid RequestId,
    long Date
) : IMtpMessage
{
    public string ConnectionId { get; set; } = ConnectionId;
    public ConnectionType ConnectionType { get; set; } = ConnectionType;
    public int DcId { get; set; } = DcId;
    public string ClientIp { get; set; } = ClientIp;
    [JsonIgnore] public IMemoryOwner<byte>? MemoryOwner { get; set; }
}