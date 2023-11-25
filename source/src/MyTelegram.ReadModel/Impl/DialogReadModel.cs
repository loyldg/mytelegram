using SendOutboxMessageCompletedEvent = MyTelegram.Domain.Sagas.Events.SendOutboxMessageCompletedEvent;

namespace MyTelegram.ReadModel.Impl;

public class DialogReadModel : IDialogReadModel,
    IAmReadModelFor<DialogAggregate, DialogId, DialogCreatedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, SetOutboxTopMessageSuccessEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, InboxMessageReceivedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, DraftSavedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, ReadInboxMessage2Event>,
    IAmReadModelFor<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, ReadChannelInboxMessageEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, HistoryClearedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, ParticipantHistoryClearedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, DialogPinChangedEvent>,
    IAmReadModelFor<DialogAggregate, DialogId, PinnedOrderChangedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
    IAmReadModelFor<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent>
{
    public virtual int ChannelHistoryMinId { get; private set; }
    public virtual DateTime CreationTime { get; private set; }
    public virtual Draft? Draft { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual int MaxSendOutMessageId { get; private set; }
    public virtual PeerNotifySettings? NotifySettings { get; protected set; }
    public virtual long OwnerId { get; private set; }
    //public virtual string TopMessageBoxId { get; private set; }
    public virtual bool Pinned { get; private set; }

    public virtual int PinnedMsgId { get; private set; }
    //public int Date { get; private set; }
    //public string Message { get; private set; }
    //public bool NoWebpage { get; private set; }
    //public int ReplyToMsgId { get; private set; }
    //public byte[] Entities { get; private set; }
    //#endregion
    ///// <summary>
    ///// only for channel
    ///// </summary>
    //public int NewTopMessageId { get; private set; }
    public virtual int PinnedOrder { get; private set; }

    public virtual int Pts { get; private set; }
    public virtual int ReadInboxMaxId { get; private set; }
    //public Draft Draft => new(Message, NoWebpage, ReplyToMsgId, Date, Entities);
    public virtual int ReadOutboxMaxId { get; private set; }

    public virtual long ToPeerId { get; private set; }
    public virtual PeerType ToPeerType { get; private set; }
    public virtual int TopMessage { get; private set; }
    public virtual int UnreadCount { get; private set; }
    public virtual long? Version { get; set; }
    public virtual bool IsDeleted { get; private set; }
    public int? TtlPeriod { get; private set; }
    public int UnreadMentionsCount { get; private set; }
    public int UnreadReactionsCount { get; private set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ChannelHistoryMinId = domainEvent.AggregateEvent.HistoryMinId;
        IsDeleted = true;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, DialogCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerId = domainEvent.AggregateEvent.OwnerId;
        ToPeerType = domainEvent.AggregateEvent.ToPeer.PeerType;
        ToPeerId = domainEvent.AggregateEvent.ToPeer.PeerId;
        ChannelHistoryMinId = domainEvent.AggregateEvent.ChannelHistoryMinId;
        CreationTime = domainEvent.AggregateEvent.CreationTime;
        TopMessage = domainEvent.AggregateEvent.TopMessageId;
        //TopMessageBoxId = domainEvent.AggregateEvent.TopMessageBoxId;
        Pts = 0;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, DialogPinChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Pinned = domainEvent.AggregateEvent.Pinned;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, DraftSavedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        Draft = domainEvent.AggregateEvent.Draft;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, HistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ChannelHistoryMinId = domainEvent.AggregateEvent.HistoryMinId;

        IsDeleted = true;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, InboxMessageReceivedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerId = domainEvent.AggregateEvent.OwnerPeerId;

        TopMessage = domainEvent.AggregateEvent.MessageId;
        //TopMessageBoxId = domainEvent.AggregateEvent.MessageBoxId.Value;
        ToPeerType = domainEvent.AggregateEvent.ToPeer.PeerType;
        ToPeerId = domainEvent.AggregateEvent.ToPeer.PeerId;

        UnreadCount++;
        if (!Version.HasValue)
        {
            CreationTime = DateTime.UtcNow;
        }

        IsDeleted = false;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        ReadOutboxMaxId = domainEvent.AggregateEvent.MaxMessageId;
        if (TopMessage < ReadOutboxMaxId)
        {
            TopMessage = ReadOutboxMaxId;
        }

        IsDeleted = false;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, ParticipantHistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ChannelHistoryMinId = domainEvent.AggregateEvent.HistoryMinId;
        IsDeleted = true;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, PinnedOrderChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        PinnedOrder = domainEvent.AggregateEvent.Order;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, ReadChannelInboxMessageEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        ReadInboxMaxId = domainEvent.AggregateEvent.MaxId;
        if (TopMessage < ReadInboxMaxId)
        {
            TopMessage = ReadInboxMaxId;
            //TopMessageBoxId = domainEvent.AggregateEvent.MessageBoxId;
        }

        var unreadCount = TopMessage - domainEvent.AggregateEvent.MaxId;
        if (unreadCount < 0)
        {
            unreadCount = 0;
        }

        UnreadCount = unreadCount;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, ReadInboxMessage2Event> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        ReadInboxMaxId = domainEvent.AggregateEvent.MaxMessageId;
        if (TopMessage < ReadInboxMaxId)
        {
            TopMessage = ReadInboxMaxId;
        }

        UnreadCount = domainEvent.AggregateEvent.UnreadCount;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DialogAggregate, DialogId, SetOutboxTopMessageSuccessEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerId = domainEvent.AggregateEvent.OwnerPeerId;
        TopMessage = domainEvent.AggregateEvent.MessageId;
        //TopMessageBoxId = domainEvent.AggregateEvent.MessageBoxId.Value;
        ToPeerType = domainEvent.AggregateEvent.ToPeer.PeerType;
        ToPeerId = domainEvent.AggregateEvent.ToPeer.PeerId;

        //ReadInboxMaxId = domainEvent.AggregateEvent.MessageId;
        MaxSendOutMessageId = domainEvent.AggregateEvent.MessageId;

        //Pts=domainEvent.AggregateEvent.
        if (!Version.HasValue)
        {
            CreationTime = DateTime.UtcNow;
        }

        if (domainEvent.AggregateEvent.ClearDraft)
        {
            Draft = null;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        PinnedMsgId = domainEvent.AggregateEvent.MessageId;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Pinned = domainEvent.AggregateEvent.Pinned;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        NotifySettings = domainEvent.AggregateEvent.PeerNotifySettings;
        return Task.CompletedTask;
    }
    //#region Draft
    public void SetNewTopMessageId(int newTopMessageId)
    {
        TopMessage = newTopMessageId;
        //NewTopMessageId = newTopMessageId;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = DialogId.Create(domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.OutboxMessageItem.ToPeer).Value;
        TopMessage = domainEvent.AggregateEvent.OutboxMessageItem.MessageId;

        TopMessage = domainEvent.AggregateEvent.OutboxMessageItem.MessageId;
        OwnerId = domainEvent.AggregateEvent.RequestInfo.UserId;

        ToPeerType = domainEvent.AggregateEvent.OutboxMessageItem.ToPeer.PeerType;
        ToPeerId = domainEvent.AggregateEvent.OutboxMessageItem.ToPeer.PeerId;
        if (!Version.HasValue)
        {
            CreationTime = DateTime.UtcNow;
        }

        ReadInboxMaxId = domainEvent.AggregateEvent.OutboxMessageItem.MessageId;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = DialogId.Create(domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.InboxMessageItem.ToPeer).Value;

        TopMessage = domainEvent.AggregateEvent.InboxMessageItem.MessageId;
        OwnerId = domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId;

        ToPeerType = domainEvent.AggregateEvent.InboxMessageItem.ToPeer.PeerType;
        ToPeerId = domainEvent.AggregateEvent.InboxMessageItem.ToPeer.PeerId;
        if (!Version.HasValue)
        {
            CreationTime = DateTime.UtcNow;
        }

        UnreadCount++;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = DialogId.Create(domainEvent.AggregateEvent.MessageItem.SenderPeer.PeerId,
            domainEvent.AggregateEvent.MessageItem.ToPeer).Value;

        Pts = domainEvent.AggregateEvent.Pts;

        return Task.CompletedTask;
    }
}
