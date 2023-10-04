// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Most active inviter in a <a href="https://corefork.telegram.org/api/channel">supergroup</a>
/// See <a href="https://corefork.telegram.org/constructor/StatsGroupTopInviter" />
///</summary>
public interface IStatsGroupTopInviter : IObject
{
    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Number of invitations for <a href="https://corefork.telegram.org/api/stats">statistics</a> period in consideration
    ///</summary>
    int Invitations { get; set; }
}
