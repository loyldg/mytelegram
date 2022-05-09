using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class SendChatMessageStartedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public string Title { get; }
    public IReadOnlyList<long> ChatMemberUidList { get; }

    public SendChatMessageStartedEvent(string title, IReadOnlyList<long> chatMemberUidList)
    {
        Title = title;
        ChatMemberUidList = chatMemberUidList;
    }
}