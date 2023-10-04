// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Most active admin in a <a href="https://corefork.telegram.org/api/channel">supergroup</a>
/// See <a href="https://corefork.telegram.org/constructor/StatsGroupTopAdmin" />
///</summary>
public interface IStatsGroupTopAdmin : IObject
{
    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Number of deleted messages for <a href="https://corefork.telegram.org/api/stats">statistics</a> period in consideration
    ///</summary>
    int Deleted { get; set; }

    ///<summary>
    /// Number of kicked users for <a href="https://corefork.telegram.org/api/stats">statistics</a> period in consideration
    ///</summary>
    int Kicked { get; set; }

    ///<summary>
    /// Number of banned users for <a href="https://corefork.telegram.org/api/stats">statistics</a> period in consideration
    ///</summary>
    int Banned { get; set; }
}
