using MyTelegram.Schema;

namespace MyTelegram.Queries;

public record GetPagedListQuery(int Skip, int Limit);

public record GetAllBlockedQuery(int Skip, int Limit) : IQuery<IReadOnlyCollection<IBlockedReadModel>>;

public record GetAllDraftQuery(long OwnerPeerId) : IQuery<IReadOnlyCollection<IDraftReadModel>>;

public record GetAllUserNameQuery(
    int Skip,
    int Limit) : IQuery<IReadOnlyCollection<IUserNameReadModel>>;

//public record GetAuthKeyByAuthKeyIdQuery(long AuthKeyId) : IQuery<IAuthKeyReadModel?>;

public record GetBlockedListQuery(
    long UserId,
    int Skip,
    int Limit) : IQuery<IReadOnlyCollection<IBlockedReadModel>>;

public record GetBlockedQuery(
    long UserId,
    long TargetPeerId) : IQuery<IBlockedReadModel?>;

//public record GetBotByBotUserIdQuery(long BotUserId) : IQuery<IBotReadModel?>;

public record GetBotsByChannelIdQuery(long ChannelId) : IQuery<IReadOnlyCollection<long>>;

public record GetChannelByChannelIdListQuery(IList<long> ChannelIdList)
    : IQuery<IReadOnlyCollection<IChannelReadModel>>;

public record GetChannelByIdQuery(long ChannelId) : IQuery<IChannelReadModel?>;

public record GetChannelFullByIdQuery(long ChannelId) : IQuery<IChannelFullReadModel?>;

public record GetChannelIdListByMemberUserIdQuery(long MemberUserId) : IQuery<IReadOnlyCollection<long>>;

public record GetChannelIdListByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<long>>;

public record GetChannelMemberByUserIdQuery(
    long ChannelId,
    long UserId) : IQuery<IChannelMemberReadModel?>;

public record GetChannelMemberListByChannelIdListQuery(
    long MemberUserId,
    List<long> ChannelIdList) : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>;

public record GetChannelMembersByChannelIdQuery(
    long ChannelId,
    List<long> MemberUserIdList,
    //bool? Kicked,
    int Offset,
    int Limit,
    bool OnlyAdmin = false,
    bool OnlyBots = false,
    bool OnlyKicked = false,
    bool OnlyBanned = false,
    string? Keyword = null
    )
    : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>;

public record GetChannelPushUpdatesBySeqNoQuery(
    List<long> ChannelIdList,
    long SeqNo,
    int Limit) : IQuery<IReadOnlyCollection<IPushUpdatesReadModel>>;

public record GetChatByChatIdListQuery(IList<long> ChatIdList) : IQuery<IReadOnlyCollection<IChatReadModel>>;

public record GetChatByChatIdQuery(long ChatId) : IQuery<IChatReadModel?>;

public record GetChatInvitesQuery(
    bool Revoked,
    long PeerId,
    long AdminId,
    int? OffsetDate,
    string OffsetLink,
    int Limit)
    : IQuery<IReadOnlyCollection<IChatInviteReadModel>>;

public record GetChosenVoteAnswersQuery(List<long> PollIds, long VoterPeerId)
    : IQuery<IReadOnlyCollection<IPollAnswerVoterReadModel>>;

public record GetContactListQuery(long SelfUserId, List<long> TargetUserIdList)
    : IQuery<IReadOnlyCollection<IContactReadModel>>;

public record GetContactListBySelfIdAndTargetUserIdQuery(long SelfUserId, long TargetUserId)
    : IQuery<IReadOnlyCollection<IContactReadModel>>;

public record GetContactQuery(
    long SelfUserId,
    long TargetUserId) : IQuery<IContactReadModel?>;

public record GetContactsByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IContactReadModel>>;

public record GetContactUserIdListQuery(long SelfUserId) : IQuery<IReadOnlyCollection<long>>;

public record GetContactUserIdListByTargetUserIdListQuery(long SelfUserId, List<long> TargetUserIdList)
    : IQuery<IReadOnlyCollection<long>>;

public record GetContactsByPhonesQuery(long SelfUserId, List<string> Phones)
    : IQuery<IReadOnlyCollection<IContactReadModel>>;

//public record GetDeviceByAuthKeyIdQuery(long AuthKeyId) : IQuery<IDeviceReadModel?>;

//public record GetDeviceByHashQuery(long UserId, long Hash) : IQuery<IDeviceReadModel?>;

//public record GetDeviceByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IDeviceReadModel>>;

public record GetDialogByIdQuery(string Id) : IQuery<IDialogReadModel?>;

public record GetDialogFilterByIdQuery(long OwnerUserId, int FolderId) : IQuery<IDialogFilterReadModel?>;
public record GetDialogFiltersQuery(long OwnerUserId) : IQuery<IReadOnlyCollection<IDialogFilterReadModel>>;

