// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageUserVote : IObject
{
    long UserId { get; set; }
    int Date { get; set; }

}
