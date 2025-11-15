using System.Buffers;

namespace MyTelegram.Core;

public record AuthKeyCreatedIntegrationEvent(
    string ConnectionId,
    ConnectionType ConnectionType,
    long ReqMsgId,
    ReadOnlyMemory<byte> Data,
    long ServerSalt,
    bool IsPermanent,
    ReadOnlyMemory<byte> SetClientDhParamsAnswer,
    int? DcId
) : IMayHaveMemoryOwner
{
    public IMemoryOwner<byte>? MemoryOwner { get; set; }
}