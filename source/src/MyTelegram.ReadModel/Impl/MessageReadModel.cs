namespace MyTelegram.ReadModel.Impl;

public class MessageReadModel : IMessageReadModel,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessageCreatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessageEditedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessageEditedEvent>,
    //IAmReadModelFor<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent>,
    //IAmReadModelFor<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, UpdatePinnedMessageStartedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, ReplyToMessageStartedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, MessageDeletedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, OutboxMessageDeletedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, InboxMessageDeletedEvent>,
    IAmReadModelFor<MessageAggregate, MessageId, SelfMessageDeletedEvent>,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
    IAmReadModelFor<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent>
{
    public int Date { get; private set; }
    public int? EditDate { get; private set; }
    public bool EditHide { get; private set; }
    public byte[]? Entities { get; private set; }
    public MessageFwdHeader? FwdHeader { get; private set; }
    public long? GroupedId { get; private set; }
    public string Id { get; private set; } = default!;
    public byte[]? Media { get; private set; }
    public string Message { get; private set; } = default!;
    public string? MessageActionData { get; private set; }
    public MessageActionType MessageActionType { get; private set; }
    public MessageType MessageType { get; private set; }
    public int MessageId { get; private set; }
    public bool Out { get; private set; }
    public long OwnerPeerId { get; private set; }
    public bool Pinned { get; private set; }
    public bool Post { get; private set; }
    public string? PostAuthor { get; private set; } = null;
    public int Pts { get; private set; }
    public int? ReplyToMsgId { get; private set; }
    public int? TopMsgId { get; private set; }
    public int SenderMessageId { get; private set; }
    public long SenderPeerId { get; private set; }
    public SendMessageType SendMessageType { get; private set; }
    public bool Silent { get; private set; }
    public long ToPeerId { get; private set; }
    public PeerType ToPeerType { get; private set; }
    public Peer? SavedPeerId { get; private set; }
    public int? Views { get; private set; }
    public long? LinkedChannelId { get; private set; }
    public int Replies { get; private set; }
    public int? SavedFromMsgId { get; private set; }
    public long? SavedFromPeerId { get; private set; }
    public virtual long? Version { get; set; }
    public long? PollId { get; private set; }
    public byte[]? ReplyMarkup { get; private set; }
    public IInputReplyTo? ReplyTo { get; private set; }
    public List<UserReaction>? UserReactions { get; set; }
    //public List<long>? ReactionUserIds { get; private set; }
    public List<ReactionCount>? Reactions { get; private set; }
    public List<Reaction>? RecentReactions { get; private set; }
    public bool CanSeeList { get; private set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var messageItem = domainEvent.AggregateEvent.OutboxMessageItem;
        Id = domainEvent.AggregateIdentity.Value;
        OwnerPeerId = messageItem.OwnerPeer.PeerId;
        SenderPeerId = messageItem.SenderPeer.PeerId;
        ToPeerType = messageItem.ToPeer.PeerType;
        ToPeerId = messageItem.ToPeer.PeerId;
        MessageType = messageItem.MessageType;
        MessageId = messageItem.MessageId;
        Message = messageItem.Message;
        Entities = messageItem.Entities;
        Date = messageItem.Date;
        SenderMessageId = messageItem.MessageId;
        MessageActionData = messageItem.MessageActionData;
        MessageActionType = messageItem.MessageActionType;
        //ReplyToMsgId = messageItem.ReplyToMsgId;
        TopMsgId = messageItem.TopMsgId;
        FwdHeader = messageItem.FwdHeader;
        SendMessageType = messageItem.SendMessageType;
        Media = messageItem.Media;
        GroupedId = messageItem.GroupId;
        Out = messageItem.IsOut;
        Views = messageItem.Views;
        Post = messageItem.Post;
        LinkedChannelId = domainEvent.AggregateEvent.LinkedChannelId;
        PostAuthor = messageItem.PostAuthor;

        if (domainEvent.AggregateEvent.OutboxMessageItem.FwdHeader != null)
        {
            var fwdHeader = domainEvent.AggregateEvent.OutboxMessageItem.FwdHeader;
            if (fwdHeader.SavedFromPeer != null)
            {
                SavedFromPeerId = fwdHeader.SavedFromPeer.PeerId;
                SavedFromMsgId = fwdHeader.SavedFromMsgId;
            }
        }

        Silent = false;
        PollId = messageItem.PollId;
        ReplyMarkup = messageItem.ReplyMarkup;
        ReplyTo = messageItem.InputReplyTo;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var messageItem = domainEvent.AggregateEvent.InboxMessageItem;
        Id = domainEvent.AggregateIdentity.Value;
        OwnerPeerId = messageItem.OwnerPeer.PeerId;
        SenderPeerId = messageItem.SenderPeer.PeerId;
        ToPeerType = messageItem.ToPeer.PeerType;
        ToPeerId = messageItem.ToPeer.PeerId;
        MessageType = messageItem.MessageType;
        MessageId = messageItem.MessageId;
        Message = messageItem.Message;
        Entities = messageItem.Entities;
        Date = messageItem.Date;
        SenderMessageId = domainEvent.AggregateEvent.SenderMessageId;
        MessageActionData = messageItem.MessageActionData;
        MessageActionType = messageItem.MessageActionType;
        //ReplyToMsgId = messageItem.ReplyToMsgId;
        FwdHeader = messageItem.FwdHeader;
        SendMessageType = messageItem.SendMessageType;
        Media = messageItem.Media;
        GroupedId = messageItem.GroupId;
        Out = messageItem.IsOut;
        Views = messageItem.Views;

        Silent = false;
        PollId = messageItem.PollId;
        ReplyMarkup = messageItem.ReplyMarkup;
        ReplyTo = messageItem.InputReplyTo;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, OutboxMessageEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Message = domainEvent.AggregateEvent.NewMessage;
        Entities = domainEvent.AggregateEvent.Entities;
        EditDate = domainEvent.AggregateEvent.EditDate;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessageEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Message = domainEvent.AggregateEvent.NewMessage;
        Entities = domainEvent.AggregateEvent.Entities;
        EditDate = domainEvent.AggregateEvent.EditDate;
        return Task.CompletedTask;
    }

    //public Task ApplyAsync(IReadModelContext context,
    //    IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    if (string.IsNullOrEmpty(Id))
    //    {
    //        Id = MyTelegram.Domain.Aggregates.Messaging.MessageId.Create(
    //            domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //            domainEvent.AggregateEvent.MessageItem.MessageId).Value;
    //    }
    //    Pts = domainEvent.AggregateEvent.Pts;
    //    return Task.CompletedTask;
    //}

    //public Task ApplyAsync(IReadModelContext context,
    //    IDomainEvent<SendMessageSaga, SendMessageSagaId, MyTelegram.Domain.Sagas.Events.ReceiveInboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    if (string.IsNullOrEmpty(Id))
    //    {
    //        Id = MyTelegram.Domain.Aggregates.Messaging.MessageId.Create(
    //            domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //            domainEvent.AggregateEvent.MessageItem.MessageId).Value;
    //    }
    //    Pts = domainEvent.AggregateEvent.Pts;
    //    return Task.CompletedTask;
    //}

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = MyTelegram.Domain.Aggregates.Messaging.MessageId.Create(
                domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.MessageItem.MessageId).Value;
        }
        Pts = domainEvent.AggregateEvent.Pts;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<SendMessageSaga, SendMessageSagaId, MyTelegram.Domain.Sagas.Events.ReceiveInboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = MyTelegram.Domain.Aggregates.Messaging.MessageId.Create(
                domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.MessageItem.MessageId).Value;
        }
        Pts = domainEvent.AggregateEvent.Pts;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Pinned = domainEvent.AggregateEvent.Pinned;
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
        IDomainEvent<MessageAggregate, MessageId, UpdatePinnedMessageStartedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Pinned = domainEvent.AggregateEvent.Pinned;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, ReplyToMessageStartedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Replies++;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, MessageDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, OutboxMessageDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, InboxMessageDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<MessageAggregate, MessageId, SelfMessageDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }
}