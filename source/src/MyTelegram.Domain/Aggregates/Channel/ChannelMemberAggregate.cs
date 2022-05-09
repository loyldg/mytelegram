namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelMemberAggregate : AggregateRoot<ChannelMemberAggregate, ChannelMemberId>
{
    private readonly ChannelMemberState _state = new();

    public ChannelMemberAggregate(ChannelMemberId id) : base(id)
    {
        Register(_state);
    }

    public void Create(
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isBot,
        Guid correlationId)
    {
        // Kicked user can not join channel by invite link
        if (_state.KickedBy != 0 && userId == inviterId)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChannelPrivate);
        }

        if (!IsNew && !_state.Left)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserAlreadyParticipant);
        }

        Emit(new ChannelMemberCreatedEvent(
            channelId,
            userId,
            inviterId,
            date,
            !IsNew,
            _state.BannedRights,
            isBot,
            correlationId));
    }

    public void CreateCreator(long reqMsgId,
        long channelId,
        long userId,
        int date)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelCreatorCreatedEvent(reqMsgId,
            channelId,
            userId,
            userId,
            date));
    }

    public void EditBanned(long reqMsgId,
        long adminId,
        long channelId,
        long memberUid,
        ChatBannedRights bannedRights)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var needRemoveFromKicked = false;
        var needRemoveFromBanned = false;

        if (_state.BannedRights != null)
        {
            if (_state.BannedRights.ViewMessages && !bannedRights.ViewMessages)
            {
                needRemoveFromKicked = true;
            }
            else if (bannedRights.ToIntValue() == ChatBannedRights.Default.ToIntValue())
            {
                needRemoveFromBanned = true;
            }
        }

        Emit(new ChannelMemberBannedRightsChangedEvent(reqMsgId,
            adminId,
            channelId,
            memberUid,
            needRemoveFromKicked,
            needRemoveFromBanned,
            bannedRights));
    }

    public void Join(long reqMsgId,
        long channelId,
        long memberUid,
        Guid correlationId)
    {
        if (_state.KickedBy != 0)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChannelPrivate);
        }

        if (!IsNew && !_state.Kicked)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserAlreadyParticipant);
        }

        Emit(new ChannelMemberJoinedEvent(reqMsgId,
            channelId,
            memberUid,
            DateTime.UtcNow.ToTimestamp(),
            !IsNew,
            correlationId));
    }

    public void LeaveChannel(long reqMsgId,
        long channelId,
        long memberUid)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMemberLeftEvent(reqMsgId, channelId, memberUid));
    }
}
