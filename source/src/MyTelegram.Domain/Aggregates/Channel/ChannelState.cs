namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelState : AggregateState<ChannelAggregate, ChannelId, ChannelState>,
    IApply<ChannelCreatedEvent>,
    //IApply<StartSendChannelMessageEvent>,
    //IApply<StartInviteToChannelEvent>,
    IApply<IncrementParticipantCountEvent>,
    IApply<DiscussionGroupUpdatedEvent>,
    IApply<ReadChannelLatestNoneBotOutboxMessageEvent>,
    IApply<ChannelTitleEditedEvent>,
    IApply<ChannelAboutEditedEvent>,
    IApply<ChannelDefaultBannedRightsEditedEvent>,
    IApply<SetChannelPtsEvent>,
    IApply<SlowModeChangedEvent>,
    IApply<PreHistoryHiddenChangedEvent>,
    IApply<ChannelAdminRightsEditedEvent>,
    IApply<NewMsgIdPinnedEvent>,
    IApply<ChannelPhotoEditedEvent>,
    IApply<ChannelUserNameChangedEvent>,
    IApply<CheckChannelStateCompletedEvent>,
    IApply<ChannelNoForwardsChangedEvent>,
    //IApply<ChatJoinRequestHiddenEvent>,
    IApply<ChannelSignatureChangedEvent>,
    IApply<ChannelColorUpdatedEvent>,
    //IApply<ChatInviteRequestPendingUpdatedEvent>,
    IApply<LinkedChannelChangedEvent>,
    IApply<ChannelDeletedEvent>,
    IApply<ChannelParticipantCountChangedEvent>,
    IApply<ChannelTopMessageIdUpdatedEvent>,
    IApply<ChannelParticipantsHiddenUpdatedEvent>,
    IApply<ChannelJoinRequestUpdatedEvent>,
    IApply<ChannelAdminRemovedEvent>
{
    public Dictionary<long, ChatAdmin> ChatAdmins { get; private set; } = [];
    public static ChatBannedRights InitRights => ChatBannedRights.CreateDefaultBannedRights();
    public List<long> BotUserIdList { get; private set; } = [];
    public bool Broadcast { get; private set; }
    public long ChannelId { get; private set; }
    public long AccessHash { get; private set; }
    public string Title { get; private set; } = null!;
    public long CreatorId { get; private set; }
    public ChatBannedRights? DefaultBannedRights { get; private set; }
    public int LastSendDate { get; private set; }
    public int LatestNonBotSenderMessageId { get; private set; }
    public long LatestNonBotSenderPeerId { get; private set; }
    public long? LinkedChannelId { get; private set; }
    public int MaxMessageId { get; private set; }
    public byte[]? Photo { get; private set; }
    public int PinnedMsgId { get; private set; }
    public bool PreHistoryHidden { get; private set; }
    public int SlowModeSeconds { get; private set; }
    public string? UserName { get; private set; }
    public bool Forum { get; private set; }
    public int MaxTopicId { get; private set; }
    public int? TtlPeriod { get; private set; }
    public long? PhotoId { get; private set; }
    public long? MigratedFromChatId { get; private set; }
    public int? MigratedMaxId { get; private set; }
    public bool NoForwards { get; private set; }
    public bool IsFirstChatInviteCreated { get; private set; }
    public int? RequestsPending { get; private set; }
    public List<long>? RecentRequesters { get; private set; } =
        new(MyTelegramConsts.ChatInviteRecentRequesterMaxCount);
    public bool SignatureEnabled { get; private set; }
    public int ParticipantCount { get; private set; }
    public PeerColor? Color { get; private set; }
    public bool HasLink { get; private set; }
    public bool IsDeleted { get; private set; }
    public long? WallPaperId { get; private set; }
    public string? ThemeEmoticon { get; private set; }
    public WallPaperSettings? WallPaperSettings { get; private set; }
    public bool IsGeoGroup { get; private set; }
    public int TopMessageId { get; private set; }
    public long? StickerSetId { get; private set; }
    public long? EmojiStickerSetId { get; private set; }
    public EmojiStatus? EmojiStatus { get; private set; }
    public bool Verified { get; private set; }
    public bool ParticipantsHidden { get; private set; }
    public bool JoinRequest { get; private set; }
    public int Date { get; private set; }

    public void Apply(ChannelAboutEditedEvent aggregateEvent)
    {
    }

    public void Apply(ChannelAdminRightsEditedEvent aggregateEvent)
    {
        var admin = GetAdmin(aggregateEvent.UserId);
        if (admin != null)
        {
            if (aggregateEvent.AdminRights.HasNoRights())
            {
                ChatAdmins.Remove(admin.UserId, out _);
            }
            else
            {
                admin.SetAdminRights(aggregateEvent.AdminRights);
            }
        }
        else
        {
            admin = new ChatAdmin(aggregateEvent.PromotedBy,
                aggregateEvent.CanEdit,
                aggregateEvent.UserId,
                aggregateEvent.AdminRights,
                aggregateEvent.Rank);
            ChatAdmins.TryAdd(admin.UserId, admin);

            if (aggregateEvent.IsBot)
            {
                BotUserIdList.Add(aggregateEvent.UserId);
            }
        }
    }

    public void Apply(ChannelColorUpdatedEvent aggregateEvent)
    {
        Color = aggregateEvent.Color;
    }

    public void Apply(ChannelCreatedEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        AccessHash = aggregateEvent.AccessHash;
        Title = aggregateEvent.Title;
        CreatorId = aggregateEvent.CreatorId;
        Broadcast = aggregateEvent.Broadcast;
        TtlPeriod = aggregateEvent.TtlPeriod;
        PhotoId = aggregateEvent.PhotoId;
        MigratedFromChatId = aggregateEvent.MigratedFromChatId;
        MigratedMaxId = aggregateEvent.MigratedMaxId;
        ParticipantCount = 1;
        IsGeoGroup = aggregateEvent.GeoPoint != null;
        ChatAdmins = new Dictionary<long, ChatAdmin>
        {
            { CreatorId, new ChatAdmin(CreatorId, true, CreatorId, ChatAdminRights.GetCreatorRights(), string.Empty) }
        };
        Date = aggregateEvent.Date;
    }

    public void Apply(ChannelDefaultBannedRightsEditedEvent aggregateEvent)
    {
        DefaultBannedRights = aggregateEvent.DefaultBannedRights;
    }

    public void Apply(ChannelDeletedEvent aggregateEvent)
    {
        IsDeleted = true;
    }

    public void Apply(ChannelNoForwardsChangedEvent aggregateEvent)
    {
        NoForwards = aggregateEvent.Enabled;
    }

    public void Apply(ChannelParticipantCountChangedEvent aggregateEvent)
    {
        ParticipantCount = aggregateEvent.NewParticipantCount;
    }

    public void Apply(ChannelPhotoEditedEvent aggregateEvent)
    {
        PhotoId = aggregateEvent.PhotoId;
    }

    public void Apply(ChannelSignatureChangedEvent aggregateEvent)
    {
        SignatureEnabled = aggregateEvent.SignatureEnabled;
    }

    public void Apply(ChannelTitleEditedEvent aggregateEvent)
    {
    }

    public void Apply(ChannelUserNameChangedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserName;
    }

    //public void Apply(ChatInviteRequestPendingUpdatedEvent aggregateEvent)
    //{
    //    RequestsPending = aggregateEvent.RequestsPending;
    //    RecentRequesters = aggregateEvent.RecentRequesters;
    //}

    //public void Apply(ChatJoinRequestHiddenEvent aggregateEvent)
    //{
    //    RequestsPending = aggregateEvent.RequestsPending;
    //    RecentRequesters = aggregateEvent.RecentRequesters;
    //}

    public void Apply(CheckChannelStateCompletedEvent aggregateEvent)
    {
        LatestNonBotSenderPeerId = aggregateEvent.SenderPeerId;
        LatestNonBotSenderMessageId = aggregateEvent.MessageId;

        LastSendDate = aggregateEvent.Date;
    }

    public void Apply(DiscussionGroupUpdatedEvent aggregateEvent)
    {
        LinkedChannelId = aggregateEvent.GroupChannelId;
    }

    public void Apply(IncrementParticipantCountEvent aggregateEvent)
    {
        ParticipantCount = aggregateEvent.NewParticipantCount;
    }

    public void Apply(LinkedChannelChangedEvent aggregateEvent)
    {
        LinkedChannelId = aggregateEvent.LinkedChannelId;
        HasLink = aggregateEvent.LinkedChannelId.HasValue;
    }

    public void Apply(NewMsgIdPinnedEvent aggregateEvent)
    {
        PinnedMsgId = aggregateEvent.PinnedMsgId;
    }

    public void Apply(PreHistoryHiddenChangedEvent aggregateEvent)
    {
        PreHistoryHidden = aggregateEvent.Hidden;
    }

    public void Apply(ReadChannelLatestNoneBotOutboxMessageEvent aggregateEvent)
    {
    }

    public void Apply(SetChannelPtsEvent aggregateEvent)
    {
    }

    public void Apply(SlowModeChangedEvent aggregateEvent)
    {
        SlowModeSeconds = aggregateEvent.Seconds;
    }

    //public void Apply(StartInviteToChannelEvent aggregateEvent)
    //{
    //    foreach (var memberUid in aggregateEvent.MemberUidList)
    //    {
    //        if (memberUid > MyTelegramConsts.BotUserInitId)
    //        {
    //            if (!BotUserIdList.Contains(memberUid))
    //            {
    //                BotUserIdList.Add(memberUid);
    //            }
    //        }
    //    }
    //}

    //public void Apply(StartSendChannelMessageEvent aggregateEvent)
    //{
    //    MaxMessageId = aggregateEvent.MessageId;
    //    if (!aggregateEvent.SenderIsBot)
    //    {
    //        LatestNonBotSenderPeerId = aggregateEvent.SenderPeerId;
    //        LatestNonBotSenderMessageId = aggregateEvent.MessageId;
    //    }

    //    LastSendDate = aggregateEvent.Date;
    //}

    public ChatAdmin? GetAdmin(long adminId)
    {
        ChatAdmins.TryGetValue(adminId, out var admin);

        return admin;
    }

    public ChatBannedRights GetDefaultBannedRights()
    {
        return DefaultBannedRights ?? InitRights;
    }

    public void LoadFromSnapshot(ChannelSnapshot snapshot)
    {
        ChannelId = snapshot.ChannelId;
        AccessHash = snapshot.AccessHash;
        Title = snapshot.Title;
        Broadcast = snapshot.Broadcast;
        CreatorId = snapshot.CreatorUid;
        MaxMessageId = snapshot.MaxMessageId;
        PreHistoryHidden = snapshot.PreHistoryHidden;
        BotUserIdList = new List<long>(snapshot.BotUidList);
        LatestNonBotSenderPeerId = snapshot.LatestNoneBotSenderPeerId;
        LatestNonBotSenderMessageId = snapshot.LatestNoneBotSenderMessageId;
        DefaultBannedRights = snapshot.DefaultBannedRights;
        SlowModeSeconds = snapshot.SlowModeSeconds;
        LastSendDate = snapshot.LastSendDate;
        PinnedMsgId = snapshot.PinnedMsgId;
        Photo = snapshot.Photo;
        LinkedChannelId = snapshot.LinkedChannelId;
        UserName = snapshot.UserName;
        Forum = snapshot.Forum;
        MaxTopicId = snapshot.MaxTopicId;
        TtlPeriod = snapshot.TtlPeriod;
        MigratedFromChatId = snapshot.MigratedFromChatId;
        MigratedMaxId = snapshot.MigratedMaxId;
        NoForwards = snapshot.NoForwards;
        IsFirstChatInviteCreated = snapshot.IsFirstChatInviteCreated;

        ChatAdmins = snapshot.AdminList.ToDictionary(k => k.UserId);

        RequestsPending = snapshot.RequestsPending;
        RecentRequesters = snapshot.RecentRequesters ?? new List<long>();
        SignatureEnabled = snapshot.SignatureEnabled;

        ParticipantCount = snapshot.ParticipantCount;
        Color = snapshot.Color;
        HasLink = snapshot.HasLink;
        IsDeleted = snapshot.IsDeleted;

        WallPaperId = snapshot.WallPaperId;
        ThemeEmoticon = snapshot.ThemeEmoticon;
        WallPaperSettings = snapshot.WallPaperSettings;
        IsGeoGroup = snapshot.IsGeoGroup;
        TopMessageId = snapshot.TopMessageId;
        StickerSetId = snapshot.StickerSetId;
        EmojiStickerSetId = snapshot.EmojiStickerSetId;
        EmojiStatus = snapshot.EmojiStatus;
        ParticipantsHidden = snapshot.ParticipantsHidden;
        JoinRequest = snapshot.JoinRequest;
    }
    public void Apply(ChannelTopMessageIdUpdatedEvent aggregateEvent)
    {
        TopMessageId = aggregateEvent.TopMessageId;
    }
    public void Apply(ChannelParticipantsHiddenUpdatedEvent aggregateEvent)
    {
        ParticipantsHidden = aggregateEvent.Enabled;
    }
    public void Apply(ChannelJoinRequestUpdatedEvent aggregateEvent)
    {
        JoinRequest = aggregateEvent.Enabled;
    }
    public void Apply(ChannelAdminRemovedEvent aggregateEvent)
    {
        ChatAdmins = aggregateEvent.AdminList.ToDictionary(k => k.UserId);
    }
}