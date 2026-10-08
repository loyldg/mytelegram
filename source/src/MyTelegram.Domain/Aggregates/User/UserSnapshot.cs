namespace MyTelegram.Domain.Aggregates.User;

public class UserSnapshot(
    long userId,
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
    GlobalPrivacySettings? globalPrivacySettings,
    bool premium,
    long? personalChannelId,
    Birthday? birthday,
    int? profilePhotoUpdateDate,
    int? userNameUpdateDate
    )
    : ISnapshot
{
    public long AccessHash { get; } = accessHash;
    public string FirstName { get; } = firstName;
    public bool HasPassword { get; } = hasPassword;
    public bool IsOnline { get; } = isOnline;

    public string? LastName { get; } = lastName;

    //public string UserName { get;private set; }
    public string PhoneNumber { get; } = phoneNumber;
    public byte[]? Photo { get; } = photo;
    public bool IsBot { get; } = isBot;
    public bool IsDeleted { get; } = isDeleted;
    public long? EmojiStatusDocumentId { get; } = emojiStatusDocumentId;
    public int? EmojiStatusValidUntil { get; } = emojiStatusValidUntil;
    public List<long> RecentEmojiStatuses { get; } = recentEmojiStatuses;
    public long? PhotoId { get; } = photoId;
    public long? FallbackPhotoId { get; } = fallbackPhotoId;
    public PeerColor? Color { get; } = color;
    public PeerColor? ProfileColor { get; } = profileColor;
    public GlobalPrivacySettings? GlobalPrivacySettings { get; } = globalPrivacySettings;
    public bool Premium { get; } = premium;
    public long? PersonalChannelId { get; } = personalChannelId;
    public Birthday? Birthday { get; } = birthday;
    public int? ProfilePhotoUpdateDate { get; } = profilePhotoUpdateDate;
    public int? UserNameUpdateDate { get; } = userNameUpdateDate;
    public long UserId { get; } = userId;
    public string? UserName { get; } = userName;
}
