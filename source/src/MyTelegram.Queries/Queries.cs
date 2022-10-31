namespace MyTelegram.Queries;

//public class GetUserByPhoneNumberQuery : IQuery<IUserReadModel?>
//{
//    public string PhoneNumber { get; }

//    public GetUserByPhoneNumberQuery(string phoneNumber)
//    {
//        PhoneNumber = phoneNumber;
//    }
//}

public class GetAllDraftQuery : IQuery<IReadOnlyCollection<IDraftReadModel>>
{
    public GetAllDraftQuery(long ownerPeerId)
    {
        OwnerPeerId = ownerPeerId;
    }

    public long OwnerPeerId { get; }
}

public class GetAllUserNameQuery : IQuery<IReadOnlyCollection<string>>
{
    public GetAllUserNameQuery(int skip,
        int limit)
    {
        Skip = skip;
        Limit = limit;
    }

    public int Limit { get; }

    public int Skip { get; }
}

public class GetAuthKeyByAuthKeyIdQuery : IQuery<IAuthKeyReadModel?>
{
    public GetAuthKeyByAuthKeyIdQuery(long authKeyId)
    {
        AuthKeyId = authKeyId;
    }

    public long AuthKeyId { get; }
}

public class GetAuthKeyByTempAuthKeyIdQuery : IQuery<IAuthKeyReadModel?>
{
    public GetAuthKeyByTempAuthKeyIdQuery(long tempAuthKeyId)
    {
        TempAuthKeyId = tempAuthKeyId;
    }

    public long TempAuthKeyId { get; }
}

public class GetChannelByChannelIdListQuery : IQuery<IReadOnlyCollection<IChannelReadModel>>
{
    public GetChannelByChannelIdListQuery(IList<long> channelIdList)
    {
        ChannelIdList = channelIdList;
    }

    public IList<long> ChannelIdList { get; }
}

public class GetChannelByIdQuery : IQuery<IChannelReadModel>
{
    public GetChannelByIdQuery(long channelId)
    {
        ChannelId = channelId;
    }

    public long ChannelId { get; }
}

public class GetChannelFullByIdQuery : IQuery<IChannelFullReadModel?>
{
    public GetChannelFullByIdQuery(long channelId)
    {
        ChannelId = channelId;
    }

    public long ChannelId { get; }
}

public class GetChannelIdListByMemberUidQuery : IQuery<IReadOnlyCollection<long>>
{
    public GetChannelIdListByMemberUidQuery(long memberUid)
    {
        MemberUid = memberUid;
    }

    public long MemberUid { get; }
}

public class GetChannelIdListByUidQuery : IQuery<IReadOnlyCollection<long>>
{
    public GetChannelIdListByUidQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; }
}

public class GetChannelMemberByUidQuery : IQuery<IChannelMemberReadModel?>
{
    public GetChannelMemberByUidQuery(long channelId,
        long userId)
    {
        ChannelId = channelId;
        UserId = userId;
    }

    public long ChannelId { get; }
    public long UserId { get; }
}

public class GetChannelMemberListByChannelIdListQuery : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetChannelMemberListByChannelIdListQuery(long memberUid,
        List<long> channelIdList)
    {
        MemberUid = memberUid;
        ChannelIdList = channelIdList;
    }

    public List<long> ChannelIdList { get; }

    public long MemberUid { get; }
}

public class GetChannelMembersByChannelIdQuery : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetChannelMembersByChannelIdQuery(long channelId,
        List<long> memberUidList,
        bool kicked,
        int offset,
        int limit)
    {
        ChannelId = channelId;
        MemberUidList = memberUidList;
        Kicked = kicked;
        Offset = offset;
        Limit = limit;
    }

    public long ChannelId { get; }
    public bool Kicked { get; }
    public int Limit { get; }
    public List<long> MemberUidList { get; }
    public int Offset { get; }
}

