// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IBroadcastStats : IObject
{
    Schema.IStatsDateRangeDays Period { get; set; }
    Schema.IStatsAbsValueAndPrev Followers { get; set; }
    Schema.IStatsAbsValueAndPrev ViewsPerPost { get; set; }
    Schema.IStatsAbsValueAndPrev SharesPerPost { get; set; }
    Schema.IStatsPercentValue EnabledNotifications { get; set; }
    Schema.IStatsGraph GrowthGraph { get; set; }
    Schema.IStatsGraph FollowersGraph { get; set; }
    Schema.IStatsGraph MuteGraph { get; set; }
    Schema.IStatsGraph TopHoursGraph { get; set; }
    Schema.IStatsGraph InteractionsGraph { get; set; }
    Schema.IStatsGraph IvInteractionsGraph { get; set; }
    Schema.IStatsGraph ViewsBySourceGraph { get; set; }
    Schema.IStatsGraph NewFollowersBySourceGraph { get; set; }
    Schema.IStatsGraph LanguagesGraph { get; set; }
    TVector<Schema.IMessageInteractionCounters> RecentMessageInteractions { get; set; }
}
