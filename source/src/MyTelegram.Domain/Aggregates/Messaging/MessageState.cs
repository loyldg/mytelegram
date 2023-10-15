using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageState : AggregateState<MessageAggregate, MessageId, MessageState>,
    IApply<SendMessageStartedEvent>,
    IApply<OutboxMessageCreatedEvent>,
    IApply<InboxMessageCreatedEvent>,
    IApply<InboxMessageIdAddedToOutboxMessageEvent>,
    IApply<MessageDeletedEvent>,
    IApply<OutboxMessageEditedEvent>,
    IApply<InboxMessageEditedEvent>,
    IApply<MessageForwardedEvent>,
    IApply<InboxMessageHasReadEvent>,
    IApply<ReplyToMessageEvent>,
    IApply<ReplyToMessageStartedEvent>,
    IApply<MessageViewsIncrementedEvent>,
    IApply<DeleteMessagesStartedEvent>,
    IApply<UpdatePinnedMessageStartedEvent>,
    IApply<InboxMessagePinnedUpdatedEvent>,
    IApply<OutboxMessagePinnedUpdatedEvent>,
    IApply<OtherPartyMessageDeletedEvent>,
    IApply<ForwardMessageStartedEvent>,
    IApply<SelfMessageDeletedEvent>,
    IApply<OutboxMessageDeletedEvent>,
    IApply<InboxMessageDeletedEvent>,
    IApply<InboxItemsAddedToOutboxMessageEvent>

