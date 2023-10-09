namespace MyTelegram.Domain.Commands.Chat;

public class EditChatPhotoCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>
{
    public EditChatPhotoCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long fileId,
        //byte[] photo,
        long photoId,
        string messageActionData,
        long randomId) : base(aggregateId, requestInfo)
    {
        FileId = fileId;
        PhotoId = photoId;
        //Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }
    public long FileId { get; }
    public long PhotoId { get; }

    public string MessageActionData { get; }
    //public byte[] Photo { get; }
    public long RandomId { get; }
}