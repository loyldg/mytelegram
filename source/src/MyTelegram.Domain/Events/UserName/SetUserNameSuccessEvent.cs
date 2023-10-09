namespace MyTelegram.Domain.Events.UserName;

public class SetUserNameSuccessEvent : RequestAggregateEvent2<UserNameAggregate, UserNameId>
{
    public SetUserNameSuccessEvent(RequestInfo requestInfo,
        long selfUserId,
        string userName,
        PeerType peerType,
        long peerId) : base(requestInfo)
    {
        SelfUserId = selfUserId;
        UserName = userName;
        PeerType = peerType;
        PeerId = peerId;

    }

    public long PeerId { get; }
    public PeerType PeerType { get; }
    public long SelfUserId { get; }
    public string UserName { get; }


}