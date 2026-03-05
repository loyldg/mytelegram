namespace MyTelegram.ReadModel.Interfaces;

public interface IMessageReadModel : IReadModel, IReactionItem
{
    int Date { get; }
    int? EditDate { get; }
    bool EditHide { get; }
    byte[]? Entities { get; }
    TVector<IMessageEntity>? Entities2 { get; }
    MessageFwdHeader? FwdHeader { get; }
    long? GroupedId { get; }
    string Id { get; }
    byte[]? Media { get; }
    IMessageMedia? Media2 { get; }
    string Message { get; }
    string? MessageActionData { get; }
    IMessageAction? MessageAction { get; }
    MessageActionType MessageActionType { get; }
    MessageType MessageType { get; }
    bool Out { get; }
    long OwnerPeerId { get; }
    bool Pinned { get; }
    bool Post { get; }
    string? PostAuthor { get; }
    int Pts { get; }
    int? ReplyToMsgId { get; }
    int? TopMsgId { get; }
    int SenderMessageId { get; }
    long SenderPeerId { get; }
    long SenderUserId { get; }
    SendMessageType SendMessageType { get; }
    bool Silent { get; }
    long ToPeerId { get; }
    PeerType ToPeerType { get; }

    Peer? SavedPeerId { get; }
    int? Views { get; }
    long? LinkedChannelId { get; }
    int Replies { get; }
    long? PollId { get; }
    byte[]? ReplyMarkup { get; }
    IReplyMarkup? ReplyMarkup2 { get; }
    //IInputReplyTo? ReplyTo { get; }
    Peer? SendAs { get; }
    MessageReply? Reply { get; }
    long? PostChannelId { get; }
    int? PostMessageId { get; }
    bool IsQuickReplyMessage { get; }
    int? ShortcutId { get; }
    QuickReplyItem? QuickReplyItem { get; }
    Guid BatchId { get; }
    long? Effect { get; }
    bool FromScheduled { get; }
    int? ScheduleDate { get; }
    int? TtlPeriod { get; }
    int? ExpirationTime { get; }
    bool InvertMedia { get; }
    bool PublicPosts { get; }
    List<string> Hashtags { get; }
    List<long>? MentionedUserIds { get; }
    long? TodoId { get; }
    ReadOnlyMemory<byte>? EncryptedData { get; }
}