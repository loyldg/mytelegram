namespace MyTelegram.Domain.Commands.Messaging;

public class EditOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public int EditDate { get; }
    public byte[]? Media { get; }
    public Guid CorrelationId { get; }

    public EditOutboxMessageCommand(MessageId aggregateId,
        RequestInfo request,
        int messageId, string newMessage, byte[]? entities, int editDate, byte[]? media, Guid correlationId) : base(aggregateId, request)
    {
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        EditDate = editDate;
        Media = media;
        CorrelationId = correlationId;
    }
}