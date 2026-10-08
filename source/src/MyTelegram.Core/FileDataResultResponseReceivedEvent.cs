using System.Buffers;

namespace MyTelegram.Core;

public record FileDataResultResponseReceivedEvent(
    string ConnectionId,
    long PermAuthKeyId,
    long TempAuthKeyId,
    long SessionId,
    long ReqMsgId,
    ReadOnlyMemory<byte> Data
//byte[] Data
) : ISessionMessage
{
    public IMemoryOwner<byte>? MemoryOwner { get; set; }
}