public class GetChannelPushUpdatesBySeqNoQuery : IQuery<IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetChannelPushUpdatesBySeqNoQuery(List<long> channelIdList,
        long seqNo,
        int limit)
    {
        ChannelIdList = channelIdList;
        SeqNo = seqNo;
        Limit = limit;
    }

    public List<long> ChannelIdList { get; }
    public int Limit { get; }

    public long SeqNo { get; }
}

public class GetChatByChatIdListQuery : IQuery<IReadOnlyList<IChatReadModel>>
{
    public GetChatByChatIdListQuery(IList<long> chatIdList)
    {
        ChatIdList = chatIdList;
    }

    public IList<long> ChatIdList { get; }
}

public class GetChatByChatIdQuery : IQuery<IChatReadModel?>
{
    public GetChatByChatIdQuery(long chatId)
    {
        ChatId = chatId;
    }

    public long ChatId { get; }
}

public class GetChatInvitesQuery : IQuery<IReadOnlyCollection<IChatInviteReadModel>>
{
    public GetChatInvitesQuery(bool revoked,
        long channelId,
        long adminId,
        int offsetDate,
        string offsetLink,
        int limit)
    {
        Revoked = revoked;
        ChannelId = channelId;
        AdminId = adminId;
        OffsetDate = offsetDate;
        OffsetLink = offsetLink;
        Limit = limit;
    }

    public long AdminId { get; }
    public long ChannelId { get; }
    public int Limit { get; }
    public int OffsetDate { get; }
    public string OffsetLink { get; }

    public bool Revoked { get; }
}

public class GetDeviceByAuthKeyIdQuery : IQuery<IDeviceReadModel?>
{
    public GetDeviceByAuthKeyIdQuery(long authKeyId)
    {
        AuthKeyId = authKeyId;
    }

    public long AuthKeyId { get; }
}

public class GetDeviceByHashQuery : IQuery<IDeviceReadModel?>
{
    public GetDeviceByHashQuery(long userId,
        long hash)
    {
        UserId = userId;
        Hash = hash;
    }

    public long Hash { get; }
    public long UserId { get; }
}

public class GetDeviceByUidQuery : IQuery<IReadOnlyCollection<IDeviceReadModel>>
{
    public GetDeviceByUidQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; }
}

public class GetDialogByIdQuery : IQuery<IDialogReadModel?>
{
    public GetDialogByIdQuery(DialogId id)
    {
        Id = id;
    }

    public DialogId Id { get; }
}

public class GetDialogFiltersQuery : IQuery<IReadOnlyCollection<IDialogFilterReadModel>>
{
    public GetDialogFiltersQuery(long ownerUserId)
    {
        OwnerUserId = ownerUserId;
    }
    public long OwnerUserId { get; }
}
public class GetDialogsQuery : IQuery<IReadOnlyList<IDialogReadModel>>
{
    public GetDialogsQuery(long ownerId,
        bool? pinned,
        DateTime? offsetDate,
        OffsetInfo offset,
        int limit,
        List<long>? peerIdList)
    {
        OwnerId = ownerId;
        Pinned = pinned;
        OffsetDate = offsetDate;
        Offset = offset;
        Limit = limit;
        PeerIdList = peerIdList;
    }

    public int Limit { get; }

    public OffsetInfo Offset { get; }
    public DateTime? OffsetDate { get; }
    public long OwnerId { get; }
    public List<long>? PeerIdList { get; }
    public bool? Pinned { get; }
}

public class GetDiscussionMessageQuery : IQuery<IMessageReadModel?>
{
    public GetDiscussionMessageQuery(long savedFromPeerId,
        int savedFromMessageId)
    {
        SavedFromPeerId = savedFromPeerId;
        SavedFromMessageId = savedFromMessageId;
    }
    public int SavedFromMessageId { get; }
    public long SavedFromPeerId { get; }
}
public class GetFileQuery : IQuery<IFileReadModel?>
{
    public GetFileQuery(long fileId,
        Guid fileReference)
    {
        FileId = fileId;
        FileReference = fileReference;
    }

