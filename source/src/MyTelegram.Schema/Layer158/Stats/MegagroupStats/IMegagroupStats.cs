// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IMegagroupStats : IObject
{
    Schema.IStatsDateRangeDays Period { get; set; }
    Schema.IStatsAbsValueAndPrev Members { get; set; }
    Schema.IStatsAbsValueAndPrev Messages { get; set; }
    Schema.IStatsAbsValueAndPrev Viewers { get; set; }
    Schema.IStatsAbsValueAndPrev Posters { get; set; }
    Schema.IStatsGraph GrowthGraph { get; set; }
    Schema.IStatsGraph MembersGraph { get; set; }
    Schema.IStatsGraph NewMembersBySourceGraph { get; set; }
    Schema.IStatsGraph LanguagesGraph { get; set; }
    Schema.IStatsGraph MessagesGraph { get; set; }
    Schema.IStatsGraph ActionsGraph { get; set; }
    Schema.IStatsGraph TopHoursGraph { get; set; }
    Schema.IStatsGraph WeekdaysGraph { get; set; }
    TVector<Schema.IStatsGroupTopPoster> TopPosters { get; set; }
    TVector<Schema.IStatsGroupTopAdmin> TopAdmins { get; set; }
    TVector<Schema.IStatsGroupTopInviter> TopInviters { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
