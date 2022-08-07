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
    IApply<ChatDeletedEvent>
{
    private List<ChatMember> _chatMembers = new();

    public string? About { get; private set; }

    public long ChatId { get; private set; }

    public IReadOnlyList<ChatMember> ChatMembers => _chatMembers.AsReadOnly();

    public long CreatorUid { get; private set; }

    public ChatBannedRights? DefaultBannedRights { get; private set; }
    public static ChatBannedRights InitRights { get; } = ChatBannedRights.Default;

    public long LatestDeletedMemberUid { get; private set; }
    public int LatestSenderMessageId { get; private set; }

    public long LatestSenderPeerId { get; private set; }

    public IReadOnlyList<long> MemberUidList => _chatMembers.Select(p => p.UserId).ToList();

    public byte[]? Photo { get; private set; }

    public string Title { get; private set; } = default!;
    public bool IsDeleted { get; private set; }
    public void Apply(ChatAboutEditedEvent aggregateEvent)
    {
        About = aggregateEvent.About;
    }

    public void Apply(ChatCreatedEvent aggregateEvent)
    {
        ChatId = aggregateEvent.ChatId;
        Title = aggregateEvent.Title;
        CreatorUid = aggregateEvent.CreatorUid;
        foreach (var member in aggregateEvent.MemberUidList)
        {
            AddChatMember(member);
        }
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
        _chatMembers.RemoveAll(p => p.UserId == aggregateEvent.UserId);
        LatestDeletedMemberUid = aggregateEvent.UserId;
    }

    public void Apply(ChatPhotoEditedEvent aggregateEvent)
    {
        Photo = aggregateEvent.Photo;
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
        if (_chatMembers.All(p => p.UserId != member.UserId))
        {
            _chatMembers.Add(member);
        }
    }

    public ChatBannedRights GetDefaultBannedRights()
    {
        return DefaultBannedRights ?? InitRights;
    }

    public void LoadSnapshot(ChatSnapshot snapshot)
    {
        ChatId = snapshot.ChatId;
        Title = snapshot.Title;
        CreatorUid = snapshot.CreatorUid;
        _chatMembers = new List<ChatMember>(snapshot.ChatMemberList);
        LatestSenderMessageId = snapshot.LatestSenderMessageId;
        LatestSenderPeerId = snapshot.LatestSenderPeerId;
        LatestDeletedMemberUid = snapshot.LatestDeletedUserId;
        DefaultBannedRights = snapshot.DefaultBannedRights;
        About = snapshot.About;
    }
    public void Apply(ChatDeletedEvent aggregateEvent)
    {
        IsDeleted = true;
    }
}
