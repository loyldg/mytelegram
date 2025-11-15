namespace MyTelegram.Schema;

public interface IHasSubQuery
{
    IObject Query { get; set; }

    public uint GetObjectId()
    {
        if (Query is IHasSubQuery subQuery)
        {
            return subQuery.GetObjectId();
        }

        return Query.ConstructorId;
    }
}

public interface IHasAccessHash
{
    long Id { get; }
    long AccessHash { get; set; }
    AccessHashType2 AccessHashType2 { get; }
}

public enum AccessHashType2
{
    Unknown = 0,
    User = 1,
    Channel = 2,
    Document = 3,
    Photo = 4,
    StickerSet = 5,
    Sticker = 6,
    WallPaper = 7,
    Theme = 8,
    GroupCall = 9,
    BotApp = 10,
    Game = 11,
    Call = 12,
}

public interface IAccessHashOwner
{
    IEnumerable<IHasAccessHash> GetAccessHashes();
}