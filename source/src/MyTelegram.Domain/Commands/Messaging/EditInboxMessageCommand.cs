namespace MyTelegram.Domain.Commands.Messaging;

public class EditInboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public int EditDate { get; }

    public EditInboxMessageCommand(MessageId aggregateId, RequestInfo requestInfo, int messageId, string newMessage, int editDate, byte[]? entities, byte[]? media) : base(aggregateId, requestInfo)
    {
        MessageId = messageId;
        NewMessage = newMessage;
        EditDate = editDate;
        Entities = entities;
        Media = media;
    }
}