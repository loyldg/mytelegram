// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILayeredChatFull : Schema.IChatFull
{
    Schema.IChatParticipants Participants { get; set; }
}
