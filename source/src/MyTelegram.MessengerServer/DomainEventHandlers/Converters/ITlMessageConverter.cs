namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlMessageConverter
{
    IMessage ToMessage(MessageItem item,
        long selfUserId = 0);

    IMessage ToMessage(InboxMessageEditCompletedEvent aggregateEvent);

    IMessage ToMessage(OutboxMessageEditCompletedEvent aggregateEvent,
        long selfUserId);

    IMessageFwdHeader? ToMessageFwdHeader(MessageFwdHeader? messageFwdHeader);
    IMessageReplyHeader? ToMessageReplyHeader(int? replyToMessageId);

    IList<IMessage> ToMessages(IReadOnlyCollection<IMessageReadModel> readModels,
        long selfUserId);
}
