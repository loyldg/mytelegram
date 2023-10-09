namespace MyTelegram.Domain.Commands.UserName;

public class SetUserNameCommand : RequestCommand2<UserNameAggregate, UserNameId, IExecutionResult>
{
    public SetUserNameCommand(UserNameId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        PeerType peerType,
        long peerId,
        string userName) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        PeerType = peerType;
        PeerId = peerId;
        UserName = userName;
    }

    public long PeerId { get; }
    public PeerType PeerType { get; }
    public long SelfUserId { get; }
    public string UserName { get; }
}