public record GetDialogsQuery(
    long OwnerId,
    bool? Pinned,
    DateTime? OffsetDate,
    OffsetInfo Offset,
    int Limit,
    List<long>? PeerIdList,
    int? FolderId
)
    : IQuery<IReadOnlyCollection<IDialogReadModel>>;

public record GetDialogsByFolderIdQuery(long OwnerUserId, int FolderId) : IQuery<IReadOnlyCollection<Peer>>;

public record GetDiscussionMessageQuery(
    bool GetDiscussionMessageFromPostChannel,
    long SavedFromPeerId,
    int SavedFromMessageId) : IQuery<IMessageReadModel?>;

public record GetEncryptedChatByIdQuery(long ChatId) : IQuery<IEncryptedChatReadModel?>;

public record GetEncryptedPushUpdatesByQtsQuery(
    long PeerId,
    long PermAuthKeyId,
    int Qts,
    int Limit)
    : IQuery<IReadOnlyCollection<IEncryptedPushUpdatesReadModel>>;

public record GetFileQuery(long FileId) : IQuery<IFileReadModel?>;

public record GetJoinedChannelIdListQuery(
    long MemberUserId,
    List<long> ChannelIdList) : IQuery<IReadOnlyCollection<long>>;

public record GetAllJoinedChannelIdListQuery(long SelfUserId) : IQuery<IReadOnlyCollection<long>>;

public record GetKickedChannelMembersQuery(
    long ChannelId,
    int Offset,
    int Limit) : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>;

public record GetLatestAppCodeQuery(
    string PhoneNumber,
    string PhoneCodeHash) : IQuery<IAppCodeReadModel?>;

public record GetLinkedChannelIdQuery(long ChannelId) : IQuery<long?>;

public record GetSendAsQuery(long SelfUserId) : IQuery<IReadOnlyCollection<IChannelReadModel>>;

public record GetSendAsPeerIdQuery(long CreatorUserId, long LinkedChannelId) : IQuery<long?>;

public record GetLoginLogQuery(long UserId) : IQuery<ILoginLogReadModel?>;

public record GetMegaGroupByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IChannelReadModel>>;

public record GetMessageByIdQuery(string Id) : IQuery<IMessageReadModel?>;

public record GetMessageByPeerIdAndMessageIdQuery(long OwnerPeerId, int MessageId) : IQuery<IMessageReadModel?>;

public record GetMessageIdListQuery(
    long OwnerPeerId,
    long ToPeerId,
    int MaxMessageId,
    int Limit)
    : IQuery<List<int>>;

//public record GetMessageReactionListQuery(
//    long SelfUserId,
//    long ToPeerId,
//    int MessageId,
//    long? ReactionId,
//    int? Offset,
//    int Limit)
//    : IQuery<IReadOnlyCollection<IUserReactionReadModel>>;

public record GetMessagesByIdListQuery(IList<string> MessageIdList) : IQuery<IReadOnlyCollection<IMessageReadModel>>;

public record GetMessagesByMessageIdListQuery(List<int> MessageIdList) : IQuery<IReadOnlyCollection<IMessageReadModel>>;

public record GetMessagesByUserIdQuery(
    long OwnerPeerId,
    long ToPeerId) : IQuery<IReadOnlyCollection<IMessageReadModel>>;

public record GetMessagesQuery(
    long OwnerPeerId,
    MessageType MessageType,
    string? Q,
    List<int>? MessageIdList,
    int ChannelHistoryMinId,
    int Limit,
    OffsetInfo? Offset,
    Peer? Peer,
    long SelfUserId,
    int Pts,
    int ReplyToMsgId = 0,
    List<long>? JoinedChannelIdList = null,
    MessageActionType? MessageActionType = null,
    bool BroadcastsOnly = false,
    bool GroupsOnly = false,
    bool UsersOnly = false
)
    : IQuery<IReadOnlyCollection<IMessageReadModel>>
{
    public bool IsSearchGlobal { get; set; }
    public OffsetInfo? Offset { get; set; } = Offset;
}

public record GetMessagesReactionsQuery(long OwnerPeerId, List<int> MessageIds)
    : IQuery<IReadOnlyCollection<IReactionItem>>;

public record GetMessageViewsQuery(
    long ChannelId,
    List<int> MessageIdList) : IQuery<IReadOnlyCollection<MessageView>>;

public record GetPeerNotifySettingsByIdQuery(string Id) : IQuery<IPeerNotifySettingsReadModel>;

//public record GetPeerNotifySettingsListQuery(IReadOnlyList<PeerNotifySettingsId> PeerNotifySettingsIdList)
//    : IQuery<IReadOnlyCollection<IPeerNotifySettingsReadModel>>;

public record GetPhoneCallConfigQuery(long UserId) : IQuery<IPhoneCallConfigReadModel?>;

