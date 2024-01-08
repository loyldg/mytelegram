namespace MyTelegram;

public enum AccessHashType
{
    User,
    Channel,
    Document,
    Photo,
    StickerSet,
    Sticker,
    WallPaper
}

public record UserReaction(long UserId,
    List<long> ReactionIds);