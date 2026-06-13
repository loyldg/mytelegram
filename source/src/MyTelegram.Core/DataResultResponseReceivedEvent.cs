using System.Buffers;
using MyTelegram.Schema;

namespace MyTelegram.Core;

public record DataResultResponseReceivedEvent(
    string ConnectionId,
    long TempAuthKeyId,
    long SessionId,
    long ReqMsgId,
    ReadOnlyMemory<byte> Data
) : ISessionMessage
{
   public IObject? DataObject { get; set; }
   public IMemoryOwner<byte>? MemoryOwner { get; set; }
}