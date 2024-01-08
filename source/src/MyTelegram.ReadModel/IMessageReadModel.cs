namespace MyTelegram.ReadModel;

public interface IMessageReadModel : IReadModel, IHasReactions
{
    int Date { get; }
    int EditDate { get; }
    bool EditHide { get; }
    byte[]? Entities { get; }
    MessageFwdHeader? FwdHeader { get; }
    long? GroupedId { get; }
    string Id { get; }

    byte[]? Media { get; }
    string Message { get; }
    string? MessageActionData { get; }
    MessageActionType MessageActionType { get; }
    MessageType MessageType { get; }
    // int MessageId { get; }
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
    //bool Mentioned { get; }
    //List<UserReaction>? UserReactions { get; }
    //List<long>? ReactionUserIds { get; }
    //List<ReactionCount>? Reactions { get; }
    //List<Reaction>? RecentReactions { get; }
    IInputReplyTo? ReplyTo { get; }
}