public record GetPollAnswerVotersQuery(long PollId, long VoterPeerId)
    : IQuery<IReadOnlyCollection<IPollAnswerVoterReadModel>>;

public record GetPollVotesQuery(long PollId, string? Option, int Skip, int Limit)
    : IQuery<IReadOnlyCollection<IPollAnswerVoterReadModel>>;

public record GetPollIdByMessageIdQuery(long PeerId, int MessageId) : IQuery<long?>;

public record GetPollQuery(/*long ToPeerId,*/ long PollId) : IQuery<IPollReadModel?>;

public record GetPollsQuery(List<long> PollIds) : IQuery<IReadOnlyCollection<IPollReadModel>>;

public record GetPrivacyListQuery(
    IReadOnlyList<long> UserIdList,
    IReadOnlyList<PrivacyType> PrivacyTypes)
    : IQuery<IReadOnlyCollection<IPrivacyReadModel>>;

public record GetPrivacyQuery(
    long UserId,
    PrivacyType PrivacyType) : IQuery<IPrivacyReadModel?>;

public record GetPtsByPeerIdQuery(long PeerId) : IQuery<IPtsReadModel?>;

public record GetPtsByPermAuthKeyIdQuery(
    long PeerId,
    long PermAuthKeyId) : IQuery<IPtsForAuthKeyIdReadModel?>;

public record GetReadHistoryMessageQuery(
    long OwnerPeerId,
    int MessageId,
    long ToPeerId) : IQuery<IMessageReadModel?>;

public record GetReadingHistoryQuery(
    long TargetPeerId,
    long MessageId) : IQuery<IReadOnlyCollection<long>>;

public record GetRepliesQuery(
    long ChannelId,
    IList<int> MessageIds) : IQuery<IReadOnlyCollection<IReplyReadModel>>;

public record GetReplyQuery(
    long ChannelId,
    int SavedFromMsgId) : IQuery<IReplyReadModel?>;

public record GetRpcResultByIdQuery(string Id) : IQuery<IRpcResultReadModel?>;
public record GetRpcResultListQuery(int ExpirationDate, int Limit) : IQuery<IReadOnlyCollection<RpcResultSimpleItem>>;

public record RpcResultSimpleItem(long UserId, long ReqMsgId);

public record GetUserByIdQuery(long UserId) : IQuery<IUserReadModel?>;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<IUserReadModel?>;

public record GetUserNameByIdQuery(string UserName) : IQuery<IUserNameReadModel?>;

public record GetUserNameByNameQuery(string Name) : IQuery<IUserNameReadModel?>;

public record GetUserPasswordQuery(long UserId) : IQuery<IUserPasswordReadModel?>;

public record GetUsersByUserIdListQuery(List<long> UserIdList) : IQuery<IReadOnlyCollection<IUserReadModel>>;

public record MessageView
{
    public int MessageId { get; set; }
    public int Views { get; set; }
}

public record SearchContactQuery(
    long SelfUserId,
    string Keyword) : IQuery<IReadOnlyCollection<IContactReadModel>>;

public record SearchUserByKeywordQuery(
    string Keyword,
    int Limit) : IQuery<IReadOnlyCollection<IUserReadModel>>;

public record SearchUserNameQuery(string Keyword) : IQuery<IReadOnlyCollection<IUserNameReadModel>>;

public record GetMessageIdListByUserIdQuery(long ChannelId, long SenderUserId, int Limit)
    : IQuery<IReadOnlyCollection<int>>;

public record GetMessageIdListByChannelIdQuery(long ChannelId, int Limit) : IQuery<IReadOnlyCollection<int>>;

public record GetForumTopicsQuery(
    long ChannelId,
    int OffsetDate,
    int OffsetId,
    int OffsetTopic,
    string? Q,
    int Limit) : IQuery<IReadOnlyCollection<IForumTopicReadModel>>;

public record GetForumMessagesQuery(long ChannelId, List<int> MessageIds)
    : IQuery<IReadOnlyCollection<IMessageReadModel>>;

public record GetForumTopicsByIdsQuery(
    long ChannelId,
    List<int> TopicIds) : IQuery<IReadOnlyCollection<IForumTopicReadModel>>;

public record GetForumTopicByIdQuery(long ChannelId, int TopicId) : IQuery<IForumTopicReadModel?>;

//public record GetAccessHashQueryByIdQuery(long Id) : IQuery<IAccessHashReadModel?>;

public record GetMessageReadParticipantsQuery(long SelfUserId, long TargetPeerId, long MessageId)
    : IQuery<IReadOnlyCollection<IReadingHistoryReadModel>>;

public record GetPeerSettingsQuery(long SelfUserId, long PeerId) : IQuery<IPeerSettingsReadModel?>;

public record GetPhotosByUserIdQuery(long UserId, IList<long> PhotoIds) : IQuery<IReadOnlyCollection<IPhotoReadModel>>;

