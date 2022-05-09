namespace MyTelegram.Domain.Sagas.Events;

public class InviteToChannelCompletedEvent : RequestAggregateEvent<InviteToChannelSaga, InviteToChannelSagaId>,
    IHasCorrelationId
{
    public InviteToChannelCompletedEvent(long reqMsgId,
        long channelId,
        long inviterId,
        IReadOnlyList<long> memberUidList,
        Guid correlationId) : base(reqMsgId)
    {
        ChannelId = channelId;
        InviterId = inviterId;
        MemberUidList = memberUidList;
        CorrelationId = correlationId;
    }

    public long ChannelId { get; }
    public long InviterId { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public Guid CorrelationId { get; }
}
