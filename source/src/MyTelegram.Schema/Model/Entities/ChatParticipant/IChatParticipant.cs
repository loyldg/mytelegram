// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatParticipant : IObject
{
    long UserId { get; set; }

}
