// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMsgDetailedInfo : IObject
{
    long AnswerMsgId { get; set; }
    int Bytes { get; set; }
    int Status { get; set; }
}
