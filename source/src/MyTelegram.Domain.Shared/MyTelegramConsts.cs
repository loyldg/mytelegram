// ReSharper disable once CheckNamespace

namespace MyTelegram;

public class MyTelegramConsts
{
    public const int Layer = Layers.LayerLatest;

    public const string RepositoryUrl = "https://github.com/loyldg/mytelegram";
    public const long UserIdInitId = 2000000;
    public const long BotUserInitId = 600000000000;
    public const long ChatIdInitId = 700000000000;
    public const long ChannelInitId = 800000000000;
    public const long BotFatherUserId = BotUserInitId;
    public const int FolderInitId = 1;

    public const int PtsInitId = 1;

    public const long OfficialUserId = 777000;
    public const long GroupAnonymousBotUserId = 568888;
    public const long AnonymousUserId = 2666000;
    public const long DefaultSupportUserId = 569999;

    public const long DeletedChannelIdForChannelPost = 777;

    public const string VideoMimeType = "video";
    public const int MediaDcId = 2;
    public const int QrCodeExpireSeconds = 30;
    public const int ChatAboutMaxLength = 255;

    public const int ChannelAdminMaxCount = 20;
    public const int UsernameMinLength = 5;
    public const int UsernameMaxLength = 32;
    public const int MaxRecentRepliersCount = 3;
    public const int MaxVoteOptions = 10;
    public const int ChatMemberMaxCount = 50;
    public const int ChannelBotMaxCount = 20;

    public const int AuthKeyExpireSeconds = 30;
    //public const int MaxAllowedJoinChannel

    public const int InternalErrorCode = 500;
    public const int BadRequestErrorCode = 400;
    public const string InternalErrorMessage = "Internal_Server_Error";

    public const int ClearHistoryDefaultPageSize = 500;
    public const int UnPinAllMessagesDefaultPageSize = 500;
    public const int DefaultPtsTotalLimit = 500;
    public const int MaxTimeDiffSeconds = 300;
    public const int LeftChannelUid = -1;
    public const int MaxBotCount = 20;
    public const int GzipMinLength = 512;

    public const int ReactionUniqueMax = 11;
    public const int RecentReactionMaxCount = 10;
    public const int ChatInviteRecentRequesterMaxCount = 10;
    public const int ChatReadMarkExpirePeriod = 604800;

    public const string BlockedCuckooFilterKey = "MyTelegarm.CuckooFilter.Blocked";
    public const string UserNameCuckooFilterKey = "MyTelegarm.CuckooFilter.UserName";
    public const string ChannelMessageViewsBloomFilterKey = "MyTelegarm.BloomFilter.ChannelMessageViews";
    public static int EditTimeLimit { get; set; } = 172800;

    public const int QuickRepliesLimit = 100;
    public const int QuickReplyMessagesLimit = 20;
    public const int ScheduleMessagesLimit = 30;
    public const int ActiveStoriesLimit = 10;
    public const int StoriesWeeklyLimit = 10;
    public const int StoriesMonthlyLimit = 30;
    public const int StoryCaptionLengthLimit = 200;

    public const int ChatlistInvitesLimitDefault = 3;
    public const int ChatlistInvitesLimitPremium = 50;
    public const int ChatlistsJoinedLimitDefault = 2;
    public const int ChatlistsJoinedLimitPremium = 20;

    public class GeoKeys
    {
        public const string User = "User";
        public const string GeoGroup = "GeoGroup";
    }
}