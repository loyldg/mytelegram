namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ITlMessageConverter
{
    IMessage ToMessage(MessageItem item, long selfUserId = 0);
    IList<IMessage> ToMessages(IReadOnlyCollection<IMessageReadModel> readModels, long selfUserId);
    IMessage ToMessage(InboxMessageEditCompletedEvent aggregateEvent);
    IMessage ToMessage(OutboxMessageEditCompletedEvent aggregateEvent, long selfUserId);
    IMessageReplyHeader? ToMessageReplyHeader(int? replyToMessageId);
    IMessageFwdHeader? ToMessageFwdHeader(MessageFwdHeader? messageFwdHeader);
}