namespace MyTelegram;

public enum AccessHashType
{
    User,
    Channel,
    Document,
    Photo,
    StickerSet,
    Sticker,
}

public record UserReaction(long UserId,
    List<long> ReactionIds);