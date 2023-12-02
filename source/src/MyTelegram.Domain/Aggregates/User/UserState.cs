#pragma warning disable CS8618

namespace MyTelegram.Domain.Aggregates.User;

public class UserState : AggregateState<UserAggregate, UserId, UserState>,
    IApply<UserCreatedEvent>,
    IApply<UserProfileUpdatedEvent>,
    IApply<CheckUserStatusCompletedEvent>,
    IApply<UserSupportHasSetEvent>,
    IApply<UserVerifiedHasSetEvent>,
    IApply<UserNameUpdatedEvent>,
    IApply<UserProfilePhotoChangedEvent>,
    IApply<CheckUserStateCompletedEvent>,
    IApply<UserProfilePhotoUploadedEvent>
{
    public long AccessHash { get; private set; }
    public string FirstName { get; private set; } = default!;
    public bool HasPassword { get; private set; }
    public bool IsOnline { get; private set; }
    public string? LastName { get; private set; }
    public string PhoneNumber { get; private set; } = default!;
    public byte[]? Photo { get; private set; }
    public bool SensitiveEnabled { get; private set; }
    public bool Support { get; private set; }
    public long UserId { get; private set; }
    public string? UserName { get; private set; }
    public bool Verified { get; private set; }
    public bool IsBot { get; private set; }
    public bool IsDeleted { get; private set; }
    //public bool IsPremium { get; private set; }
    public long? EmojiStatusDocumentId { get; private set; }
    public int? EmojiStatusValidUntil { get; private set; }
    public long? PhotoId { get; private set; }
    public long? FallbackPhotoId { get; private set; }
    public CircularBuffer<long> RecentEmojiStatus { get; private set; } = new(10);
    public int Color { get; private set; }
    public long? BackgroundEmojiId { get; private set; }
    public void Apply(CheckUserStatusCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(UserCreatedEvent aggregateEvent)
    {
        UserId = aggregateEvent.UserId;
        PhoneNumber = aggregateEvent.PhoneNumber;
        FirstName = aggregateEvent.FirstName;
        LastName = aggregateEvent.LastName;
        SensitiveEnabled = true;
        AccessHash = aggregateEvent.AccessHash;
        IsBot = aggregateEvent.Bot;
        UserName = aggregateEvent.UserName;
    }

    public void Apply(UserNameUpdatedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserItem.UserName;
    }

    public void Apply(UserProfilePhotoChangedEvent aggregateEvent)
    {
        //Photo = aggregateEvent.UserItem.ProfilePhoto;
        if (aggregateEvent.Fallback)
        {
            FallbackPhotoId = aggregateEvent.PhotoId;
        }
        else
        {
            PhotoId = aggregateEvent.PhotoId;
        }
    }

    public void Apply(UserProfileUpdatedEvent aggregateEvent)
    {
        if (!string.IsNullOrEmpty(aggregateEvent.FirstName))
        {
            FirstName = aggregateEvent.FirstName;
        }

        LastName = aggregateEvent.LastName;
    }

    public void Apply(UserSupportHasSetEvent aggregateEvent)
    {
        Support = aggregateEvent.Support;
    }

    public void Apply(UserVerifiedHasSetEvent aggregateEvent)
    {
        Verified = aggregateEvent.Verified;
    }
    public void Apply(CheckUserStateCompletedEvent aggregateEvent)
    {
    }

    public void LoadFromSnapshot(UserSnapshot snapshot)
    {
        UserId = snapshot.UserId;
        IsOnline = snapshot.IsOnline;

        AccessHash = snapshot.AccessHash;
        FirstName = snapshot.FirstName;
        LastName = snapshot.LastName;
        PhoneNumber = snapshot.PhoneNumber;
        HasPassword = snapshot.HasPassword;
        UserName = snapshot.UserName;
        Photo = snapshot.Photo;
        IsBot = snapshot.IsBot;
        IsDeleted = snapshot.IsDeleted;

        EmojiStatusDocumentId = snapshot.EmojiStatusDocumentId;
        EmojiStatusValidUntil = snapshot.EmojiStatusValidUntil;
        RecentEmojiStatus = new CircularBuffer<long>(10, snapshot.RecentEmojiStatuses.ToArray());

        PhotoId = snapshot.PhotoId;
        FallbackPhotoId = snapshot.FallbackPhotoId;
    }


    public void Apply(UserProfilePhotoUploadedEvent aggregateEvent)
    {
        PhotoId = aggregateEvent.PhotoId;
    }
}
