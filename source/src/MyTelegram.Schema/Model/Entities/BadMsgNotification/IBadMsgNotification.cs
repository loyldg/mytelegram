// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBadMsgNotification : IObject
{
    long BadMsgId { get; set; }
    int BadMsgSeqno { get; set; }
    int ErrorCode { get; set; }

}