    public long FileId { get; }
    public Guid FileReference { get; }
}

public class GetJoinedChannelIdListQuery : IQuery<IReadOnlyCollection<long>>
{
    public GetJoinedChannelIdListQuery(long memberUid,
        List<long> channelIdList)
    {
        MemberUid = memberUid;
        ChannelIdList = channelIdList;
    }

    public List<long> ChannelIdList { get; }

    public long MemberUid { get; }
}

public class GetKickedChannelMembersQuery : IQuery<IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetKickedChannelMembersQuery(long channelId,
        int offset,
        int limit)
    {
        ChannelId = channelId;
        Offset = offset;
        Limit = limit;
    }

    public long ChannelId { get; }
    public int Limit { get; }
    public int Offset { get; }
}

public class GetLinkedChannelIdQuery : IQuery<long?>
{
    public GetLinkedChannelIdQuery(long channelId)
    {
        ChannelId = channelId;
    }
    public long ChannelId { get; }
}
public class GetMegaGroupByUidQuery : IQuery<IReadOnlyCollection<IChannelReadModel>>
{
    public GetMegaGroupByUidQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; }
}

public class GetMessageByIdQuery : IQuery<IMessageReadModel?>
{
    public GetMessageByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetMessageIdListQuery : IQuery<List<int>>
{
    public GetMessageIdListQuery(long ownerPeerId,
        long toPeerId,
        int maxMessageId,
        int limit)
    {
        OwnerPeerId = ownerPeerId;
        ToPeerId = toPeerId;
        MaxMessageId = maxMessageId;
        Limit = limit;
    }

    public int Limit { get; }
    public int MaxMessageId { get; }

    public long OwnerPeerId { get; }
    public long ToPeerId { get; }
}

public class GetMessagesByIdListQuery : IQuery<IReadOnlyList<IMessageReadModel>>
{
    public GetMessagesByIdListQuery(IList<string> messageIdList)
    {
        MessageIdList = messageIdList;
    }

    public IList<string> MessageIdList { get; }
}

public class GetMessagesByMessageIdListQuery : IQuery<IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesByMessageIdListQuery(List<int> messageIdList)
    {
        MessageIdList = messageIdList;
    }

    public List<int> MessageIdList { get; }
}

public class GetMessagesByUserIdQuery : IQuery<IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesByUserIdQuery(long ownerPeerId,
        long toPeerId)
    {
        OwnerPeerId = ownerPeerId;
        ToPeerId = toPeerId;
    }

    //public UserId OwnerId { get; }
    public long OwnerPeerId { get; }
    public long ToPeerId { get; }
}

//public class GetDialogByIdQuery : IQuery<IDialogReadModel>
//{
//    public string Id { get; }
//}
public class GetMessagesQuery : IQuery<IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesQuery(long ownerPeerId,
        MessageType messageType,
        string? q,
        List<int>? messageIdList,
        int channelHistoryMinId,
        int limit,
        OffsetInfo? offset,
        Peer? peer,
        long selfUserId,
        int pts,
        int replyToMsgId = 0
    )
    {
        OwnerPeerId = ownerPeerId;
        MessageType = messageType;
        Q = q;
        MessageIdList = messageIdList;
        ChannelHistoryMinId = channelHistoryMinId;
        Limit = limit;
        Offset = offset;
        Peer = peer;
        SelfUserId = selfUserId;
        Pts = pts;
        ReplyToMsgId = replyToMsgId;
    }

    public int ChannelHistoryMinId { get; }
    public bool IsSearchGlobal { get; set; }

    public int Limit { get; }

    public List<int>? MessageIdList { get; }

    //public UserId OwnerId { get; }
    //public int OwnerPeerId { get; private set;}
    public MessageType MessageType { get; }
    public OffsetInfo? Offset { get; set; }

    public long OwnerPeerId { get; }
    public Peer? Peer { get; }
    public int Pts { get; }
    public string? Q { get; }
    public int ReplyToMsgId { get; }
    // use private setter for auto mapper

    public long SelfUserId { get; }
}

