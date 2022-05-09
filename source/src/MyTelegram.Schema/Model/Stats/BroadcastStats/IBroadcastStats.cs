// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IBroadcastStats : IObject
{
    MyTelegram.Schema.IStatsDateRangeDays Period { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev Followers { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev ViewsPerPost { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev SharesPerPost { get; set; }
    MyTelegram.Schema.IStatsPercentValue EnabledNotifications { get; set; }
    MyTelegram.Schema.IStatsGraph GrowthGraph { get; set; }
    MyTelegram.Schema.IStatsGraph FollowersGraph { get; set; }
    MyTelegram.Schema.IStatsGraph MuteGraph { get; set; }
    MyTelegram.Schema.IStatsGraph TopHoursGraph { get; set; }
    MyTelegram.Schema.IStatsGraph InteractionsGraph { get; set; }
    MyTelegram.Schema.IStatsGraph IvInteractionsGraph { get; set; }
    MyTelegram.Schema.IStatsGraph ViewsBySourceGraph { get; set; }
    MyTelegram.Schema.IStatsGraph NewFollowersBySourceGraph { get; set; }
    MyTelegram.Schema.IStatsGraph LanguagesGraph { get; set; }
    TVector<MyTelegram.Schema.IMessageInteractionCounters> RecentMessageInteractions { get; set; }

}
