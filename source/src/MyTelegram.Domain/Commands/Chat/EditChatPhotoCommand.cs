namespace MyTelegram.Domain.Commands.Chat;

public class EditChatPhotoCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatPhotoCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long fileId,
        byte[] photo,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        FileId = fileId;
        Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public long FileId { get; }
    public string MessageActionData { get; }
    public byte[] Photo { get; }
    public long RandomId { get; }
}
