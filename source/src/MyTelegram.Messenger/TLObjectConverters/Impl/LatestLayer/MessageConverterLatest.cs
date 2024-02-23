namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class MessageConverterLatest : LayeredConverterBase, IMessageConverterLatest
{
    private readonly ILayeredService<IPollConverter> _layeredPollService;
    private readonly IPeerHelper _peerHelper;
    private IPollConverter? _pollConverter;

    public MessageConverterLatest(IPeerHelper peerHelper,
        ILayeredService<IPollConverter> layeredPollService)
    {
        _peerHelper = peerHelper;
        _layeredPollService = layeredPollService;
    }

    public override int Layer => Layers.LayerLatest;

    public IMessage ToDiscussionMessage(IMessageReadModel messageReadModel,
        int maxId,
        int readMaxId,
        int readInboxMaxId,
        int readOutboxMaxId,
        long selfUserId
    )
    {
        var m = ToMessage(messageReadModel, null, null, selfUserId);
        if (m is TMessage tMessage)
        {
            var replies = ToMessageReplies(messageReadModel.Post,
                messageReadModel.LinkedChannelId,
                messageReadModel.Pts);
            if (replies != null)
            {
                replies.MaxId = maxId;
                replies.ReadMaxId = readMaxId;
                tMessage.Replies = replies;
            }
        }

        return m;
    }

    public virtual IMessage ToMessage(MessageItem item,
            long selfUserId = 0,
        long? linkedChannelId = null,
        int pts = 0,
        List<ReactionCount>? reactions = null,
        List<Reaction>? recentReactions = null,
        int? editDate = null,
        bool editHide = false,
        List<UserReaction>? userReactions = null,
        bool mentioned = false
    )
    {
        var isOut = item.IsOut;
        var canSeeList = item.IsOut &&
                         (item.ToPeer.PeerType == PeerType.Channel || item.ToPeer.PeerType == PeerType.Chat);

        if (item.ToPeer.PeerType == PeerType.Channel && selfUserId != 0)
        {
            isOut = item.SenderPeer.PeerId == selfUserId;
        }

        switch (item.SendMessageType)
        {
            case SendMessageType.MessageService:
                {
                    if (string.IsNullOrEmpty(item.MessageActionData))
                    {
                        throw new ArgumentNullException(nameof(item),
                            "MessageActionData can not be null for service message");
                    }

                    var bytes = item.MessageActionData.ToBytes();
                    var fromId = item.SenderPeer.ToPeer();
                    //if (box.ToPeer.PeerType == PeerType.Channel && box.Post)
                    //{
                    //    fromId = null;
                    //}

                    if (item.ToPeer.PeerType == PeerType.Channel && item.Post)
                    {
                        fromId = null;
                    }

                    var m = new TMessageService
                    {
                        Date = item.Date,
                        //Silent = outbox.Silent,
                        Post = false, // outbox.Post,
                        PeerId = item.ToPeer.ToPeer(),
                        FromId = fromId,
                        Id = item.MessageId,
                        //Out = item.IsOut,
                        Out = isOut,
                        Action = bytes.ToTObject<IMessageAction>(),
                        ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                        Mentioned = mentioned,
                        MediaUnread = mentioned
                    };

                    return m;
                }
            //break;

            default:
                {
                    var m = new TMessage
                    {
                        Date = item.Date,
                        EditDate = editDate,
                        EditHide = editHide,
                        Entities = item.Entities.ToTObject<TVector<IMessageEntity>>(),
                        FromId = item.SenderPeer.ToPeer(),
                        PeerId = item.ToPeer.ToPeer(),
                        Id = item.MessageId,
                        Message = item.Message,
                        Out = isOut,
                        FwdFrom = ToMessageFwdHeader(item.FwdHeader),
                        GroupedId = item.GroupId,
                        Media = item.Media.ToTObject<IMessageMedia>(),
                        Views = item.Views,
                        Forwards = item.Views.HasValue ? 0 : null,
                        Post = item.Post,
                        Mentioned = mentioned,
                        MediaUnread = mentioned,
                        PostAuthor = item.PostAuthor,
                        SavedPeerId = item.SavedPeerId.ToPeer()
                    };
                    if (item.ToPeer.PeerType == PeerType.Channel)
                    {
                        //m.FromId = null;
                        if (item.Post /*|| item.SenderPeer.PeerId == selfUserId*/)
                        {
                            m.FromId = null;
                        }

                        m.Replies = ToMessageReplies(item.Post, linkedChannelId, pts);
                        if (m.Replies != null && item.FwdHeader?.SavedFromPeer != null) // forward from linked channel
                        {
                            //m.FromId = _peerHelper.ToPeer(PeerType.Channel, item.FwdHeader.SavedFromPeer.PeerId);
                            m.FromId = item.FwdHeader.SavedFromPeer.ToPeer();
                            m.Out = false;
                        }
                    }
                    if (editDate == 0)
                    {
                        m.EditDate = null;
                    }

                    m.ReplyTo = item.InputReplyTo.ToMessageReplyHeader();
                    m.ReplyMarkup = item.ReplyMarkup.ToTObject<IReplyMarkup>();

                    //m.Reactions = GetReactionConverter().ToMessageReactions(selfUserId,
                    //    item.ToPeer,
                    //    reactions,
                    //    recentReactions,
                    //    canSeeList,
                    //    userReactions);

                    return m;
                }
                //break;
        }
    }

    public IMessage ToMessage(InboxMessageEditCompletedEvent aggregateEvent)
    {
        return new TMessage
        {
            Date = aggregateEvent.EditDate,
            EditDate = aggregateEvent.EditDate,
            Entities = aggregateEvent.Entities.ToTObject<TVector<IMessageEntity>>(),
            FromId = new TPeerUser { UserId = aggregateEvent.SenderPeerId },
            PeerId = aggregateEvent.ToPeer.ToPeer(),
            Id = aggregateEvent.MessageId,
            Out = false,
            Message = aggregateEvent.Message,
            Media = aggregateEvent.Media.ToTObject<IMessageMedia>()
        };
    }

    public IMessage ToMessage(OutboxMessageEditCompletedEvent aggregateEvent,
        long selfUserId)
    {
        var m = new TMessage
        {
            Date = aggregateEvent.Date,
            Entities = aggregateEvent.Entities.ToTObject<TVector<IMessageEntity>>(),
            FromId = new Peer(PeerType.User, aggregateEvent.SenderPeerId).ToPeer(),
            PeerId = aggregateEvent.ToPeer.ToPeer(),
            Id = aggregateEvent.MessageId,
            EditDate = aggregateEvent.Date,
            Out = aggregateEvent.SenderPeerId == selfUserId,
            Message = aggregateEvent.Message,
            Views = aggregateEvent.Views,
            Forwards = aggregateEvent.Views.HasValue ? 0 : null,
            Post = aggregateEvent.Post,
            Media = aggregateEvent.Media.ToTObject<IMessageMedia>()
        };
        if (aggregateEvent.Post)
        {
            m.FromId = null;
        }

        return m;
    }

    public IMessageFwdHeader? ToMessageFwdHeader(MessageFwdHeader? fwdHeader)
    {
        if (fwdHeader == null)
        {
            return null;
        }

        return new TMessageFwdHeader
        {
            ChannelPost = fwdHeader.ChannelPost,
            Date = fwdHeader.Date,
            FromId = fwdHeader.FromId.ToPeer(),
            FromName = fwdHeader.FromName,
            PostAuthor = fwdHeader.PostAuthor,
            SavedFromPeer = fwdHeader.SavedFromPeer.ToPeer(),
            SavedFromMsgId = fwdHeader.SavedFromMsgId
        };
    }

    public IMessageReplyHeader? ToMessageReplyHeader(int? replyToMsgId,
                    int? topMsgId)
    {
        if (replyToMsgId > 0)
        {
            return new TMessageReplyHeader
            { ReplyToMsgId = replyToMsgId.Value, ReplyToTopId = topMsgId /*, ForumTopic = topMsgId.HasValue*/ };
        }

        return null;
    }

    public IMessageReplyHeader? ToMessageReplyHeader(IInputReplyTo? inputReplyTo)
    {
        return inputReplyTo.ToMessageReplyHeader();
    }

    public IList<IMessage> ToMessages(IReadOnlyCollection<IMessageReadModel> readModels,
        IReadOnlyCollection<IPollReadModel>? pollReadModels,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? pollAnswerVoterReadModels,
        long selfUserId)
    {
        var messages = new List<IMessage>();
        foreach (var readModel in readModels)
        {
            IPollReadModel? poll = null;
            List<string>? chosenOptions = null;
            if (readModel.PollId.HasValue)
            {
                poll = pollReadModels?.FirstOrDefault(p => p.PollId == readModel.PollId);
                chosenOptions = pollAnswerVoterReadModels?.Where(p => p.PollId == readModel.PollId)
                    .Select(p => p.Option).ToList();
            }

            messages.Add(ToMessage(readModel, poll, chosenOptions, selfUserId));
        }

        return messages;
    }
    protected IPollConverter GetPollConverter()
    {
        return _pollConverter ??= _layeredPollService.GetConverter(GetLayer());
    }

    //public IMessage ToDiscussionMessage(IMessageReadModel readModel,long selfUserId,long from)

    private IMessage ToMessage(IMessageReadModel readModel,
        IPollReadModel? pollReadModel,
        List<string>? chosenOptions,
        long selfUserId)
    {
        switch (readModel.SendMessageType)
        {
            case SendMessageType.MessageService:
                {
                    ArgumentNullException.ThrowIfNull(readModel.MessageActionData);

                    var bytes = readModel.MessageActionData.ToBytes();
                    var fromId = _peerHelper.ToPeer(PeerType.User, readModel.SenderPeerId);
                    if (readModel.ToPeerType == PeerType.Channel && readModel.Post &&
                        readModel.MessageActionType != MessageActionType.ChatAddUser)
                    {
                        fromId = null;
                    }

                    var m = new TMessageService
                    {
                        Date = readModel.Date,
                        Silent = readModel.Silent,
                        Post = readModel.Post,
                        PeerId = _peerHelper.ToPeer(readModel.ToPeerType, readModel.ToPeerId),
                        FromId = fromId,
                        Id = readModel.MessageId,
                        Out = readModel.SenderPeerId == selfUserId,
                        Action = bytes.ToTObject<IMessageAction>(),
                        ReplyTo = ToMessageReplyHeader(readModel.ReplyTo)
                    };

                    return m;
                }
            default:
                {
                    var fromId = _peerHelper.ToPeer(PeerType.User, readModel.SenderPeerId);
                    var toPeerId = _peerHelper.ToPeer(readModel.ToPeerType, readModel.ToPeerId);

                    var m = new TMessage
                    {
                        Date = readModel.Date,
                        EditDate = readModel.EditDate,
                        EditHide = readModel.EditHide,
                        Message = readModel.Message,
                        Silent = readModel.Silent,
                        Post = readModel.Post,
                        PostAuthor = readModel.PostAuthor,
                        GroupedId = readModel.GroupedId,
                        Views = readModel.Views,
                        Forwards = readModel.Views.HasValue ? 0 : null,
                        Entities = readModel.Entities.ToTObject<TVector<IMessageEntity>>(),
                        PeerId = toPeerId,
                        FromId = fromId,
                        Id = readModel.MessageId,
                        Out = readModel.SenderPeerId == selfUserId,
                        Pinned = readModel.Pinned,
                        FwdFrom = ToMessageFwdHeader(readModel.FwdHeader),
                        Media = readModel.Media.ToTObject<IMessageMedia>(),
                        ReplyTo = ToMessageReplyHeader(readModel.ReplyTo),
                        ReplyMarkup = readModel.ReplyMarkup.ToTObject<IReplyMarkup>(),
                        SavedPeerId = readModel.SavedPeerId.ToPeer()
                    };
                    if (pollReadModel != null)
                    {
                        m.Media = new TMessageMediaPoll
                        {
                            Poll = GetPollConverter().ToPoll(pollReadModel),
                            Results = GetPollConverter().ToPollResults(pollReadModel, chosenOptions ?? new List<string>())
                        };
                    }

                    if (readModel.EditDate == 0)
                    {
                        m.EditDate = null;
                    }

                    if (readModel.ToPeerType == PeerType.Channel)
                    {
                        if (readModel.Post)
                        {
                            m.FromId = null;
                        }

                        m.Replies = ToMessageReplies(readModel.Post, readModel.LinkedChannelId, readModel.Pts);
                        if (m.Replies != null && readModel.FwdHeader != null) // forward from linked channel
                        {
                            //m.FromId = _peerHelper.ToPeer(PeerType.Channel, readModel.LinkedChannelId!.Value);
                            m.FromId = readModel.FwdHeader.FromId.ToPeer();
                            m.Out = false;
                            m.Replies.Replies = readModel.Replies;
                        }
                    }

                    return m;
                }
        }
    }

    private IMessageReplies? ToMessageReplies(bool post,
        long? linkedChannelId,
        int pts)
    {
        if (post)
        {
            if (linkedChannelId.HasValue)
            {
                return new TMessageReplies { ChannelId = linkedChannelId, Comments = true };
            }
        }
        else
        {
            if (linkedChannelId.HasValue)
            {
                return new TMessageReplies { RepliesPts = pts };
            }
        }

        return null;
    }
}