public class GetMessageViewsQuery : IQuery<IReadOnlyCollection<MessageView>>
{
    public GetMessageViewsQuery(long channelId,
        List<int> messageIdList)
    {
        ChannelId = channelId;
        MessageIdList = messageIdList;
    }

    public long ChannelId { get; }
    public List<int> MessageIdList { get; }
}

public class GetPeerNotifySettingsByIdQuery : IQuery<IPeerNotifySettingsReadModel>
{
    public GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId id)
    {
        Id = id;
    }

    public PeerNotifySettingsId Id { get; }
}

public class GetPeerNotifySettingsListQuery : IQuery<IReadOnlyCollection<IPeerNotifySettingsReadModel>>
{
    public GetPeerNotifySettingsListQuery(IReadOnlyList<PeerNotifySettingsId> peerNotifySettingsIdList)
    {
        PeerNotifySettingsIdList = peerNotifySettingsIdList;
    }

    public IReadOnlyList<PeerNotifySettingsId> PeerNotifySettingsIdList { get; }
}

public class GetPtsByPeerIdQuery : IQuery<IPtsReadModel?>
{
    public GetPtsByPeerIdQuery(long peerId)
    {
        PeerId = peerId;
    }

    public long PeerId { get; }
}

public class GetPtsByPermAuthKeyIdQuery : IQuery<IPtsForAuthKeyIdReadModel?>
{
    public GetPtsByPermAuthKeyIdQuery(long peerId,
        long permAuthKeyId)
    {
        PeerId = peerId;
        PermAuthKeyId = permAuthKeyId;
    }

    public long PeerId { get; }
    public long PermAuthKeyId { get; }
}

public class GetPushUpdatesByPtsQuery : IQuery<IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetPushUpdatesByPtsQuery(long peerId,
        int pts,
        int limit)
    {
        PeerId = peerId;
        Pts = pts;
        Limit = limit;
    }

    public int Limit { get; }
    public long PeerId { get; }
    public int Pts { get; }
}

public class GetPushUpdatesQuery : IQuery<IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetPushUpdatesQuery(long peerId,
        int minPts,
        int limit)
    {
        PeerId = peerId;
        MinPts = minPts;
        Limit = limit;
    }

    public int Limit { get; }
    public int MinPts { get; }

    public long PeerId { get; }
}

public class GetReadHistoryMessageQuery : IQuery<IMessageReadModel?>
{
    public GetReadHistoryMessageQuery(long ownerPeerId,
        int messageId,
        long toPeerId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        ToPeerId = toPeerId;
    }

    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public long ToPeerId { get; }
}

public class GetReadingHistoryQuery : IQuery<IReadOnlyCollection<long>>
{
    public GetReadingHistoryQuery(long targetPeerId,
        long messageId)
    {
        TargetPeerId = targetPeerId;
        MessageId = messageId;
    }

    public long MessageId { get; }
    public long TargetPeerId { get; }
}
public class GetRepliesQuery : IQuery<IReadOnlyCollection<IReplyReadModel>>
{
    public GetRepliesQuery(long channelId,
        IList<int> messageIds)
    {
        ChannelId = channelId;
        MessageIds = messageIds;
    }
    public long ChannelId { get; }
    public IList<int> MessageIds { get; }
}
public class GetReplyQuery : IQuery<IReplyReadModel?>
{
    public GetReplyQuery(long channelId,
        int savedFromMsgId)
    {
        ChannelId = channelId;
        SavedFromMsgId = savedFromMsgId;
    }
    public long ChannelId { get; }
    public int SavedFromMsgId { get; }
}

public class GetRpcResultByIdQuery : IQuery<IRpcResultReadModel?>
{
    public GetRpcResultByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetUserByIdQuery : IQuery<IUserReadModel?>
{
    public GetUserByIdQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; }

