namespace MyTelegram.Domain.Events.UserName;

public class SetUserNameSuccessEvent : RequestAggregateEvent<UserNameAggregate, UserNameId>, IHasCorrelationId
{
    public SetUserNameSuccessEvent(long reqMsgId,
        long selfUserId,
        string userName,
        PeerType peerType,
        long peerId,
        Guid correlationId) : base(reqMsgId)
    {
        SelfUserId = selfUserId;
        UserName = userName;
        PeerType = peerType;
        PeerId = peerId;
        CorrelationId = correlationId;
    }

    public long PeerId { get; }
    public PeerType PeerType { get; }
    public long SelfUserId { get; }
    public string UserName { get; }

    public Guid CorrelationId { get; }
}
