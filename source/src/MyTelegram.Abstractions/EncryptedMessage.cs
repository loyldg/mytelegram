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
    long Date,
    bool QuickAckRequested = false
) : IMtpMessage
{
    public string ConnectionId { get; set; } = ConnectionId;
    public ConnectionType ConnectionType { get; set; } = ConnectionType;
    public int DcId { get; set; } = DcId;
    public string ClientIp { get; set; } = ClientIp;

    /// <summary>
    /// The client requested a transport-level quick ACK for this payload (MSB of the transport length).
    /// The session server answers with a quick ACK token once the payload has been decrypted and accepted.
    /// </summary>
    public bool QuickAckRequested { get; set; } = QuickAckRequested;

    [JsonIgnore] public IMemoryOwner<byte>? MemoryOwner { get; set; }
}