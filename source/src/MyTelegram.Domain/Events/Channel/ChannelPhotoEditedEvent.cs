namespace MyTelegram.Domain.Events.Channel;

public class ChannelPhotoEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelPhotoEditedEvent(RequestInfo requestInfo,
        long channelId,
        //byte[] photo,
        long? photoId,
        string messageActionData,
        long randomId) : base(requestInfo)
    {
        ChannelId = channelId;
        PhotoId = photoId;
        //Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public long ChannelId { get; }
    public long? PhotoId { get; }
    public string MessageActionData { get; }
    //public byte[] Photo { get; }
    public long RandomId { get; }
}