public record GetUserProfilePhotosQuery(long UserId) : IQuery<IReadOnlyCollection<IPhotoReadModel>>;

//public record GetPhotosByPhotoIdLisQuery(IList<long> PhotoIds) : IQuery<IReadOnlyCollection<IPhotoReadModel>>;

//public record GetPhotoByIdQuery(long PhotoId) : IQuery<IPhotoReadModel?>;

public record GetUsersByPhoneNumberListQuery(List<string> PhoneNumbers) : IQuery<IReadOnlyCollection<IUserReadModel>>;

public record GetMaxMessageByChatIdQuery(long SelfUserId, long ChatId) : IQuery<int>;
public record GetMinMessageIdQuery(long SelfUserId) : IQuery<int>;
public record GetMaxMessageIdByPeerIdQuery(long PeerId) : IQuery<int>;

public record GetChatAdminListByChannelIdQuery(long PeerId, int Skip, int Limit)
    : IQuery<IReadOnlyCollection<IChatAdminReadModel>>;

public record GetChatAdminListQuery(int Skip, int Limit) : IQuery<IReadOnlyCollection<IChatAdminReadModel>>;

public record GetAdminLogListQuery(long ChannelId, List<AdminLogEventAction> ActionTypes, int Skip, int Limit) : IQuery<
    IReadOnlyCollection<IChannelAdminLogEventReadModel>>;

public record AdminWithInvites(long AdminId, int InvitesCount, int RevokedInvitesCount);

public record GetAdminInvitesQuery(long ChannelId) : IQuery<IReadOnlyCollection<AdminWithInvites>>;

public record GetChatInviteQuery(long PeerId, string Link) : IQuery<IChatInviteReadModel?>;

public record GetChatInviteByLinkQuery(string Link) : IQuery<IChatInviteReadModel?>;

public record GetRevokedChatInvitesQuery(long PeerId, long AdminId) : IQuery<IReadOnlyCollection<IChatInviteReadModel>>;

//public record GetChatInviteImportersQuery(
//    long PeerId,
//    ChatInviteRequestState? ChatInviteRequestState,
//    int? InviteId,
//    int OffsetDate,
//    long OffsetUserId,
//    string? Q,
//    int Limit) : IQuery<IReadOnlyCollection<IChatInviteImporterReadModel>>;

public record GetUserNameListByNamesQuery(List<string> UserNames, PeerType? PeerType = null)
    : IQuery<IReadOnlyCollection<IUserNameReadModel>>;

public record GetUnreadMentionedMessageIdListQuery(
    long OwnerUserId,
    long ToPeerId,
    OffsetInfo? Offset,
    int Skip,
    int Limit) : GetPagedListQuery(Skip, Limit), IQuery<IReadOnlyCollection<int>>;

public record GetRpcResultQuery(long UserId, long ReqMsgId) : IQuery<IRpcResultReadModel?>;

public record GetUpdatesQuery(
    long SelfUserId,
    long PeerId,
    int MinPts,
    int Date,
    int Limit) : IQuery<IReadOnlyCollection<IUpdatesReadModel>>;

public record GetChannelUpdatesByGlobalSeqNoQuery(List<long> ChannelIdList, long MinGlobalSeqNo, int Limit)
    : IQuery<IReadOnlyCollection<IUpdatesReadModel>>;

public record GetUpdatesByGlobalSeqNoQuery(long UserId, long MinGlobalSeqNo)
    : IQuery<IReadOnlyCollection<IUpdatesReadModel>>;

public record GetReplyToMsgIdListQuery(Peer ToPeer, long SelfUserId, int? ReplyToMsgId)
    : IQuery<IReadOnlyCollection<ReplyToMsgItem>?>;

public record GetPermanentChatInviteQuery(long ChannelId, long AdminUserId) : IQuery<IChatInviteReadModel?>;

public record GetEncryptedMessagesQuery(long UserId, long PermAuthKeyId, int Qts)
    : IQuery<IReadOnlyCollection<IEncryptedMessageReadModel>>;

public record GetChannelMaxMessageIdQuery(long ChannelId) : IQuery<int>;

public record GetMaxUserIdQuery : IQuery<long>;

public record GetUnreadCountQuery(long OwnerUserId, long ToPeerId, int MaxMessageId) : IQuery<int>;

//public record GetDocumentByIdQuery(long Id) : IQuery<IDocumentReadModel?>;

public record GetWallPapersQuery(long UserId) : IQuery<IReadOnlyCollection<IWallPaperReadModel>>;

public record GetWallPaperQuery(long WallPaperId) : IQuery<IWallPaperReadModel?>;

public record GetWallPaperBySlugQuery(string Slug) : IQuery<IWallPaperReadModel?>;

//public record GetDocumentsByIdsQuery(IList<long> Ids) : IQuery<IReadOnlyCollection<IDocumentReadModel>>;

