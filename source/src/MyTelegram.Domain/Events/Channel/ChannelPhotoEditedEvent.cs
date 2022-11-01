namespace MyTelegram.Domain.Events.Channel;

public class ChannelPhotoEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public ChannelPhotoEditedEvent(RequestInfo requestInfo,
        long channelId,
        byte[] photo,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(requestInfo)
    {
        ChannelId = channelId;
        Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public string MessageActionData { get; }
    public byte[] Photo { get; }
    public long RandomId { get; }
    public Guid CorrelationId { get; }
}
