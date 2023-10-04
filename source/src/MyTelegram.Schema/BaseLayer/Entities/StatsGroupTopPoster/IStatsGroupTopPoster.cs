// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Most active user in a <a href="https://corefork.telegram.org/api/channel">supergroup</a>
/// See <a href="https://corefork.telegram.org/constructor/StatsGroupTopPoster" />
///</summary>
public interface IStatsGroupTopPoster : IObject
{
    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Number of messages for <a href="https://corefork.telegram.org/api/stats">statistics</a> period in consideration
    ///</summary>
    int Messages { get; set; }

    ///<summary>
    /// Average number of characters per message
    ///</summary>
    int AvgChars { get; set; }
}