public record GetGlobalPrivacySettingsQuery(long UserId) : IQuery<GlobalPrivacySettings?>;

public record GetGlobalPrivacySettingsListQuery(List<long> UserIds)
    : IQuery<IReadOnlyDictionary<long, GlobalPrivacySettings?>>;

public record GetOutboxReadDateQuery(long UserId, int MessageId, Peer ToPeer) : IQuery<int>;

public record GetChatAdminQuery(long PeerId, long AdminId) : IQuery<IChatAdminReadModel?>;

public record GetChatInviteImporterQuery(long PeerId, long UserId) : IQuery<IChatInviteImporterReadModel?>;

public record GetChatInviteImporterListForApprovalQuery(long PeerId, long InviteId)
    : IQuery<IReadOnlyCollection<IChatInviteImporterReadModel>>;

public record GetAdminedPublicChannelIdsQuery(long UserId) : IQuery<IReadOnlyCollection<long>>;

public record GetAdminedPublicChannelsQuery(long UserId) : IQuery<IReadOnlyCollection<IChannelReadModel>>;

public record GetFirstInboxMessageIdByMessageIdListQuery(long UserId, long ChannelId, List<int> MessageIds) : IQuery<int?>;

public record GetCommentsMessageIdListQuery(long ChannelId, List<int> MessageIds) : IQuery<IReadOnlyCollection<int>>;

public record GetTopMessageIdQuery(long OwnerPeerId, List<int> MessageIds) : IQuery<int>;

public record GetTopMessageQuery(long OwnerPeerId, List<int> MessageIds) : IQuery<IMessageReadModel?>;

//public record GetSenderTopMessageIdQuery(long SenderUserId,)
public record GetMessageItemListToBeDeletedQuery(long OwnerPeerId, List<int> MessageIds, bool Revoke)
    : IQuery<IReadOnlyCollection<MessageItemToBeDeleted>>;

public record GetMessageItemListToBeDeletedQuery2(long OwnerPeerId, long ToPeerId, int MaxId, int Limit, bool Revoke)
    : IQuery<IReadOnlyCollection<MessageItemToBeDeleted>>;

public record GetPhoneCallHistoryToBeDeletedQuery(long UserId, int Limit, bool Revoke)
    : IQuery<IReadOnlyCollection<MessageItemToBeDeleted>>;

public record GetPinnedMessageListQuery(
    long RequestUserId,
    Peer ToPeer,
    bool IncludeOtherParticipantMessages,
    int Limit) : IQuery<IReadOnlyCollection<SimpleMessageItem>>;

public record GetSimpleMessageListQuery(
    long OwnerPeerId,
    Peer ToPeer,
    List<int>? MessageIds,
    bool? Pinned,
    bool IncludeOtherParticipantMessages,
    int Limit) : IQuery<IReadOnlyCollection<SimpleMessageItem>>;

//public record GetBotCallbackAnswerQuery(long PeerId, long QueryId) : IQuery<IBotCallbackAnswerReadModel?>;
public record GetBotByIdQuery(long BotUserId) : IQuery<IBotReadModel?>;

public record GetBotCallbackAnswerQuery(long PeerId, long QueryId) : IQuery<IBotCallbackAnswerReadModel?>;

public record GetBotByUserNameQuery(long OwnerUserId, string UserName) : IQuery<IBotReadModel?>;

public record GetBotsCountQuery(long OwnerUserId) : IQuery<int>;

public record GetMaxBotUserIdQuery : IQuery<long>;

public record GetMyBotQuery(long OwnerUserId, long BotUserId) : IQuery<IBotReadModel?>;

public record GetMyBotsQuery(long OwnerUserId) : IQuery<IReadOnlyCollection<IBotReadModel>>;

public record GetBotListQuery(List<long> BotUserIds) : IQuery<IReadOnlyCollection<IBotReadModel>>;

public record GetPushDevicesQuery(long UserId) : IQuery<IReadOnlyCollection<IPushDeviceReadModel>>;

public record GetThemeByIdQuery(long Id) : IQuery<IThemeReadModel?>;

public record GetThemeBySlugQuery(string Slug) : IQuery<IThemeReadModel?>;

public record GetThemeByEmoticonQuery(string Emoticon) : IQuery<IThemeReadModel?>;

public record GetDefaultThemesQuery : IQuery<IReadOnlyCollection<IThemeReadModel>>;

public record GetWallPaperListQuery(List<long>? Ids, List<string>? Slugs)
    : IQuery<IReadOnlyCollection<IWallPaperReadModel>>;

public record GetChannelMemberListQuery(long ChannelId) : IQuery<IReadOnlyCollection<long>>;

public record GetChatMemberListQuery(long ChatId) : IQuery<IReadOnlyCollection<long>>;

