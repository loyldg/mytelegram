namespace MyTelegram.Domain.Aggregates.Channel;

[EnableAutoGeneration]
public class ChannelMemberAggregate : SnapshotAggregateRoot<ChannelMemberAggregate, ChannelMemberId, ChannelMemberSnapshot>
{
    private readonly ChannelMemberState _state = new();

    public ChannelMemberAggregate(ChannelMemberId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void EditChannelAdmin2(RequestInfo requestInfo, long channelId, long userId, int adminRights, string rank)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var isAdmin = adminRights != 0;
        Emit(new ChannelAdminEditedEvent2(requestInfo, channelId, userId, adminRights, rank, isAdmin));
    }

    public void CreateChannelMember(
        RequestInfo requestInfo,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isBot,
        long? chatInviteId,
        bool isBroadcast,
        ChatJoinType chatJoinType = ChatJoinType.InvitedByAdmin
        )
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
        var isRejoin = !IsNew;
        Emit(new ChannelMemberCreatedEvent(
            requestInfo,
            channelId,
            userId,
            inviterId,
            date,
            isRejoin,
            _state.BannedRights,
            isBot,
            chatInviteId,
            chatJoinType,
            isBroadcast
            ));
    }

    public void CreateChannelCreator(RequestInfo requestInfo,
        long channelId,
        long userId,
        int date,
        bool isBroadcast
        )
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var inviterId = userId;
        Emit(new ChannelCreatorCreatedEvent(requestInfo,
            channelId,
            userId,
            inviterId,
            date,
            isBroadcast));
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
            kickedBy = adminId;
            left = false;
        }

        if (_state.BannedRights != null)
        {
            if (_state.BannedRights.ViewMessages && !bannedRights.ViewMessages)
            {
                removedFromKicked = true;
            }
            else if (bannedRights.ToIntValue() == ChatBannedRights.CreateDefaultBannedRights().ToIntValue())
            {
                removedFromBanned = true;
            }
        }

        var banned = bannedRights.ToIntValue() != ChatBannedRights.CreateDefaultBannedRights().ToIntValue();

        Emit(new ChannelMemberBannedRightsChangedEvent(requestInfo,
            adminId,
            channelId,
            memberUserId,
            _state.IsBot,
            kicked,
            kickedBy,
            left,
            banned,
            removedFromKicked,
            removedFromBanned,
            bannedRights));
    }

    public void LeaveChannel(RequestInfo requestInfo,
        long channelId,
        long memberUserId,
        bool notifyOtherParticipants
        )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMemberLeftEvent(requestInfo, channelId, memberUserId, _state.IsBot, _state.Broadcast, notifyOtherParticipants));
    }

    public void LeaveChannel2(RequestInfo requestInfo,
        long channelId,
        long memberUserId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMemberLeftEvent2(requestInfo, channelId, memberUserId, _state.IsBot, _state.Broadcast));
    }

    protected override Task<ChannelMemberSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChannelMemberSnapshot(_state.Banned, _state.BannedRights, _state.Kicked,
            _state.KickedBy, _state.Left, _state.IsBot, _state.Broadcast));
    }

    protected override Task LoadSnapshotAsync(ChannelMemberSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}
