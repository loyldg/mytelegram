namespace MyTelegram.Domain.Aggregates.User;

public class UserSnapshot : ISnapshot
{
    public UserSnapshot(long userId,
        bool isOnline,
        long accessHash,
        string firstName,
        string? lastName,
        string phoneNumber,
        string? userName,
        bool hasPassword,
        byte[]? photo,
        bool isBot,
        bool isDeleted,
        long? emojiStatusDocumentId,
        int? emojiStatusValidUntil,
        List<long> recentEmojiStatuses,
        long? photoId,
        long? fallbackPhotoId,
        PeerColor? color,
        PeerColor? profileColor,
        GlobalPrivacySettings globalPrivacySettings,
        bool premium
        )
    {
        UserId = userId;
        IsOnline = isOnline;
        AccessHash = accessHash;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        UserName = userName;
        HasPassword = hasPassword;
        Photo = photo;
        IsBot = isBot;
        IsDeleted = isDeleted;
        EmojiStatusDocumentId = emojiStatusDocumentId;
        EmojiStatusValidUntil = emojiStatusValidUntil;
        RecentEmojiStatuses = recentEmojiStatuses;
        PhotoId = photoId;
        FallbackPhotoId = fallbackPhotoId;
        Color = color;
        ProfileColor = profileColor;
        GlobalPrivacySettings = globalPrivacySettings;
        Premium = premium;
    }

    public long AccessHash { get; }
    public string FirstName { get; }
    public bool HasPassword { get; }
    public bool IsOnline { get; }

    public string? LastName { get; }

    //public string UserName { get;private set; }
    public string PhoneNumber { get; }
    public byte[]? Photo { get; }
    public bool IsBot { get; }
    public bool IsDeleted { get; }
    public long? EmojiStatusDocumentId { get; }
    public int? EmojiStatusValidUntil { get; }
    public List<long> RecentEmojiStatuses { get; }
    public long? PhotoId { get; }
    public long? FallbackPhotoId { get; }
    public PeerColor? Color { get; }
    public PeerColor? ProfileColor { get; }
    public GlobalPrivacySettings GlobalPrivacySettings { get; }
    public bool Premium { get; }

    public long UserId { get; }
    public string? UserName { get; }
}
