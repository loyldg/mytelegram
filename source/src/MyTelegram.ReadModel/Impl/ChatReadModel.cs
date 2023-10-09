using MyTelegram.Domain.ValueObjects;

namespace MyTelegram.ReadModel.Impl;

public class ChatReadModel : IChatReadModel,
    IAmReadModelFor<ChatAggregate, ChatId, ChatCreatedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatMemberAddedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatMemberDeletedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatPhotoEditedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatAboutEditedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatTitleEditedEvent>,
    IAmReadModelFor<ChatAggregate, ChatId, ChatDeletedEvent>

{
    public virtual string? About { get; private set; }

    public virtual long ChatId { get; private set; }

    public virtual List<ChatMember> ChatMembers { get; protected set; } = new()!;

    public virtual long CreatorUid { get; private set; }

    public virtual int Date { get; private set; }

    public virtual ChatBannedRights? DefaultBannedRights { get; protected set; }

    public virtual string Id { get; private set; } = null!;
    public long? PhotoId { get; private set; }

    //public virtual byte[]? Photo { get; private set; }

    public virtual int PinnedMsgId { get; private set; }

    public virtual string Title { get; private set; } = null!;

    public virtual long? Version { get; set; }
    public bool IsDeleted { get; set; }
    //public ReactionType ReactionType { get; private set; }
    public bool AllowCustomReaction { get; private set; }
    public List<string>? AvailableReactions { get; private set; }
    public int? TtlPeriod { get; private set; }
    public long? MigrateToChannelId { get; private set; }
    public long? MigrateToChannelAccessHash { get; private set; }
    public bool Deactivated { get; private set; }
    public bool NoForwards { get; private set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatAboutEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        About = domainEvent.AggregateEvent.About;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ChatId = domainEvent.AggregateEvent.ChatId;
        CreatorUid = domainEvent.AggregateEvent.CreatorUid;
        Title = domainEvent.AggregateEvent.Title;
        Date = domainEvent.AggregateEvent.Date;
        //ChatMembers = domainEvent.AggregateEvent.MemberUidList;
        //ChatMembers.AddRange(domainEvent.AggregateEvent.MemberUidList);
        ChatMembers = new List<ChatMember>(domainEvent.AggregateEvent.MemberUidList);
        PinnedMsgId = 0;
        TtlPeriod = domainEvent.AggregateEvent.TtlPeriod;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        DefaultBannedRights = domainEvent.AggregateEvent.DefaultBannedRights;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ChatMembers.Add(domainEvent.AggregateEvent.ChatMember);
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ChatMembers.RemoveAll(p => p.UserId == domainEvent.AggregateEvent.UserId);
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatPhotoEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        //Photo = domainEvent.AggregateEvent.Photo;
        PhotoId = domainEvent.AggregateEvent.PhotoId;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatTitleEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Title = domainEvent.AggregateEvent.Title;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<ChatAggregate, ChatId, ChatDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        IsDeleted = true;

        return Task.CompletedTask;
    }
}