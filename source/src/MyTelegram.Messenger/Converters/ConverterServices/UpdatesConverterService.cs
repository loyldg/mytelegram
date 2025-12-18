namespace MyTelegram.Messenger.Converters.ConverterServices;

public class UpdatesConverterService(
    IMessageConverterService messageConverterService,
    IChatConverterService chatConverterService,
    IMessageResponseService messageResponseService,
    ILayeredService<IDraftMessageConverter> draftMessageLayeredService) : IUpdatesConverterService, ITransientDependency
{

    public IUpdates ToChannelMessageUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer, bool mentioned = false)
    {
        if (aggregateEvent.IsSendGroupedMessages)
        {
            return CreateSendGroupedChannelMessageUpdates(selfUserId, aggregateEvent, layer);
        }

        var item = aggregateEvent.MessageItems.First();
        // selfUser==-1 means the updates is for channel member except sender
        //const int selfUserId = -1;
        var message = messageConverterService
            .ToMessage(
                selfUserId,
                //selfUserId,
                aggregateEvent.MessageItem,
                mentioned: mentioned,
                layer: layer
            );
        var updateNewChannelMessage =
            new TUpdateNewChannelMessage
            {
                Message = message,
                Pts = item.Pts,
                PtsCount = 1
            };

        return new TUpdates
        {
            Chats = [],
            Date = item.Date,
            Seq = 0,
            Users = [],
            Updates = new TVector<IUpdate>(updateNewChannelMessage)
        };
    }

    private IUpdates CreateSendGroupedChannelMessageUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer)
    {
        List<IUpdate> updateList = aggregateEvent.MessageItems.Select(p => (IUpdate)new TUpdateMessageID
        {
            Id = p.MessageId,
            RandomId = p.RandomId,
        }).ToList();

        // selfUser==-1 means the updates is for channel member except sender
        //const int selfUserId = -1;
        foreach (var item in aggregateEvent.MessageItems)
        {
            var updateReadChannelInbox = new TUpdateReadChannelInbox
            {
                ChannelId = item.ToPeer.PeerId,
                MaxId = item.MessageId,
                Pts = item.Pts,
            };
            var updateNewChannelMessage = new TUpdateNewChannelMessage
            {
                Message = messageConverterService.ToMessage(selfUserId, item, layer: layer),
                Pts = item.Pts,
                PtsCount = 1
            };

            updateList.Add(updateReadChannelInbox);
            updateList.Add(updateNewChannelMessage);
        }

        return new TUpdates
        {
            Updates = [.. updateList],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Users = []
        };
    }
    public IUpdates ToChannelUpdates(IRequestWithAccessHashKeyId request, IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel, int layer)
    {
        var channel = chatConverterService.ToChannel(request, channelReadModel, photoReadModel, null, false, layer);
        if (channel is ILayeredChannel layeredChannel)
        {
            layeredChannel.Left = false;
        }

        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Updates = new TVector<IUpdate>(new TUpdateChannel { ChannelId = channelReadModel.ChannelId }),
            Users = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public async Task<IUpdates> ToCreateChannelUpdatesAsync(ChannelCreatedEvent eventData,
        SendOutboxMessageCompletedSagaEvent aggregateEvent, bool createUpdatesForSelf, int layer)
    {
        var channelId = eventData.ChannelId;
        var item = aggregateEvent.MessageItem;
        var updateList = ToChannelMessageServiceUpdates(item.MessageId,
            eventData.RandomId,
            item.Pts,
            null,
            item.OwnerPeer with { PeerType = PeerType.Channel },
            new TMessageActionChannelCreate { Title = eventData.Title },
            item.Date,
            0,
            createUpdatesForSelf,
            layer
            );
        var updateChannel = new TUpdateChannel { ChannelId = eventData.ChannelId };
        updateList.Insert(1, updateChannel);
        //var channel = GetChatConverter().ToChannel(eventData);
        var channel = await chatConverterService.GetChannelAsync(eventData.RequestInfo, channelId, false, false,
            layer);

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Date = item.Date,
            Updates = [.. updateList],
            Users = []
        };
        return updates;
    }

    public IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date)
    {
        if (toPeerType == PeerType.Channel)
        {
            return new TUpdateShort
            {
                Date = date,
                Update = new TUpdateDeleteChannelMessages
                {
                    ChannelId = item.OwnerPeerId,
                    Messages = [.. item.DeletedMessageIdList],
                    Pts = item.Pts,
                    PtsCount = item.PtsCount
                }
            };
        }

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(new TUpdateDeleteMessages
            {
                Messages = [.. item.DeletedMessageIdList],
                Pts = item.Pts,
                PtsCount = item.PtsCount
            }),
            Chats = [],
            Users = [],
            Date = date,
            Seq = 0
        };
    }

    public virtual IUpdates ToDraftsUpdates(IReadOnlyCollection<IDraftReadModel> draftReadModels, int layer)
    {
        var converter = draftMessageLayeredService.GetConverter(layer);
        var draftUpdates = draftReadModels.Select(p => new TUpdateDraftMessage
        {
            Draft = converter.ToDraftMessage(p),
            Peer = p.Peer.ToPeer(),
            TopMsgId = p.Draft.TopMsgId
        });

        return new TUpdates
        {
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Users = [],
            Updates = [.. draftUpdates]
        };
    }

    public IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        return ToInboxForwardMessageUpdates(item, item.Pts);
    }

    //public IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent,
    //    StartInviteToChannelEvent startInviteToChannelEvent,
    //    IChannelReadModel channelReadModel,
    //    //IChat channel,
    //    bool createUpdatesForSelf,
    //    int layer
    //    )
    //{
    //    var item = aggregateEvent.MessageItem;
    //    var channel = chatConverterService.ToChannel(createUpdatesForSelf ? aggregateEvent.RequestInfo : RequestInfo.Empty,
    //        channelReadModel,
    //        null,
    //        null,
    //        false,
    //        aggregateEvent.RequestInfo.Layer
    //    );

    //    if (!createUpdatesForSelf)
    //    {
    //        return new TUpdates
    //        {
    //            Updates = new TVector<IUpdate>([new TUpdateChannel
    //            {
    //                ChannelId = channel.Id
    //            }
    //            ]),
    //            Chats = new TVector<IChat>(channel),
    //            Users = [],
    //            Date = DateTime.UtcNow.ToTimestamp()
    //        };
    //    }

    //    var updateList = ToChannelMessageServiceUpdates(item.MessageId,
    //        item.RandomId,
    //        item.Pts,
    //        item.SenderPeer,
    //        item.ToPeer,
    //        new TMessageActionChatAddUser { Users = [.. startInviteToChannelEvent.MemberUidList] },
    //        item.Date,
    //        0,
    //        createUpdatesForSelf,
    //        layer
    //    );

    //    return new TUpdates
    //    {
    //        Chats = new TVector<IChat>(channel),
    //        Date = item.Date,
    //        Updates = [.. updateList],
    //        Users = []
    //    };
    //}

    //public IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent)
    //{
    //    return ToUpdatePinnedMessageUpdates(aggregateEvent, true);
    //}

    public IUpdates ToUpdatePinnedMessageServiceUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer)
    {
        var item = aggregateEvent.MessageItem;

        var update = ToMessageServiceUpdate(selfUserId, item, layer);
        return new TUpdates
        {
            Date = item.Date,
            Users = [],
            Chats = [],
            Seq = 0,
            Updates = new TVector<IUpdate>(update)
        };
    }

    //public IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent)
    //{
    //    return ToUpdatePinnedMessageUpdates(aggregateEvent, false);
    //}

    public IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent, int layer)
    {
        var item = aggregateEvent.MessageItem;
        var updateMessageId = new TUpdateMessageID
        {
            Id = item.MessageId,
            RandomId = item.RandomId
        };

        var fromPeer = item.SendAs ?? item.SenderPeer;

        var update = ToMessageServiceUpdate(aggregateEvent.RequestInfo.UserId, item with { SenderPeer = item.SendAs ?? item.SenderPeer }, layer);

        return new TUpdates
        {
            Date = item.Date,
            Users = [],
            Chats = [],
            Seq = 0,
            Updates = new TVector<IUpdate>(updateMessageId, update)
        };
    }

    public IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;

        var update = ToMessageServiceUpdate(0, item, 0);
        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Chats = [],
            Date = item.Date,
            Seq = 0,
            Users = []
        };
    }

    private static TChatParticipants ToChatParticipants(long chatId,
        IReadOnlyList<ChatMember> chatMemberList,
        int date,
        long creatorUid,
        int chatVersion)
    {
        var participants = chatMemberList.Select(p =>
        {
            if (p.UserId == creatorUid)
            {
                return (IChatParticipant)new TChatParticipantCreator { UserId = p.UserId };
            }

            return new TChatParticipant { Date = date, InviterId = creatorUid, UserId = p.UserId };
        }).ToList();

        return new TChatParticipants
        {
            ChatId = chatId,
            Participants = [.. participants],
            Version = chatVersion
        };
    }

    private List<IUpdate> ToChannelMessageServiceUpdates(int messageId,
        long randomId,
        int pts,
        Peer? fromPeer,
        Peer toPeer,
        IMessageAction messageAction,
        int date,
        int? replyToMsgId,
        bool createUpdatesForSelf, int layer)
    {
        var updateMessageId = new TUpdateMessageID { Id = messageId, RandomId = randomId };
        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = toPeer.PeerId,
            MaxId = messageId,
            // FolderId = 0,
            Pts = pts,
            StillUnreadCount = 0
        };
        var isOut = createUpdatesForSelf;
        var message = new TMessageService
        {
            Action = messageAction,
            Date = date,
            FromId = fromPeer.ToPeer(),
            Out = isOut,
            PeerId = toPeer.ToPeer(),
            Id = messageId,
            ReplyTo = ToMessageReplyHeader(replyToMsgId, null)
        };
        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = pts,
            PtsCount = 1,
            Message = messageResponseService.ToLayeredData(message, layer)
        };

        return [updateMessageId, updateReadChannelInbox, updateNewChannelMessage];
    }

    private IUpdates ToEditUpdates(
        MessageItem item,
        int pts,
        long selfUserId = 0,
        List<long>? userReactions = null,
        long? linkedChannelId = null,
        int layer = 0
    )
    {
        var message = messageConverterService
            .ToMessage(selfUserId, item, userReactions, layer: layer);

        IUpdate update = item.ToPeer.PeerType switch
        {
            PeerType.Channel => new TUpdateEditChannelMessage
            {
                Pts = pts,
                PtsCount = 1,
                Message = message
            },
            _ => new TUpdateEditMessage
            {
                Pts = pts,
                PtsCount = 1,
                Message = message
            }
        };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update, new TUpdateRecentReactions()),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    private IUpdates ToInboxForwardMessageUpdates(MessageItem item,
        int pts)
    {
        var updateNewMessage =
            new TUpdateNewMessage
            { Message = messageConverterService.ToMessage(0, item), Pts = pts, PtsCount = 1 };
        return new TUpdates
        {
            Chats = [],
            Date = item.Date,
            Users = [],
            Seq = 0,
            Updates = new TVector<IUpdate>(updateNewMessage)
        };
    }

    private IUpdate ToMessageServiceUpdate(long selfUserId, MessageItem item, int layer)
    {
        var m = messageConverterService.ToMessage(selfUserId, item, layer: layer);

        if (item.ToPeer.PeerType == PeerType.Channel)
        {
            if (item.MessageAction is TMessageActionHistoryClear)
            {
                return new TUpdateEditChannelMessage { Message = m, Pts = item.Pts, PtsCount = 1 };
            }

            return new TUpdateNewChannelMessage { Message = m, Pts = item.Pts, PtsCount = 1 };
        }

        IUpdate updateNewMessage = new TUpdateNewMessage { Pts = item.Pts, PtsCount = 1, Message = m };
        if (item.MessageAction is TMessageActionHistoryClear)
        {
            updateNewMessage = new TUpdateEditMessage { Pts = item.Pts, PtsCount = 1, Message = m };
        }

        return updateNewMessage;
    }

    private List<IUpdate> ToSelfMessageServiceUpdates(
        int messageId,
        long randomId,
        int pts,
        Peer fromPeer,
        Peer toPeer,
        IMessageAction messageAction,
        int date,
        int? replyToMsgId)
    {
        var updateMessageId = new TUpdateMessageID { Id = messageId, RandomId = randomId };
        var updateNewMessage = new TUpdateNewMessage
        {
            Pts = pts,
            PtsCount = 1,
            Message = new TMessageService
            {
                Action = messageAction,
                Date = date,
                FromId = fromPeer.ToPeer(),
                Out = true,
                PeerId = toPeer.ToPeer(),
                Id = messageId,
                ReplyTo = ToMessageReplyHeader(replyToMsgId, null)
            }
        };

        return [updateMessageId, updateNewMessage];
    }

    //private IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent,
    //    bool createForSelf)
    //{
    //    if (aggregateEvent.ToPeer.PeerType == PeerType.Channel)
    //    {
    //        var updatePinnedChannelMessages = new TUpdatePinnedChannelMessages
    //        {
    //            Pinned = aggregateEvent.Pinned,
    //            ChannelId = aggregateEvent.ToPeer.PeerId,
    //            Messages = new TVector<int>(aggregateEvent.MessageId),
    //            Pts = aggregateEvent.Pts,
    //            PtsCount = 1
    //        };
    //        return new TUpdateShort { Update = updatePinnedChannelMessages, Date = aggregateEvent.Date };
    //    }

    //    var toPeer = aggregateEvent.ToPeer;
    //    if (!createForSelf)
    //    {
    //        toPeer = new Peer(PeerType.User, aggregateEvent.SenderPeerId);
    //    }

    //    var updatePinnedMessages = new TUpdatePinnedMessages
    //    {
    //        Messages = new TVector<int>(aggregateEvent.MessageId),
    //        Pts = aggregateEvent.Pts,
    //        Peer = toPeer.ToPeer(),
    //        Pinned = aggregateEvent.Pinned,
    //        PtsCount = 1
    //    };
    //    var updates = new TUpdates
    //    {
    //        Chats = [],
    //        Date = aggregateEvent.Date,
    //        Updates = new TVector<IUpdate>(updatePinnedMessages),
    //        Seq = 0,
    //        Users = []
    //    };
    //    return updates;
    //}

    public IMessageReplyHeader? ToMessageReplyHeader(int? replyToMsgId,
        int? topMsgId)
    {
        if (replyToMsgId > 0)
        {
            return new TMessageReplyHeader
            {
                ReplyToMsgId = replyToMsgId.Value,
                ReplyToTopId = topMsgId /*, ForumTopic = topMsgId.HasValue*/
            };
        }

        return null;
    }
}