namespace MyTelegram.Domain.Events.Chat;

public class NewChatMsgIdPinnedEvent : AggregateEvent<ChatAggregate, ChatId>
{
    public NewChatMsgIdPinnedEvent(int pinnedMsgId)
    {
        PinnedMsgId = pinnedMsgId;
    }

    public int PinnedMsgId { get; }
}
