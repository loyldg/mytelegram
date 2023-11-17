namespace MyTelegram.ReadModel.Impl;

public class UserReadModel : IUserReadModel,
    IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserProfileUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserSupportHasSetEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserVerifiedHasSetEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserNameUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserProfilePhotoChangedEvent>
{
    public virtual string? About { get; private set; }
    public virtual long AccessHash { get; private set; }
    public virtual int AccountTtl { get; private set; }
    public virtual bool Bot { get; private set; }
    public int? BotInfoVersion { get; private set; }
    public virtual string FirstName { get; private set; } = null!;
    public virtual bool HasPassword { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual bool IsOnline { get; private set; }
    public virtual string? LastName { get; private set; }
    public virtual DateTime LastUpdateDate { get; private set; }
    public virtual string PhoneNumber { get; private set; } = null!;
    public virtual int PinnedMsgId { get; private set; }
    public virtual List<int> PinnedMsgIdList { get; protected set; } = new();
    public virtual byte[]? ProfilePhoto { get; private set; }
    public virtual bool SensitiveCanChange { get; private set; }
    public virtual bool SensitiveEnabled { get; private set; }
    public virtual bool ShowContactSignUpNotification { get; private set; }
    public virtual bool Support { get; private set; }
    public virtual long UserId { get; private set; }
    //public string UserId { get; private set; }
    public virtual string? UserName { get; private set; }

    public virtual bool Verified { get; private set; }
    public bool Premium { get; private set; }
    public string? Email { get; private set; }
    public long? EmojiStatusDocumentId { get; private set; }
    public int? EmojiStatusValidUntil { get; private set; }
    public List<long> RecentEmojiStatuses { get; private set; }
    public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; private set; }
    public long? ProfilePhotoId { get; private set; }
    public long? PersonalPhotoId { get; private set; }
    public long? FallbackPhotoId { get; private set; }
    public int? Color { get; private set; }
    public long? BackgroundEmojiId { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.User)
        {
            UpdatePinnedMsgId(domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.Pinned);
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.User)
        {
            UpdatePinnedMsgId(domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.Pinned);
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        //UserId = domainEvent.AggregateIdentity;
        UserId = domainEvent.AggregateEvent.UserId;
        PhoneNumber = domainEvent.AggregateEvent.PhoneNumber;
        FirstName = domainEvent.AggregateEvent.FirstName;
        LastName = domainEvent.AggregateEvent.LastName;
        AccessHash = domainEvent.AggregateEvent.AccessHash;
        LastUpdateDate = DateTime.UtcNow;
        Bot = domainEvent.AggregateEvent.Bot;
        BotInfoVersion = domainEvent.AggregateEvent.BotInfoVersion;
        AccountTtl = domainEvent.AggregateEvent.AccountTtl;
        SensitiveCanChange = true;
        ShowContactSignUpNotification = false;
        UserName = domainEvent.AggregateEvent.UserName;

        Premium = true;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        UserName = domainEvent.AggregateEvent.UserItem.UserName;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ProfilePhotoId = domainEvent.AggregateEvent.PhotoId;
        //ProfilePhoto = domainEvent.AggregateEvent.UserItem.ProfilePhoto;
        //VideoEmojiMarkup = domainEvent.AggregateEvent.VideoEmojiMarkup;
        if (domainEvent.AggregateEvent.Fallback)
        {
            FallbackPhotoId = domainEvent.AggregateEvent.PhotoId;
        }
        else
        {
            ProfilePhotoId = domainEvent.AggregateEvent.PhotoId;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.FirstName))
        {
            FirstName = domainEvent.AggregateEvent.FirstName;
        }

        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.LastName))
        {
            LastName = domainEvent.AggregateEvent.LastName;
        }

        About = domainEvent.AggregateEvent.About;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserSupportHasSetEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Support = domainEvent.AggregateEvent.Support;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserVerifiedHasSetEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Verified = domainEvent.AggregateEvent.Verified;
        return Task.CompletedTask;
    }
    private void UpdatePinnedMsgId(int messageId,
        bool pinned)
    {
        if (pinned)
        {
            PinnedMsgId = messageId;
            PinnedMsgIdList.Add(messageId);
        }
        else
        {
            PinnedMsgIdList.Remove(messageId);
            PinnedMsgId = PinnedMsgIdList.LastOrDefault();
        }
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserProfilePhotoUploadedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Fallback)
        {
            FallbackPhotoId = domainEvent.AggregateEvent.PhotoId;
        }
        else
        {
            ProfilePhotoId = domainEvent.AggregateEvent.PhotoId;
        }

        return Task.CompletedTask;
    }
}
