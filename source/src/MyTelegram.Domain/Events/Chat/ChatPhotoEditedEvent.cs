namespace MyTelegram.Domain.Events.Chat;

public class ChatPhotoEditedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatPhotoEditedEvent(RequestInfo requestInfo,
        long chatId,
        //byte[] photo,
        long photoId,
        string messageActionData,
        long randomId) : base(requestInfo)
    {
        ChatId = chatId;
        PhotoId = photoId;
        //Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public long ChatId { get; }
    public long PhotoId { get; }
    public string MessageActionData { get; }
    //public byte[] Photo { get; }
    public long RandomId { get; }
}