using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelState : AggregateState<ChannelAggregate, ChannelId, ChannelState>,
    IApply<ChannelCreatedEvent>,
    IApply<StartSendChannelMessageEvent>,
    IApply<StartInviteToChannelEvent>,
    IApply<IncrementParticipantCountEvent>,
    IApply<SetDiscussionGroupEvent>,
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
    IApply<DeleteParticipantHistoryStartedEvent>,
    IApply<ChannelColorUpdatedEvent>,
    IApply<ChatJoinRequestHiddenEvent>,
    IApply<ChatInviteRequestPendingUpdatedEvent>
{
    //private List<ChatAdmin> _adminList = new();
    //public IReadOnlyList<ChatAdmin> AdminList => _adminList.AsReadOnly();

    //public List<ChatAdmin> AdminList { get; set; } = new();
    public ConcurrentDictionary<long, ChatAdmin> ChatAdmins { get; private set; } = new();

    //private List<long> _botUidList = new();
    public static ChatBannedRights InitRights => ChatBannedRights.Default;
    //public List<long> BotUidList { get; private set; } = new();
    public List<long> BotUserIdList { get; private set; } = new();

    public bool Broadcast { get; private set; }
    public long ChannelId { get; private set; }

    public long CreatorId { get; private set; }

    public ChatBannedRights? DefaultBannedRights { get; private set; }
    public int LastSendDate { get; private set; }
    public int LatestNoneBotSenderMessageId { get; private set; }

    public long LatestNoneBotSenderPeerId { get; private set; }
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
    //public HashSet<long> RecentRequesters { get; private set; } = new();
    public int? RequestsPending { get; private set; }
    public List<long>? RecentRequesters { get; private set; } = new(MyTelegramServerDomainConsts.ChatInviteRecentRequesterMaxCount);

    public bool SignatureEnabled { get; private set; }
    public int ParticipantCount { get; private set; }
    public PeerColor? Color { get; private set; }

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
                //AdminList.Remove(admin);
                ChatAdmins.TryRemove(admin.UserId, out _);
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

    public void Apply(ChannelCreatedEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        CreatorId = aggregateEvent.CreatorId;
        Broadcast = aggregateEvent.Broadcast;
        TtlPeriod = aggregateEvent.TtlPeriod;
        PhotoId = aggregateEvent.PhotoId;
        MigratedFromChatId = aggregateEvent.MigratedFromChatId;
        MigratedMaxId = aggregateEvent.MigratedMaxId;
        ParticipantCount = 1;

        ChatAdmins.TryAdd(CreatorId, new ChatAdmin(CreatorId, true, CreatorId, ChatAdminRights.GetCreatorRights(), string.Empty));
    }

    public void Apply(ChannelDefaultBannedRightsEditedEvent aggregateEvent)
    {
        DefaultBannedRights = aggregateEvent.DefaultBannedRights;
    }

    public void Apply(ChannelPhotoEditedEvent aggregateEvent)
    {
        //Photo = aggregateEvent.Photo;
        PhotoId = aggregateEvent.PhotoId;
    }

    public void Apply(ChannelTitleEditedEvent aggregateEvent)
    {
    }

    public void Apply(ChannelUserNameChangedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserName;
    }

    public void Apply(CheckChannelStateCompletedEvent aggregateEvent)
    {
        LatestNoneBotSenderPeerId = aggregateEvent.SenderPeerId;
        LatestNoneBotSenderMessageId = aggregateEvent.MessageId;

        LastSendDate = aggregateEvent.Date;
    }


    public void Apply(IncrementParticipantCountEvent aggregateEvent)
    {
        ParticipantCount = aggregateEvent.NewParticipantCount;
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

    public void Apply(SetDiscussionGroupEvent aggregateEvent)
    {
        LinkedChannelId = aggregateEvent.GroupChannelId;
    }

    public void Apply(SlowModeChangedEvent aggregateEvent)
    {
        SlowModeSeconds = aggregateEvent.Seconds;
    }

    public void Apply(StartInviteToChannelEvent aggregateEvent)
    {
        foreach (var memberUid in aggregateEvent.MemberUidList)
        {
            if (memberUid > MyTelegramServerDomainConsts.BotUserInitId)
            {
                if (!BotUserIdList.Contains(memberUid))
                {
                    BotUserIdList.Add(memberUid);
                }
            }

            //if (!_botUidList.Contains(memberUid))
            //{
            //    _botUidList.Add(memberUid);
            //}
        }
    }

    public void Apply(StartSendChannelMessageEvent aggregateEvent)
    {
        MaxMessageId = aggregateEvent.MessageId;
        if (!aggregateEvent.SenderIsBot)
        {
            LatestNoneBotSenderPeerId = aggregateEvent.SenderPeerId;
            LatestNoneBotSenderMessageId = aggregateEvent.MessageId;
        }

        LastSendDate = aggregateEvent.Date;
    }

    public ChatAdmin? GetAdmin(long adminId)
    {
        ChatAdmins.TryGetValue(adminId, out var admin);

        return admin;
        //var admin = AdminList.FirstOrDefault(p => p.UserId == adminId);

        //return admin;
    }

    public ChatBannedRights GetDefaultBannedRights()
    {
        return DefaultBannedRights ?? InitRights;
    }

    public void LoadFromSnapshot(ChannelSnapshot snapshot)
    {
        ChannelId = snapshot.ChannelId;
        Broadcast = snapshot.Broadcast;
        CreatorId = snapshot.CreatorUid;
        MaxMessageId = snapshot.MaxMessageId;
        PreHistoryHidden = snapshot.PreHistoryHidden;
        //_botUidList = new List<long>(snapshot.BotUidList);
        BotUserIdList = new List<long>(snapshot.BotUidList);
        LatestNoneBotSenderPeerId = snapshot.LatestNoneBotSenderPeerId;
        LatestNoneBotSenderMessageId = snapshot.LatestNoneBotSenderMessageId;
        DefaultBannedRights = snapshot.DefaultBannedRights;
        SlowModeSeconds = snapshot.SlowModeSeconds;
        LastSendDate = snapshot.LastSendDate;
        //AdminList = new List<ChatAdmin>(snapshot.AdminList);
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

        ChatAdmins = new();
        foreach (var chatAdmin in snapshot.AdminList)
        {
            ChatAdmins.TryAdd(chatAdmin.UserId, chatAdmin);
        }

        RequestsPending = snapshot.RequestsPending;
        RecentRequesters = snapshot.RecentRequesters ?? new List<long>();
        SignatureEnabled = snapshot.SignatureEnabled;

        ParticipantCount = snapshot.ParticipantCount;
    }

    public void Apply(DeleteParticipantHistoryStartedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }


    public void Apply(ChannelColorUpdatedEvent aggregateEvent)
    {
        Color = aggregateEvent.Color;
    }

    public void Apply(ChatJoinRequestHiddenEvent aggregateEvent)
    {
        RequestsPending = aggregateEvent.RequestsPending;
        RecentRequesters = aggregateEvent.RecentRequesters;
    }

    public void Apply(ChatInviteRequestPendingUpdatedEvent aggregateEvent)
    {
        RequestsPending = aggregateEvent.RequestsPending;
        RecentRequesters = aggregateEvent.RecentRequesters;
    }
}