public record GetChatWallPaperQuery(long UserId, long ToPeerId) : IQuery<IChatWallPaperReadModel?>;

public record GetWallPaperListByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IUserWallPaperReadModel>>;

public record GetChatThemeQuery(long UserId, long ToPeerId) : IQuery<IChatThemeReadModel?>;

public record GetThemesByFormatQuery(long UserId, string Format, List<long> ThemeIds)
    : IQuery<IReadOnlyCollection<IThemeReadModel>>;

public record GetInstalledThemeIdsQuery(long UserId, string Format) : IQuery<IReadOnlyCollection<long>>;

public record GetGroupCallQuery(long ChatId) : IQuery<IGroupCallReadModel?>;

public record GetGroupCallByIdQuery(long GroupCallId) : IQuery<IGroupCallReadModel?>;

public record GetGroupCallParticipantQuery(long GroupCallId, long UserId) : IQuery<IGroupCallParticipantReadModel?>;

public record GetGroupCallParticipantsQuery(long GroupCallId, List<long> UserIds)
    : IQuery<IReadOnlyCollection<IGroupCallParticipantReadModel>>;

public record GetChannelTopMessageIdQuery(long ChannelId) : IQuery<int?>;

public record GetUserFullQuery(long UserId) : IQuery<IUserFullReadModel?>;

public record GetAllAnimatedStickerIdListQuery : IQuery<IReadOnlyCollection<long>>;

public record GetAllStickerIdListQuery : IQuery<IReadOnlyCollection<long>>;

public record GetQuickReplyByIdQuery(long UserId, int ShortcutId) : IQuery<IQuickReplyReadModel?>;

public record GetQuickReplyByTitleQuery(long UserId, string Title) : IQuery<IQuickReplyReadModel?>;

public record GetQuickReplyCountQuery(long UserId) : IQuery<long>;

public record GetQuickRepliesByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IQuickReplyReadModel>>;

public record GetQuickReplyMessagesQuery(long UserId, int? ShortcutId, List<int> MessageIds)
    : IQuery<IReadOnlyCollection<IMessageReadModel>>;

//public record GetQuickReplyMessagesByShortcutIdQuery(long UserId, int ShortcutId, List<int> MessageIds)
//    : IQuery<IReadOnlyCollection<IMentionsReadModel>>;
//public record GetDocumentIdByMd5Query(string Md5) : IQuery<long?>;

public record GetLastReceivedMessageDateQuery(long OwnerUserId, long SenderUserId, int LatestReceiveMessageDate)
    : IQuery<int?>;

public record GetAllEffectsQuery : IQuery<IReadOnlyCollection<IEffectReadModel>>;

public record GetAllReactionsQuery : IQuery<IReadOnlyCollection<IReactionReadModel>>;
public record GetAllCustomEmojiStickerSetQuery : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;

public record GetAllStickerSetsQuery : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;
//public record GetAllStickersQuery : IQuery<IReadOnlyCollection<IStickerReadModel>>;
public record GetFeaturedStickerSetsQuery(StickerSetType StickerSetType) : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;
public record GetInstalledStickerSetQuery(long UserId, long StickerSetId) : IQuery<IInstalledStickerSetReadModel?>;
public record GetInstalledStickerSetsQuery(long UserId, StickerSetType StickerSetType, List<long>? StickerSetIds = null) : IQuery<IReadOnlyCollection<IInstalledStickerSetReadModel>>;
//public record GetStickersByStickerIdListQuery(List<long> stickerIds) : IQuery<IReadOnlyCollection<IStickerReadModel>>;

//public class GetStickersByStickerSetIdListQuery(List<long> stickerSetIdList)
//    : IQuery<IReadOnlyCollection<IStickerReadModel>>;

//public class GetStickersByStickerSetIdQuery(long stickerSetId, int limit)
//    : IQuery<IReadOnlyCollection<IStickerReadModel>>
//{
//    public long StickerSetId { get; } = stickerSetId;
//    public int Limit { get; } = limit;
//}

public record GetStickerSetByIdQuery(long StickerSetId) : IQuery<IStickerSetReadModel?>;
public record GetStickerSetByNameQuery(string ShortName) : IQuery<IStickerSetReadModel?>;
public record GetStickerSetsByIdListQuery(List<long> StickerSetIds) : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;
public record GetStickerSetsByNamesQuery(List<string> StickerSetShortNames) : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;
public record GetStickerSetTotalCountQuery : IQuery<int>;
public record GetUserStickerConfigQuery(long UserId) : IQuery<IUserStickerConfigReadModel?>;
public record SearchStickerSetQuery(string Q, int Limit) : IQuery<IReadOnlyCollection<IStickerSetReadModel>>;
public record GetChannelUserNameByChannelIdQuery(long ChannelId) : IQuery<string?>;
public record GetScheduledMessagesQuery(long UserId, long ToPeerId, List<int> MessageIds) : IQuery<IReadOnlyCollection<IScheduleMessageReadModel>>;
public record GetScheduleMessagesByDateQuery(int MaxScheduleDate) : IQuery<IReadOnlyCollection<ScheduleItem>>;
public record GetScheduleMessageCountQuery(long UserId) : IQuery<int>;
public record GetTtlQuery(long UserId, Peer? ToPeer = null) : IQuery<ITtlReadModel?>;