    //public GetUserByIdQuery(UserId userId)
    //{
    //    UserId = userId;
    //}

    //public UserId UserId { get; }
}

public class GetUserByPhoneNumberQuery : IQuery<IUserReadModel?>
{
    public GetUserByPhoneNumberQuery(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}

public class GetUserNameByIdQuery : IQuery<IUserNameReadModel?>
{
    public GetUserNameByIdQuery(
        //PeerType peerType,
        //int peerId,
        string userName)
    {
        //PeerType = peerType;
        //PeerId = peerId;
        UserName = userName;
    }
    //public GetUserNameByIdQuery(string userNameId)
    //{
    //    UserNameId = userNameId;
    //}

    //public string UserNameId { get; }
    //public PeerType PeerType { get; }
    //public int PeerId { get; }
    public string UserName { get; }
}

public class GetUserNameByNameQuery : IQuery<IUserNameReadModel?>
{
    public GetUserNameByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

public class GetUsersByUidListQuery : IQuery<IReadOnlyList<IUserReadModel>>
{
    public GetUsersByUidListQuery(List<long> uidList)
    {
        UidList = uidList;
    }

    public List<long> UidList { get; }
}

public class MessageView
{
    public int MessageId { get; set; }
    public int Views { get; set; }
}

public class SearchUserByKeywordQuery : IQuery<IReadOnlyCollection<IUserReadModel>>
{
    public SearchUserByKeywordQuery(string keyword,
        int limit)
    {
        Keyword = keyword;
        Limit = limit;
    }

    public string Keyword { get; }
    public int Limit { get; }
}

public class SearchUserNameQuery : IQuery<IReadOnlyCollection<IUserNameReadModel>>
{
    public SearchUserNameQuery(string keyword)
    {
        Keyword = keyword;
    }

    public string Keyword { get; }
}

public class GetLatestAppCodeQuery : IQuery<IAppCodeReadModel>
{
    public GetLatestAppCodeQuery(string phoneNumber,
        string phoneCodeHash)
    {
        PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
    }

    public string PhoneCodeHash { get; }

    public string PhoneNumber { get; }
}
public class GetPollIdByMessageIdQuery : IQuery<long?>
{
    public long PeerId { get; }
    public int MessageId { get; }
    public GetPollIdByMessageIdQuery(long peerId, int messageId)
    {
        PeerId = peerId;
        MessageId = messageId;
    }
}
public class GetPollQuery : IQuery<IPollReadModel?>
{
    public long ToPeerId { get; }
    public long PollId { get; }
    public GetPollQuery(long toPeerId, long pollId)
    {
        ToPeerId = toPeerId;
        PollId = pollId;
    }
}
public class GetChosenVoteAnswersQuery : IQuery<IReadOnlyCollection<IPollAnswerVoterReadModel>>
{
    public List<long> PollIds { get; }
    public long VoterPeerId { get; }
    public GetChosenVoteAnswersQuery(List<long> pollIds, long voterPeerId)
    {
        PollIds = pollIds;
        VoterPeerId = voterPeerId;
    }
}
public class GetPollAnswerVotersQuery : IQuery<IReadOnlyCollection<IPollAnswerVoterReadModel>>
{
    public long PollId { get; }
    public long VoterPeerId { get; }
    public GetPollAnswerVotersQuery(long pollId, long voterPeerId)
    {
        PollId = pollId;
        VoterPeerId = voterPeerId;
    }
}
public class GetPollsQuery : IQuery<IReadOnlyCollection<IPollReadModel>>
{
    public List<long> PollIds { get; }
    public GetPollsQuery(List<long> pollIds)
    {
        PollIds = pollIds;
    }
}

public record GetMessageIdListByUserIdQuery(long ChannelId, long SenderUserId, int Limit) : IQuery<IReadOnlyCollection<int>>;
public record GetMessageIdListByChannelIdQuery(long ChannelId, int Limit) : IQuery<IReadOnlyCollection<int>>;