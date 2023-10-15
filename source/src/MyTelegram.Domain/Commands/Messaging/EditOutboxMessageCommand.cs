namespace MyTelegram.Domain.Commands.Messaging;

public class EditOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public int EditDate { get; }
    public byte[]? Media { get; }
    public List<long>? ChatMembers { get; }

    public EditOutboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        int messageId, string newMessage, byte[]? entities, int editDate, byte[]? media, List<long>? chatMembers) : base(aggregateId, requestInfo)
    {
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        EditDate = editDate;
        Media = media;
        ChatMembers = chatMembers;
    }
}
