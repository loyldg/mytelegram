namespace MyTelegram.Domain.Aggregates.ChatInvite;

public class
    ChatInviteAggregate : MyInMemorySnapshotAggregateRoot<ChatInviteAggregate, ChatInviteId, ChatInviteSnapshot>
{
    private readonly ChatInviteState _state = new();

    public ChatInviteAggregate(ChatInviteId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void CreateChatInvite(RequestInfo requestInfo, long channelId, long inviteId, string hash, long adminId, string? title,
        bool requestNeeded, int? startDate, int? expireDate, int? usageLimit, bool permanent, int date)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChatInviteCreatedEvent(requestInfo, channelId, inviteId, hash, adminId, title, requestNeeded, startDate,
            expireDate, usageLimit, permanent, date));
    }

    public void EditChatInvite(RequestInfo requestInfo, long inviteId, string hash, string? newHash, long adminId,
        string? title, bool requestNeeded, int? startDate, int? expireDate, int? usageLimit, bool permanent, bool revoked)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChatInviteEditedEvent(requestInfo, _state.ChannelId, inviteId, hash, newHash, adminId, title, requestNeeded,
            startDate, expireDate, usageLimit, permanent, revoked, _state.Requested, _state.Usage));
    }

    public void ImportChatInvite(RequestInfo requestInfo, ChatInviteRequestState chatInviteRequestState, int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.Revoked)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        if (_state.ExpireDate > 0)
        {
            var now = DateTime.UtcNow.ToTimestamp();
            if (_state.ExpireDate.Value < now)
            {
                RpcErrors.RpcErrors400.InviteHashExpired.ThrowRpcError();
            }
        }

        if (_state.UsageLimit > 0)
        {
            if (_state.Usage > _state.UsageLimit)
            {
                RpcErrors.RpcErrors400.UsersTooMuch.ThrowRpcError();
            }
        }

        var requested = _state.Requested;
        var usage = _state.Usage;
        if (chatInviteRequestState == ChatInviteRequestState.NeedApprove)
        {
            requested ??= 0;
            requested++;
        }
        else
        {
            usage ??= 0;
            usage++;
        }


        Emit(new ChatInviteImportedEvent(requestInfo, _state.ChannelId, _state.InviteId, chatInviteRequestState, requested, usage, _state.Hash, date));
    }

    public void DeleteExportedInvite(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChatInviteDeletedEvent(requestInfo, _state.ChannelId, _state.InviteId));
    }

    public void HideChatJoinRequest(RequestInfo requestInfo, long userId, bool approved)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
    }

    protected override Task<ChatInviteSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChatInviteSnapshot(
            _state.ChannelId,
            _state.InviteId,
            _state.Hash,
            _state.AdminId,
            _state.Title,
            _state.RequestNeeded,
            _state.StartDate,
            _state.ExpireDate,
            _state.UsageLimit,
            _state.Permanent,
            _state.Revoked,
            _state.Usage,
            _state.Requested
        ));
    }

    protected override Task LoadSnapshotAsync(ChatInviteSnapshot snapshot, ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}