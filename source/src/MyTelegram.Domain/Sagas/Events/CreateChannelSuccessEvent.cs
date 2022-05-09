namespace MyTelegram.Domain.Sagas.Events;

public class CreateChannelSuccessEvent : AggregateEvent<CreateChannelSaga, CreateChannelSagaId>
{
    public CreateChannelSuccessEvent(long reqMsgId,
        long selfAuthKeyId,
        long channelId,
        long creatorUid,
        int date,
        string messageActionData,
        long randomId
    )
    {
        ChannelId = channelId;
        CreatorUid = creatorUid;
        Date = date;
        MessageActionData = messageActionData;
        RandomId = randomId;
        ReqMsgId = reqMsgId;
        SelfAuthKeyId = selfAuthKeyId;
    }

    public long ChannelId { get; }
    public long CreatorUid { get; }
    public int Date { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public long ReqMsgId { get; }
    public long SelfAuthKeyId { get; }
}
