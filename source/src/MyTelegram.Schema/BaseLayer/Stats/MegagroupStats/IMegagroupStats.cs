// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// Supergroup statistics
/// See <a href="https://corefork.telegram.org/constructor/stats.MegagroupStats" />
///</summary>
[JsonDerivedType(typeof(TMegagroupStats), nameof(TMegagroupStats))]
public interface IMegagroupStats : IObject
{
    ///<summary>
    /// Period in consideration
    /// See <a href="https://corefork.telegram.org/type/StatsDateRangeDays" />
    ///</summary>
    MyTelegram.Schema.IStatsDateRangeDays Period { get; set; }

    ///<summary>
    /// Member count change for period in consideration
    /// See <a href="https://corefork.telegram.org/type/StatsAbsValueAndPrev" />
    ///</summary>
    MyTelegram.Schema.IStatsAbsValueAndPrev Members { get; set; }

    ///<summary>
    /// Message number change for period in consideration
    /// See <a href="https://corefork.telegram.org/type/StatsAbsValueAndPrev" />
    ///</summary>
    MyTelegram.Schema.IStatsAbsValueAndPrev Messages { get; set; }

    ///<summary>
    /// Number of users that viewed messages, for range in consideration
    /// See <a href="https://corefork.telegram.org/type/StatsAbsValueAndPrev" />
    ///</summary>
    MyTelegram.Schema.IStatsAbsValueAndPrev Viewers { get; set; }

    ///<summary>
    /// Number of users that posted messages, for range in consideration
    /// See <a href="https://corefork.telegram.org/type/StatsAbsValueAndPrev" />
    ///</summary>
    MyTelegram.Schema.IStatsAbsValueAndPrev Posters { get; set; }

    ///<summary>
    /// Supergroup growth graph (absolute subscriber count)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph GrowthGraph { get; set; }

    ///<summary>
    /// Members growth (relative subscriber count)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph MembersGraph { get; set; }

    ///<summary>
    /// New members by source graph
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph NewMembersBySourceGraph { get; set; }

    ///<summary>
    /// Subscriber language graph (pie chart)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph LanguagesGraph { get; set; }

    ///<summary>
    /// Message activity graph (stacked bar graph, message type)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph MessagesGraph { get; set; }

    ///<summary>
    /// Group activity graph (deleted, modified messages, blocked users)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph ActionsGraph { get; set; }

    ///<summary>
    /// Activity per hour graph (absolute)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph TopHoursGraph { get; set; }

    ///<summary>
    /// Activity per day of week graph (absolute)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph WeekdaysGraph { get; set; }

    ///<summary>
    /// Info about most active group members
    /// See <a href="https://corefork.telegram.org/type/StatsGroupTopPoster" />
    ///</summary>
    TVector<MyTelegram.Schema.IStatsGroupTopPoster> TopPosters { get; set; }

    ///<summary>
    /// Info about most active group admins
    /// See <a href="https://corefork.telegram.org/type/StatsGroupTopAdmin" />
    ///</summary>
    TVector<MyTelegram.Schema.IStatsGroupTopAdmin> TopAdmins { get; set; }

    ///<summary>
    /// Info about most active group inviters
    /// See <a href="https://corefork.telegram.org/type/StatsGroupTopInviter" />
    ///</summary>
    TVector<MyTelegram.Schema.IStatsGroupTopInviter> TopInviters { get; set; }

    ///<summary>
    /// Info about users mentioned in statistics
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
