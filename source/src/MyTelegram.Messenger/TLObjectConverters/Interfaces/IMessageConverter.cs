namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IMessageConverter : ILayeredConverter, IHasRequestLayer
{
    IMessage ToMessage(MessageItem item,
        long selfUserId = 0, long? linkedChannelId = null, int pts = 0,
        List<ReactionCount>? reactions = null,
        List<Reaction>? recentReactions = null,
        int? editDate = null,
        bool editHide = false,
        List<UserReaction>? userReactions = null,
        bool mentioned = false
        );

    IMessage ToMessage(InboxMessageEditCompletedEvent aggregateEvent);

    IMessage ToMessage(OutboxMessageEditCompletedEvent aggregateEvent,
        long selfUserId);
    IMessageFwdHeader? ToMessageFwdHeader(MessageFwdHeader? messageFwdHeader);
    IMessageReplyHeader? ToMessageReplyHeader(int? replyToMessageId, int? topMsgId);

    IList<IMessage> ToMessages(IReadOnlyCollection<IMessageReadModel> readModels,
        IReadOnlyCollection<IPollReadModel>? pollReadModels,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? pollAnswerVoterReadModels,
        long selfUserId);

    IMessage ToDiscussionMessage(IMessageReadModel messageReadModel, int maxId, int readMaxId, int readInboxMaxId, int readOutboxMaxId, long selfUserId);
}
