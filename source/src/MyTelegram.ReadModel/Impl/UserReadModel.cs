using MyTelegram.Domain.Extensions;

namespace MyTelegram.ReadModel.Impl;

public class UserReadModel : IUserReadModel,
    IAmReadModelFor<UserAggregate, UserId, UserCreatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserProfileUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, MessagePinnedUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserSupportHasSetEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserVerifiedHasSetEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserNameUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserProfilePhotoChangedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserProfilePhotoUploadedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserColorUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserGlobalPrivacySettingsChangedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserPremiumStatusChangedEvent>,
    IAmReadModelFor<UserAggregate, UserId, PersonalChannelUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, BirthdayUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserAboutUpdatedEvent>,
    IAmReadModelFor<UserAggregate, UserId, UserFirstNameUpdatedEvent>
{
    public virtual string? About { get; private set; }
    public virtual long AccessHash { get; private set; }
    public virtual int AccountTtl { get; private set; }
    public Birthday? Birthday { get; private set; }
    public virtual bool Bot { get; private set; }
    public int? BotActiveUsers { get; private set; }
    public bool BotHasMainApp { get; private set; }
    public int? BotInfoVersion { get; private set; }
    public PeerColor? Color { get; private set; }
    public DateTime? CreationTime { get; private set; }
    public string? Email { get; private set; }
    public long? EmojiStatusDocumentId { get; private set; }
    public int? EmojiStatusValidUntil { get; private set; }
    public long? FallbackPhotoId { get; private set; }
    public virtual string FirstName { get; private set; } = null!;
    public GlobalPrivacySettings? GlobalPrivacySettings { get; private set; }
    public virtual bool HasPassword { get; private set; }
    public virtual string Id { get; set; } = null!;
    public virtual bool IsOnline { get; private set; }
    public virtual string? LastName { get; private set; }
    public virtual DateTime LastUpdateDate { get; private set; }
    public long? PersonalChannelId { get; private set; }
    public long? PersonalPhotoId { get; private set; }
    public virtual string PhoneNumber { get; private set; } = null!;
    public virtual int? PinnedMsgId { get; private set; }
    public virtual List<int> PinnedMsgIdList { get; protected set; } = [];
    public bool Premium { get; private set; }
    public PeerColor? ProfileColor { get; private set; }
    public virtual byte[]? ProfilePhoto { get; private set; }
    public long? ProfilePhotoId { get; private set; }
    public int? ProfilePhotoUpdateDate { get; private set; }
    public List<long> RecentEmojiStatuses { get; private set; } = [];
    public virtual bool SensitiveCanChange { get; private set; }
    public virtual bool SensitiveEnabled { get; private set; }
    public virtual bool ShowContactSignUpNotification { get; private set; }
    public virtual bool Support { get; private set; }
    public virtual long UserId { get; set; }
    //public string UserId { get; private set; }
    public virtual string? UserName { get; private set; }

    public List<string>? Usernames { get; private set; }
    public int? UserNameUpdateDate { get; private set; }
    public bool? IsDeleted { get; set; }
    public virtual bool Verified { get; private set; }

    //public int? Color { get; private set; }
    //public long? BackgroundEmojiId { get; private set; }
    public virtual long? Version { get; set; }

    public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; private set; }

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
        UserId = domainEvent.AggregateEvent.UserId;
        PhoneNumber = domainEvent.AggregateEvent.PhoneNumber;
        FirstName = domainEvent.AggregateEvent.FirstName;
        LastName = domainEvent.AggregateEvent.LastName;
        AccessHash = domainEvent.AggregateEvent.AccessHash;
        LastUpdateDate = domainEvent.AggregateEvent.CreationTime;
        Bot = domainEvent.AggregateEvent.Bot;
        AccountTtl = domainEvent.AggregateEvent.AccountTtl;
        SensitiveCanChange = true;
        ShowContactSignUpNotification = false;
        UserName = domainEvent.AggregateEvent.UserName;
        CreationTime = domainEvent.AggregateEvent.CreationTime;
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.UserName))
        {
            UserNameUpdateDate = domainEvent.AggregateEvent.CreationTime.ToTimestamp();
        }

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
        if (domainEvent.AggregateEvent.Fallback)
        {
            FallbackPhotoId = domainEvent.AggregateEvent.PhotoId;
        }
        else
        {
            ProfilePhotoId = domainEvent.AggregateEvent.PhotoId;
        }

        ProfilePhotoUpdateDate = domainEvent.AggregateEvent.Date;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        FirstName = domainEvent.AggregateEvent.FirstName;
        LastName = domainEvent.AggregateEvent.LastName;

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

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserColorUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.ForProfile)
        {
            ProfileColor = domainEvent.AggregateEvent.Color;
        }
        else
        {
            Color = domainEvent.AggregateEvent.Color;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserGlobalPrivacySettingsChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        GlobalPrivacySettings = domainEvent.AggregateEvent.GlobalPrivacySettings;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserPremiumStatusChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Premium = domainEvent.AggregateEvent.Premium;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, PersonalChannelUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        PersonalChannelId = domainEvent.AggregateEvent.PersonalChannelId;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, BirthdayUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Birthday = domainEvent.AggregateEvent.Birthday;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageAggregate, MessageId, MessagePinnedUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        UpdatePinnedMsgId(domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.Pinned);

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserAboutUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        About = domainEvent.AggregateEvent.About;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserAggregate, UserId, UserFirstNameUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        FirstName = domainEvent.AggregateEvent.FirstName;

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
}
