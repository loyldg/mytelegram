namespace MyTelegram.ReadModel.Impl;

public class ChatAdminReadModel : IChatAdminReadModel,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    IAmReadModelFor<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatAdminRightsEditedEvent>
{
    public virtual string Id { get; private set; } = null!;
    //public long ChannelId { get; private set; }
    public long PeerId { get; private set; }
    public long PromotedBy { get; private set; }
    public bool CanEdit { get; private set; }
    public long UserId { get; private set; }
    public bool IsBot { get; private set; }
    public string? Rank { get; private set; }
    public ChatAdminRights AdminRights { get; private set; } = default!;
    public int Date { get; private set; }
    public bool IsCreator { get; private set; }

    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = AdminId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId).Value;

        if (domainEvent.AggregateEvent.RemoveAdminFromList)
        {
            context.MarkForDeletion();
        }
        else
        {
            if (domainEvent.AggregateEvent.IsNewAdmin)
            {
                //ChannelId = domainEvent.AggregateEvent.ChannelId;
                PeerId = domainEvent.AggregateEvent.ChannelId;
                PromotedBy = domainEvent.AggregateEvent.PromotedBy;
                UserId = domainEvent.AggregateEvent.UserId;
                IsBot = domainEvent.AggregateEvent.IsBot;

                Date = domainEvent.AggregateEvent.Date;
            }

            CanEdit = domainEvent.AggregateEvent.CanEdit;
            Rank = domainEvent.AggregateEvent.Rank;
            AdminRights = domainEvent.AggregateEvent.AdminRights;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = AdminId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CreatorId).Value;

        //ChannelId = domainEvent.AggregateEvent.ChannelId;
        PeerId = domainEvent.AggregateEvent.ChannelId;

        PromotedBy = domainEvent.AggregateEvent.CreatorId;
        UserId = domainEvent.AggregateEvent.CreatorId;
        IsBot = false;
        CanEdit = true;
        Rank = null;
        IsCreator = true;
        AdminRights = ChatAdminRights.GetCreatorRights();

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<ChatAggregate, ChatId, ChatAdminRightsEditedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = AdminId.Create(domainEvent.AggregateEvent.ChatId, domainEvent.AggregateEvent.UserId).Value;

        if (domainEvent.AggregateEvent.RemoveAdminFromList)
        {
            context.MarkForDeletion();
        }
        else
        {
            if (domainEvent.AggregateEvent.IsNewAdmin)
            {
                //ChannelId = domainEvent.AggregateEvent.ChannelId;
                PeerId = domainEvent.AggregateEvent.ChatId;
                PromotedBy = domainEvent.AggregateEvent.PromotedBy;
                UserId = domainEvent.AggregateEvent.UserId;
                IsBot = domainEvent.AggregateEvent.IsBot;

                Date = domainEvent.AggregateEvent.Date;
            }

            CanEdit = domainEvent.AggregateEvent.CanEdit;
            Rank = domainEvent.AggregateEvent.Rank;
            AdminRights = domainEvent.AggregateEvent.AdminRights;
        }

        return Task.CompletedTask;
    }
}