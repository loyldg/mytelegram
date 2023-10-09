namespace MyTelegram.ReadModel;

public interface IChatReadModel : IReadModel
{
    string? About { get; }
    long ChatId { get; }
    List<ChatMember> ChatMembers { get; }
    long CreatorUid { get; }
    int Date { get; }
    ChatBannedRights? DefaultBannedRights { get; }
    string Id { get; }
    //byte[]? Photo { get; }
    long? PhotoId { get; }
    int PinnedMsgId { get; }
    string Title { get; }
    long? Version { get; set; }
    bool IsDeleted { get; set; }
    //ReactionType ReactionType { get; }
    bool AllowCustomReaction { get; }
    List<string>? AvailableReactions { get; }
    int? TtlPeriod { get; }
    long? MigrateToChannelId { get; }
    long? MigrateToChannelAccessHash { get; }
    bool Deactivated { get; }

    bool NoForwards { get; }
}