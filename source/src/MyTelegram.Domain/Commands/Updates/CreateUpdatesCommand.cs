using MyTelegram.Domain.Aggregates.Updates;

namespace MyTelegram.Domain.Commands.Updates;

public class CreateUpdatesCommand : Command<UpdatesAggregate, UpdatesId, IExecutionResult>
{
    public long OwnerPeerId { get; }
    //public long? ChannelId { get; }
    public long? ExcludeAuthKeyId { get; }
    public long? ExcludeUserId { get; }
    public long? OnlySendToUserId { get; }
    public long? OnlySendToThisAuthKeyId { get; }
    public UpdatesType UpdatesType { get; }
    public int Pts { get; }
    public int? MessageId { get; }
    public int Date { get; }
    public long SeqNo { get; }
    public byte[]? Updates { get; }
    public List<long>? Users { get; }
    public List<long>? Chats { get; }

    public CreateUpdatesCommand(UpdatesId aggregateId, /*long reqMsgId,*/
        long ownerPeerId,
        //long? channelId,
        long? excludeAuthKeyId, 
        long? excludeUserId, 
        long? onlySendToUserId,
        long? onlySendToThisAuthKeyId,
        UpdatesType updatesType, int pts, int? messageId, int date, long seqNo,
        byte[]? updates,
        List<long>? users, List<long>? chats) : base(aggregateId)
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
        SeqNo = seqNo;
        Updates = updates;
        Users = users;
        Chats = chats;
    }
     
}