public record GetAutoDeleteMessagesQuery(int MinDate, int Skip, int Limit) : IQuery<IReadOnlyCollection<AutoDeleteMessageItem>>;
public record GetCommonChatCountQuery(long SelfUserId, long TargetUserId) : IQuery<int>;
public record GetCommonChatChannelIdsQuery(long SelfUserId, long TargetUserId, long MaxId, int Limit) : IQuery<IReadOnlyCollection<long>>;

public record GetActiveStoryCountQuery(long UserId) : IQuery<int>;
public record GetStoriesDatesQuery(long OwnerPeerId, int MinDate) : IQuery<IReadOnlyCollection<int>>;
public record GetBotMenuButtonQuery(long BotUserId, long UserId = 0) : IQuery<IBotMenuReadModel?>;
public record GetUserProfilePhotoIdQuery(long UserId) : IQuery<long?>;
public record GetTotalContactCountQuery : IQuery<int>;
public record GetTotalBlockedCountQuery : IQuery<int>;
public record GetAllContactsQuery(int Skip, int Limit) : IQuery<IReadOnlyCollection<IContactReadModel>>;
public record GetCollectibleUsernamesQuery(long OwnerPeerId) : IQuery<IReadOnlyCollection<ICollectibleUsernameReadModel>>;
public record GetCollectibleUsernameByUsernameQuery(string UserName) : IQuery<ICollectibleUsernameReadModel?>;
public record GetAllLanguagesQuery : IQuery<IReadOnlyCollection<ILanguageReadModel>>;
public record GetAllLanguageTextsQuery : IQuery<IReadOnlyCollection<ILanguageTextReadModel>>;
public record GetStoriesByPeerQuery(long PeerId) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetActiveStoriesQuery(long PeerId) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetStoriesByIdListQuery(long PeerId, List<int> StoryIds) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetStoryByIdQuery(long PeerId, int StoryId) : IQuery<IStoryReadModel?>;
public record GetPinnedStoriesQuery(long PeerId, int OffsetId, int Limit) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetExpiredStoriesQuery(int ExpireDate, int Limit) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetArchivedStoriesQuery(long PeerId, int OffsetId, int Limit) : IQuery<IReadOnlyCollection<IStoryReadModel>>;
public record GetChatsToSendQuery(long UserId) : IQuery<IReadOnlyCollection<long>>;
public record GetActiveStoryIdListQuery(long PeerId, int MaxId) : IQuery<IReadOnlyCollection<int>>;
public record GetUnreadStoryIdListQuery(long UserId, long PeerId, int MaxId, List<int> StoryIds) : IQuery<IReadOnlyCollection<int>>;
public record GetStoryPeerMaxIdsQuery(long UserId, List<long> OwnerPeerIds) : IQuery<IReadOnlyCollection<IStoryMaxIdReadModel>>;
public record GetAllStoryPeerMaxIdsQuery(long UserId) : IQuery<IReadOnlyCollection<IStoryMaxIdReadModel>>;
public record GetStoriesViewsQuery(long OwnerPeerId, List<int> StoryIds) : IQuery<IReadOnlyCollection<IStoryViewReadModel>>;

public record GetStoryViewQuery(long OwnerPeerId, int StoryId) : IQuery<IStoryViewReadModel?>;
public record GetStoryViewsListQuery(long OwnerPeerId, int StoryId, int Offset, int Limit, string? Q) : IQuery<IReadOnlyCollection<IStoryViewDetailsReadModel>>;
public record GetStoryReactionsListQuery(long OwnerPeerId, int StoryId, long? ReactionId, int Offset, int Limit) : IQuery<IReadOnlyCollection<IStoryViewDetailsReadModel>>;
public record GetStoryReactionDetailsListQuery(long OwnerPeerId, List<int> StoryIds, long UserId) : IQuery<IReadOnlyCollection<IStoryViewDetailsReadModel>>;
public record GetStoryReactionUniqueCountQuery(long OwnerPeerId, int StoryId) : IQuery<int>;
public record GetWebPageQuery(string Url) : IQuery<IWebPageReadModel?>;
public record GetMessageSenderUserIdListQuery(long ChannelId, List<int> MessageIds) : IQuery<IReadOnlyCollection<long>>;
public record GetEffectByIdQuery(long Id) : IQuery<IEffectReadModel?>;
//public record GetMessageReactionsQuery(long UserId, long ToPeerId, int MessageId) : IQuery<List<IReaction>?>;
public record GetAllMessageReactionListQuery(List<string> Ids) : IQuery<IReadOnlyCollection<IUserReactionReadModel>>;
public record GetMessageReactionsQuery(long UserId, long ToPeerId, List<int> MessageIds) : IQuery<IReadOnlyCollection<IUserReactionReadModel>>;