{
    public MessageItem MessageItem { get; private set; } = null!;
    public List<InboxItem> InboxItems { get; private set; } = new();
    public int SenderMessageId { get; private set; }

    public bool Pinned { get; private set; }
    public bool PmOneSide { get; private set; }
    public int EditDate { get; private set; }
    //public bool EditHide { get; private set; }
    public bool Edited { get; private set; }
    public int Pts { get; private set; }

    public IReadOnlyCollection<Peer> RecentRepliers { get; private set; } = new List<Peer>(MyTelegramServerDomainConsts.MaxRecentRepliersCount);



    public void LoadSnapshot(MessageSnapshot snapshot)
    {
        MessageItem = snapshot.MessageItem;
        InboxItems = snapshot.InboxItems;
        SenderMessageId = snapshot.SenderMessageId;
        Pinned = snapshot.Pinned;
        EditDate = snapshot.EditDate;
        //EditHide = snapshot.EditHide;
        Edited = snapshot.Edited;
        Pts = snapshot.Pts;
        RecentRepliers = snapshot.RecentRepliers;
    }

    //private readonly CircularBuffer<Peer> _recentRepliers = new(5);
    //public void LoadSnapshot(MessageSnapshot snapshot)
    //{
    //    MessageItem = snapshot.MessageItem;
    //    InboxItems = snapshot.InboxItems;
    //    SenderMessageId = snapshot.SenderMessageId;
    //    Pinned = snapshot.Pinned;
    //    Pts = snapshot.Pts;
    //    PmOneSide = snapshot.PmOneSide;
    //}

    //public MessageItem InMessageItem { get; private set; }
    public void Apply(OutboxMessageCreatedEvent aggregateEvent)
    {
        MessageItem = aggregateEvent.OutboxMessageItem;
        SenderMessageId = aggregateEvent.OutboxMessageItem.MessageId;
    }

    public void Apply(InboxMessageCreatedEvent aggregateEvent)
    {
        MessageItem = aggregateEvent.InboxMessageItem;
        SenderMessageId = aggregateEvent.SenderMessageId;
    }

    public void Apply(InboxMessageIdAddedToOutboxMessageEvent aggregateEvent)
    {
        InboxItems.Add(aggregateEvent.InboxItem);
    }

    public void Apply(MessageDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(OutboxMessageEditedEvent aggregateEvent)
    {
        EditDate = aggregateEvent.EditDate;
        Edited = true;
    }

    public void Apply(InboxMessageEditedEvent aggregateEvent)
    {
        EditDate = aggregateEvent.EditDate;
        Edited = true;
    }

    public void Apply(MessageForwardedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(InboxMessageHasReadEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ReplyToMessageEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ReplyToMessageStartedEvent aggregateEvent)
    {
        RecentRepliers = aggregateEvent.RecentRepliers;
        //throw new NotImplementedException();
    }

    public void Apply(SendMessageStartedEvent aggregateEvent)
    {
        MessageItem = aggregateEvent.OutMessageItem;
        SenderMessageId = aggregateEvent.OutMessageItem.MessageId;
        //throw new NotImplementedException();
    }

    public void Apply(MessageViewsIncrementedEvent aggregateEvent)
    {
        MessageItem.Views++;
    }

    public void Apply(DeleteMessagesStartedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(UpdatePinnedMessageStartedEvent aggregateEvent)
    {
        Pinned = aggregateEvent.Pinned;
        PmOneSide = aggregateEvent.PmOneSide;
    }

    public void Apply(InboxMessagePinnedUpdatedEvent aggregateEvent)
    {
        Pinned = aggregateEvent.Pinned;
        //PmOneSide = false;
    }

    public void Apply(OutboxMessagePinnedUpdatedEvent aggregateEvent)
    {
        Pinned = aggregateEvent.Pinned;
        //PmOneSide = false;
    }

    public void Apply(OtherPartyMessageDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(ForwardMessageStartedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
    //public CircularBuffer<Reaction> RecentReactions { get; private set; } = new(10);
    //public ConcurrentDictionary<long, Reaction> RecentReactions { get; private set; } = new();
    public List<Reaction> RecentReactions { get; private set; } = new();
    public ConcurrentDictionary<long, ReactionCount> ReactionCounts { get; private set; } = new();
    //public List<Reaction> AllReactions { get; private set; } = new();
    public ConcurrentDictionary<long, List<Reaction>> UserReactions { get; private set; } = new();

    public List<ReactionCount>? GetReactions()
    {
        if (ReactionCounts.Count > 0)
        {
            return ReactionCounts.Values.ToList();
        }

        return null;
    }

    private void UpdateReactions(long reactionSenderUserId,
        List<ReactionCount>? reactions,
        List<Reaction>? addedReactions,
        List<Reaction>? recentReactions, int date)
    {
        EditDate = date;
        RecentReactions = recentReactions ?? Array.Empty<Reaction>().ToList();

        //RecentReactions = new(10, recentReactions?.ToArray() ?? Array.Empty<Reaction>());
        //RecentReactions = recentReactions?.ToDictionary(k => k.GetReactionId(), v => v) ?? new();
        if (reactions?.Count > 0)
        {
            ReactionCounts = new(reactions.ToDictionary(k => k.GetReactionId(), v => v));
        }
        else
        {
            ReactionCounts = new();
        }

        UserReactions.TryRemove(reactionSenderUserId, out _);
        if (addedReactions?.Count > 0)
        {
            UserReactions.TryAdd(reactionSenderUserId, addedReactions);
        }
    }

    //private void UpdateReactions(long reactionSenderUserId, List<Reaction>? reactions)
    //{
    //    if (reactions == null || reactions.Count == 0)
    //    {
    //        if (UserReactions.TryRemove(reactionSenderUserId, out var userReactions))
    //        {
    //            // decrement reaction count
    //            foreach (var userReaction in userReactions)
    //            {
    //                var reactionId = userReaction.GetReactionId();
    //                if (ReactionCounts.TryGetValue(reactionId, out var reaction))
    //                {
    //                    reaction.DecrementCount();
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (UserReactions.TryGetValue(reactionSenderUserId, out var userReactions))
    //        {
    //            foreach (var reaction in reactions)
    //            {
    //                if (!userReactions.Contains(reaction))
    //                {
    //                    userReactions.Add(reaction);
    //                    var reactionId = reaction.GetReactionId();
    //                    if (ReactionCounts.TryGetValue(reactionId, out var reactionCount))
    //                    {
    //                        reactionCount.IncrementCount();
    //                    }
    //                    else
    //                    {
    //                        ReactionCounts.TryAdd(reactionId, reaction);
    //                    }
    //                    RecentReactions.Put(reaction);
    //                }
    //            }
    //        }
    //    }
    //}

    public void Apply(SelfMessageDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(OutboxMessageDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(InboxMessageDeletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(InboxItemsAddedToOutboxMessageEvent aggregateEvent)
    {
        InboxItems = aggregateEvent.InboxItems;
    }
}