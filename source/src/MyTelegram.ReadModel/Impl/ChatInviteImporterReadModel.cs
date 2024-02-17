namespace MyTelegram.ReadModel.Impl;

public class ChatInviteImporterReadModel : IChatInviteImporterReadModel,
        IAmReadModelFor<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent>,
        IAmReadModelFor<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent>
{
    public string Id { get; private set; } = default!;
    public long PeerId { get; private set; }
    public long InviteId { get; private set; }
    public long UserId { get; private set; }
    public ChatInviteRequestState ChatInviteRequestState { get; private set; }
    //public bool RequestNeeded { get; private set; }
    public bool Approved { get; private set; }
    public long? ApprovedBy { get; private set; }
    public int Date { get; private set; }
    public string? About { get; private set; }
    public bool ViaChatList { get; private set; }
    public long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = ChatInviteImporterId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.RequestInfo.UserId).Value;
        PeerId = domainEvent.AggregateEvent.ChannelId;
        InviteId = domainEvent.AggregateEvent.InviteId;
        UserId = domainEvent.AggregateEvent.RequestInfo.UserId;
        ChatInviteRequestState = domainEvent.AggregateEvent.ChatInviteRequestState;
        Date = domainEvent.AggregateEvent.Date;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = ChatInviteImporterId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId).Value;
        Approved=domainEvent.AggregateEvent.Approved;

        ChatInviteRequestState = domainEvent.AggregateEvent.Approved
            ? ChatInviteRequestState.Approved
            : ChatInviteRequestState.Rejected;

        return Task.CompletedTask;
    }
}