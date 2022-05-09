namespace MyTelegram.Domain.Commands.UserName;

public class SetUserNameCommand : RequestCommand<UserNameAggregate, UserNameId, IExecutionResult>, IHasCorrelationId
{
    public SetUserNameCommand(UserNameId aggregateId,
        long reqMsgId,
        long selfUserId,
        PeerType peerType,
        long peerId,
        string userName,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        PeerType = peerType;
        PeerId = peerId;
        UserName = userName;
        CorrelationId = correlationId;
    }

    public long PeerId { get; }
    public PeerType PeerType { get; }
    public long SelfUserId { get; }
    public string UserName { get; }

    public Guid CorrelationId { get; }
}
