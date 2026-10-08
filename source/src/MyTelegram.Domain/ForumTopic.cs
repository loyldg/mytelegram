namespace MyTelegram.Domain;

public record ForumTopic(/*long ChannelId,*/
    string Title,
    int? IconColor,
    long? IconEmojiId,
    long RandomId,
    Peer? SendAs);