public record GetMyMessageReactionsQuery(long UserId, long ToPeerId, int MessageId)
    : IQuery<IReadOnlyCollection<IReaction>>;

public record GetMessageReactionsListQuery(long UserId, Peer ToPeer, int MessageId, long? ReactionId, int Skip, int Limit) : IQuery<IReadOnlyCollection<IUserReactionReadModel>>;
public record GetChannelIdsByKeywordQuery(long UserId, string Keyword, int Limit) : IQuery<IReadOnlyCollection<long>>;
public record GetPendingRequestsCountQuery(long ChannelId) : IQuery<int>;
public record GetRecentRequestUserIdListQuery(long ChannelId, int Limit) : IQuery<IReadOnlyCollection<long>>;
public record GetChatInviteImportersQuery(long ChannelId,
    ChatInviteRequestState? ChatInviteRequestState,
    long? InviteId,
    int? OffsetDate,
    long? OffsetUserId,
    string? Q,
    int Limit) : IQuery<IReadOnlyCollection<IJoinChannelRequestReadModel>>;

public record GetLeftChannelCountQuery(long UserId) : IQuery<int>;
public record GetLeftChannelIdsQuery(long UserId, List<long>? ChannelIds = null, int OffsetChannelId = 0, int Limit = 500) : IQuery<IReadOnlyCollection<long>>;
public record GetBotMembersByChannelIdQuery(long ChannelId) : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>;
public record GetJoinRequestQuery(long ChannelId, long UserId) : IQuery<IJoinChannelRequestReadModel?>;
public record GetPhotoListQuery(long UserId, List<long> PhotoIds) : IQuery<IReadOnlyCollection<IPhotoReadModel>>;
public record GetChatlistInvitesCountQuery(long UserId, int FolderId) : IQuery<int>;
public record GetShareableFolderCountQuery(long UserId) : IQuery<int>;
public record GetMaxFolderIdQuery(long UserId) : IQuery<int>;
public record GetAllDialogFiltersQuery(int Skip, int Limit) : IQuery<IReadOnlyCollection<IDialogFilterReadModel>>;
public record GetImportedDialogFolderQuery(long UserId, string Slug) : IQuery<IDialogFilterReadModel?>;
public record SearchPostsQuery(string Hashtag, int OffsetRate, long OffsetPeerId, int OffsetId, int Limit) : IQuery<IReadOnlyCollection<IMessageReadModel>>;
public record GetPostsCountQuery(string Hashtag, int OffsetRate, int OffsetId) : IQuery<int>;

public record GetFilesQuery(List<long> FileIds) : IQuery<IReadOnlyCollection<IFileReadModel>>;
public record GetAuthKeyByAuthKeyIdQuery(long AuthKeyId) : IQuery<IAuthKeyReadModel?>;
public record GetDeviceByUserIdQuery(long UserId) : IQuery<IReadOnlyCollection<IDeviceReadModel>>;
public record GetDeviceByAuthKeyIdQuery(long AuthKeyId) : IQuery<IDeviceReadModel?>;
public record GetDeviceByHashQuery(long UserId, long Hash) : IQuery<IDeviceReadModel?>;
public record GetAccessHashQueryByIdQuery(long Id) : IQuery<IAccessHashReadModel?>;

public record GetDocumentByIdQuery(long Id) : IQuery<IDocumentReadModel?>;

public record GetDocumentIdByMd5Query(string Md5) : IQuery<long?>;

public record GetDocumentsByIdListQuery(List<long> Ids) : IQuery<IReadOnlyCollection<IDocumentReadModel>>;

public record GetDocumentsByIdsQuery(IList<long> Ids) : IQuery<IReadOnlyCollection<IDocumentReadModel>>;

public record GetPhotoByIdQuery(long PhotoId) : IQuery<IPhotoReadModel?>;

public record GetPhotosByPhotoIdLisQuery(IList<long> PhotoIds) : IQuery<IReadOnlyCollection<IPhotoReadModel>>;
public record GetUserConfigByKeyQuery(long UserId, string Key) : IQuery<IUserConfigReadModel?>;
public record GetChannelMemberIdListQuery(long ChannelId, List<long> MemberUserIds) : IQuery<IReadOnlyCollection<long>>;
public record GetMessageByBatchIdQuery(Guid BatchId, long ExcludePeerId) : IQuery<IMessageReadModel?>;
public record GetFutureCreatorAfterLeaveQuery(long ChannelId, long CurrentCreatorUserId) : IQuery<long?>;