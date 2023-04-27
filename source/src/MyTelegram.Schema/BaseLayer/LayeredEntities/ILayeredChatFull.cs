// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredChatFull : MyTelegram.Schema.IChatFull
{
    MyTelegram.Schema.IChatParticipants Participants { get; set; }
}