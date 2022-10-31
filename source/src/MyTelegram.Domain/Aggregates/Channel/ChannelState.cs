namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelState : AggregateState<ChannelAggregate, ChannelId, ChannelState>,
    IApply<ChannelCreatedEvent>,
    IApply<ExportChatInviteEvent>,
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
	IApply<DeleteParticipantHistoryStartedEvent>
{
    private List<ChatAdmin> _adminList = new();
    private List<long> _botUidList = new();
    public static ChatBannedRights InitRights => ChatBannedRights.Default;
    public IReadOnlyList<ChatAdmin> AdminList => _adminList.AsReadOnly();
    public IReadOnlyList<long> BotUidList => _botUidList.AsReadOnly();
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
                _adminList.Remove(admin);
            }
            else
            {
                admin.SetAdminRights(aggregateEvent.AdminRights);
            }
        }
        else
        {
            _adminList.Add(new ChatAdmin(aggregateEvent.PromotedBy,
                aggregateEvent.CanEdit,
                aggregateEvent.UserId,
                aggregateEvent.AdminRights,
                aggregateEvent.Rank));
        }
    }

    public void Apply(ChannelCreatedEvent aggregateEvent)
    {
        ChannelId = aggregateEvent.ChannelId;
        CreatorId = aggregateEvent.CreatorId;
        Broadcast = aggregateEvent.Broadcast;
    }

    public void Apply(ChannelDefaultBannedRightsEditedEvent aggregateEvent)
    {
        DefaultBannedRights = aggregateEvent.DefaultBannedRights;
    }

    public void Apply(ChannelPhotoEditedEvent aggregateEvent)
    {
        Photo = aggregateEvent.Photo;
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

    public void Apply(ExportChatInviteEvent aggregateEvent)
    {
    }

    public void Apply(IncrementParticipantCountEvent aggregateEvent)
    {
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
            if (memberUid <= MyTelegramServerDomainConsts.BotUserInitId)
            {
                continue;
            }

            if (!_botUidList.Contains(memberUid))
            {
                _botUidList.Add(memberUid);
            }
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
        var admin = _adminList.FirstOrDefault(p => p.UserId == adminId);

        return admin;
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
        _botUidList = new List<long>(snapshot.BotUidList);
        LatestNoneBotSenderPeerId = snapshot.LatestNoneBotSenderPeerId;
        LatestNoneBotSenderMessageId = snapshot.LatestNoneBotSenderMessageId;
        DefaultBannedRights = snapshot.DefaultBannedRights;
        SlowModeSeconds = snapshot.SlowModeSeconds;
        LastSendDate = snapshot.LastSendDate;
        _adminList = new List<ChatAdmin>(snapshot.AdminList);
        PinnedMsgId = snapshot.PinnedMsgId;
        Photo = snapshot.Photo;
        LinkedChannelId = snapshot.LinkedChannelId;
        UserName = snapshot.UserName;
    }
    public void Apply(DeleteParticipantHistoryStartedEvent aggregateEvent)
    {
    }
}
