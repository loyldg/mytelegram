namespace MyTelegram.Domain.Sagas.Events;

public class InviteToChannelCompletedEvent : RequestAggregateEvent2<InviteToChannelSaga, InviteToChannelSagaId>,
    IHasCorrelationId
{
    public InviteToChannelCompletedEvent(RequestInfo requestInfo,
        long channelId,
        long inviterId,
        bool broadcast,
        IReadOnlyList<long> memberUidList,
        IReadOnlyList<long>? privacyRestrictedUserId) : base(requestInfo)
    {
        ChannelId = channelId;
        InviterId = inviterId;
        Broadcast = broadcast;
        MemberUidList = memberUidList;
        PrivacyRestrictedUserId = privacyRestrictedUserId;
    }

    public long ChannelId { get; }
    public long InviterId { get; }
    public bool Broadcast { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public IReadOnlyList<long>? PrivacyRestrictedUserId { get; }
}