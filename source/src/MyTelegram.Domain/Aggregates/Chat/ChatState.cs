using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatState : AggregateState<ChatAggregate, ChatId, ChatState>,
    IApply<ChatCreatedEvent>,
    IApply<StartSendChatMessageEvent>,
    IApply<ReadLatestNoneBotOutboxMessageEvent>,
    IApply<ChatMemberAddedEvent>,
    IApply<ChatMemberDeletedEvent>,
    IApply<ChatDefaultBannedRightsEditedEvent>,
    IApply<NewChatMsgIdPinnedEvent>,
    IApply<ChatPhotoEditedEvent>,
    IApply<ChatAboutEditedEvent>,
    IApply<ChatTitleEditedEvent>,
    IApply<CheckChatStateCompletedEvent>,
    IApply<ChatDeletedEvent>,
    IApply<DeleteChatMessagesStartedEvent>,
    IApply<ChatAdminRightsEditedEvent>
{
    //private List<ChatMember> _chatMembers = new();

    public string? About { get; private set; }

    public long ChatId { get; private set; }

    //public IReadOnlyList<ChatMember> ChatMembers => _chatMembers.AsReadOnly();
    //public List<ChatMember> ChatMembers = new();
    //public List<long> ChatMemberUserIdList { get; private set; } = new();

    public long CreatorId { get; private set; }

    public ChatBannedRights? DefaultBannedRights { get; private set; }
    public static ChatBannedRights InitRights { get; } = ChatBannedRights.Default;

    public long LatestDeletedMemberUid { get; private set; }
    public int LatestSenderMessageId { get; private set; }

    public long LatestSenderPeerId { get; private set; }

    //public IReadOnlyList<long> MemberUidList => _chatMembers.Select(p => p.UserId).ToList();

    //public byte[]? Photo { get; private set; }
    public long? PhotoId { get; private set; }

    public string Title { get; private set; } = default!;
    public bool IsDeleted { get; private set; }
    public int? TtlPeriod { get; private set; }
    public long? MigrateToChannelId { get; private set; }
    public bool NoForwards { get; private set; }

    //public List<ChatAdmin> AdminList { get; private set; } = new();
    public List<long> BotUserIdList { get; private set; } = new();
    public ConcurrentDictionary<long, ChatMember> ChatMembers { get; private set; } = new();
    public ConcurrentDictionary<long, ChatAdmin> ChatAdmins { get; private set; } = new();

    public void Apply(ChatAboutEditedEvent aggregateEvent)
    {
        About = aggregateEvent.About;
    }

    public void Apply(ChatCreatedEvent aggregateEvent)
    {
        ChatId = aggregateEvent.ChatId;
        Title = aggregateEvent.Title;
        CreatorId = aggregateEvent.CreatorUid;
        foreach (var member in aggregateEvent.MemberUidList)
        {
            AddChatMember(member);
        }
        TtlPeriod = aggregateEvent.TtlPeriod;
    }

    public void Apply(ChatDefaultBannedRightsEditedEvent aggregateEvent)
    {
        DefaultBannedRights = aggregateEvent.DefaultBannedRights;
    }

    public void Apply(ChatMemberAddedEvent aggregateEvent)
    {
        AddChatMember(aggregateEvent.ChatMember);
    }

    public void Apply(ChatMemberDeletedEvent aggregateEvent)
    {
        //_chatMembers.RemoveAll(p => p.UserId == aggregateEvent.UserId);
        //ChatMembers.RemoveAll(p => p.UserId == aggregateEvent.UserId);
        ChatMembers.TryRemove(aggregateEvent.UserId, out _);
        LatestDeletedMemberUid = aggregateEvent.UserId;
    }

    public void Apply(ChatPhotoEditedEvent aggregateEvent)
    {
        //Photo = aggregateEvent.Photo;
        PhotoId = aggregateEvent.PhotoId;
    }

    public void Apply(ChatTitleEditedEvent aggregateEvent)
    {
        Title = aggregateEvent.Title;
    }

    public void Apply(CheckChatStateCompletedEvent aggregateEvent)
    {
    }

    public void Apply(NewChatMsgIdPinnedEvent aggregateEvent)
    {
    }

    public void Apply(ReadLatestNoneBotOutboxMessageEvent aggregateEvent)
    {
    }

    public void Apply(StartSendChatMessageEvent aggregateEvent)
    {
        if (!aggregateEvent.SenderIsBot)
        {
            LatestSenderPeerId = aggregateEvent.SenderPeerId;
            LatestSenderMessageId = aggregateEvent.SenderMessageId;
        }

        LatestDeletedMemberUid = 0;
    }

    public void AddChatMember(ChatMember member)
    {
        //if (_chatMembers.All(p => p.UserId != member.UserId))
        //{
        //    _chatMembers.Add(member);
        //}

    }

    public ChatBannedRights GetDefaultBannedRights()
    {
        return DefaultBannedRights ?? InitRights;
    }

    public ChatAdmin? GetAdmin(long adminId)
    {
        ChatAdmins.TryGetValue(adminId, out var admin);

        return admin;
        //var admin = AdminList.FirstOrDefault(p => p.UserId == adminId);

        //return admin;
    }

    public void LoadSnapshot(ChatSnapshot snapshot)
    {
        ChatId = snapshot.ChatId;
        Title = snapshot.Title;
        CreatorId = snapshot.CreatorUid;
        //_chatMembers = new List<ChatMember>(snapshot.ChatMemberList);


        LatestSenderMessageId = snapshot.LatestSenderMessageId;
        LatestSenderPeerId = snapshot.LatestSenderPeerId;
        LatestDeletedMemberUid = snapshot.LatestDeletedUserId;
        DefaultBannedRights = snapshot.DefaultBannedRights;
        About = snapshot.About;
        TtlPeriod = snapshot.TtlPeriod;
        MigrateToChannelId = snapshot.MigrateToChannelId;
        PhotoId = snapshot.PhotoId;
        NoForwards = snapshot.NoForwards;

        ChatMembers = new();
        //AdminList=new List<ChatAdmin>(snapshot.ChatAdmins)
        BotUserIdList = snapshot.BotUserIds;
        foreach (var chatMember in snapshot.ChatMembers)
        {
            ChatMembers.TryAdd(chatMember.UserId, chatMember);
        }

        foreach (var chatAdmin in snapshot.ChatAdmins)
        {
            ChatAdmins.TryAdd(chatAdmin.UserId, chatAdmin);
        }
    }

    public void Apply(ChatDeletedEvent aggregateEvent)
    {
        IsDeleted = true;
    }

    public void Apply(DeleteChatMessagesStartedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ChatAdminRightsEditedEvent aggregateEvent)
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
}
