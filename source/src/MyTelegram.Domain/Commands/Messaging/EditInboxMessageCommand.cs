namespace MyTelegram.Domain.Commands.Messaging;

public class EditInboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>
{
    public EditInboxMessageCommand(MessageId aggregateId,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media,
        Guid correlationId) : base(aggregateId)
    {
        MessageId = messageId;
        NewMessage = newMessage;
        EditDate = editDate;
        Entities = entities;
        Media = media;
        CorrelationId = correlationId;
    }

    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public int EditDate { get; }
    public Guid CorrelationId { get; }
}
