// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IMegagroupStats : IObject
{
    MyTelegram.Schema.IStatsDateRangeDays Period { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev Members { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev Messages { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev Viewers { get; set; }
    MyTelegram.Schema.IStatsAbsValueAndPrev Posters { get; set; }
    MyTelegram.Schema.IStatsGraph GrowthGraph { get; set; }
    MyTelegram.Schema.IStatsGraph MembersGraph { get; set; }
    MyTelegram.Schema.IStatsGraph NewMembersBySourceGraph { get; set; }
    MyTelegram.Schema.IStatsGraph LanguagesGraph { get; set; }
    MyTelegram.Schema.IStatsGraph MessagesGraph { get; set; }
    MyTelegram.Schema.IStatsGraph ActionsGraph { get; set; }
    MyTelegram.Schema.IStatsGraph TopHoursGraph { get; set; }
    MyTelegram.Schema.IStatsGraph WeekdaysGraph { get; set; }
    TVector<MyTelegram.Schema.IStatsGroupTopPoster> TopPosters { get; set; }
    TVector<MyTelegram.Schema.IStatsGroupTopAdmin> TopAdmins { get; set; }
    TVector<MyTelegram.Schema.IStatsGroupTopInviter> TopInviters { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
