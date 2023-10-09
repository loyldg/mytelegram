namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelMemberAggregate : AggregateRoot<ChannelMemberAggregate, ChannelMemberId>
{
    private readonly ChannelMemberState _state = new();

    public ChannelMemberAggregate(ChannelMemberId id) : base(id)
    {
        Register(_state);
    }

    public void Create(
        RequestInfo requestInfo,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isBot,
        long? chatInviteId)
    {
        // Kicked user can not join channel by invite link
        if (_state.KickedBy != 0 && userId == inviterId)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.ChannelPrivate);
            RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
        }

        if (!IsNew && !_state.Left)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserAlreadyParticipant);
            RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
        }

        Emit(new ChannelMemberCreatedEvent(
            requestInfo,
            channelId,
            userId,
            inviterId,
            date,
            !IsNew,
            _state.BannedRights,
            isBot,
            chatInviteId));
    }

    public void CreateCreator(RequestInfo requestInfo,
        long channelId,
        long userId,
        int date)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelCreatorCreatedEvent(requestInfo,
            channelId,
            userId,
            userId,
            date));
    }

    public void EditBanned(RequestInfo requestInfo,
        long adminId,
        long channelId,
        long memberUserId,
        ChatBannedRights bannedRights)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        bool kicked;
        long kickedBy;
        bool left;
        bool removedFromKicked = false;
        bool removedFromBanned = false;
        // User is banned all rights
        if (bannedRights.ViewMessages)
        {
            kicked = true;
            kickedBy = adminId;
            left = true;
        }
        else
        {
            kicked = false;
            kickedBy = 0;
            left = false;
        }

        if (_state.BannedRights != null)
        {
            if (_state.BannedRights.ViewMessages && !bannedRights.ViewMessages)
            {
                removedFromKicked = true;
            }
            else if (bannedRights.ToIntValue() == ChatBannedRights.Default.ToIntValue())
            {
                removedFromBanned = true;
            }
        }

        var banned = bannedRights.ToIntValue() != ChatBannedRights.Default.ToIntValue();

        Emit(new ChannelMemberBannedRightsChangedEvent(requestInfo,
            adminId,
            channelId,
            memberUserId,
            kicked,
            kickedBy,
            left,
            banned,
            removedFromKicked,
            removedFromBanned,
            bannedRights));
    }

    public void Join(RequestInfo requestInfo,
        long channelId,
        long memberUserId)
    {
        if (_state.KickedBy != 0)
        {
            RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
        }

        if (!IsNew && !_state.Kicked)
        {
            RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
        }

        Emit(new ChannelMemberJoinedEvent(requestInfo,
            channelId,
            memberUserId,
            DateTime.UtcNow.ToTimestamp(),
            !IsNew));
    }

    public void LeaveChannel(RequestInfo requestInfo,
        long channelId,
        long memberUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMemberLeftEvent(requestInfo, channelId, memberUserId));
    }
}
