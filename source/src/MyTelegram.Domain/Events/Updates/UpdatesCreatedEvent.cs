using MyTelegram.Domain.Aggregates.Updates;

namespace MyTelegram.Domain.Events.Updates;

public class UpdatesCreatedEvent : AggregateEvent<UpdatesAggregate, UpdatesId>
{
    public UpdatesCreatedEvent(long ownerPeerId,
        //long channelId,
        long? excludeAuthKeyId, long? excludeUserId, long? onlySendToUserId, long? onlySendToThisAuthKeyId,
        UpdatesType updatesType, int pts, int? messageId, int date, long globalSeqNo,
        IList<IUpdate>? updates,
        List<long>? users, List<long>? chats)

    {
        OwnerPeerId = ownerPeerId;
        //ChannelId = channelId;
        ExcludeAuthKeyId = excludeAuthKeyId;
        ExcludeUserId = excludeUserId;
        OnlySendToUserId = onlySendToUserId;
        OnlySendToThisAuthKeyId = onlySendToThisAuthKeyId;
        UpdatesType = updatesType;
        Pts = pts;
        MessageId = messageId;
        Date = date;
        GlobalSeqNo = globalSeqNo;
        Updates = updates;
        Users = users;
        Chats = chats;
    }

    public long OwnerPeerId { get; }
    //public long ChannelId { get; }
    public long? ExcludeAuthKeyId { get; }
    public long? ExcludeUserId { get; }
    public long? OnlySendToUserId { get; }
    public long? OnlySendToThisAuthKeyId { get; }
    public UpdatesType UpdatesType { get; }
    public int Pts { get; }
    public int? MessageId { get; }
    public int Date { get; }
    public long GlobalSeqNo { get; }
    public IList<IUpdate>? Updates { get; }
    public List<long>? Users { get; }
    public List<long>? Chats { get; }
}