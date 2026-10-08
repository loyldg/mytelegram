namespace MyTelegram.Domain.Sagas.Events;

public class InboxMessageEditCompletedSagaEvent(
    RequestInfo requestInfo,
    MessageItem oldMessageItem,
    MessageItem newMessageItem)
    : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public MessageItem OldMessageItem { get; } = oldMessageItem;
    public MessageItem NewMessageItem { get; } = newMessageItem;
}