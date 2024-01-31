namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class UpdatesConverterLatest : LayeredConverterBase, IUpdatesConverterLatest
{
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IMessageConverter> _layeredMessageService;
    private readonly IObjectMapper _objectMapper;
    private IChatConverter? _chatConverter;
    private IMessageConverter? _messageConverter;

    public UpdatesConverterLatest(
        IObjectMapper objectMapper,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IMessageConverter> layeredMessageService)
    {
        _objectMapper = objectMapper;
        _layeredChatService = layeredChatService;
        _layeredMessageService = layeredMessageService;
    }

    public override int Layer => Layers.LayerLatest;

    public IUpdates ToChannelMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent, bool mentioned = false)
    {
        // selfUser==-1 means the updates is for channel member except sender
        const int selfUserId = -1;
        var updateNewChannelMessage =
            new TUpdateNewChannelMessage
            {
                Message = GetMessageConverter().ToMessage(aggregateEvent.MessageItem,
                    selfUserId,
                    aggregateEvent.LinkedChannelId,
                    aggregateEvent.Pts, mentioned: mentioned),
                Pts = aggregateEvent.Pts,
                PtsCount = 1
            };

        return new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = aggregateEvent.MessageItem.Date,
            Seq = 0,
            Users = new TVector<IUser>(),
            Updates = new TVector<IUpdate>(updateNewChannelMessage)
        };
    }

    public IUpdates ToChannelUpdates(long selfUserId, IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel)
    {
        var channel = GetChatConverter().ToChannel(selfUserId, channelReadModel, photoReadModel, null, false);

        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Updates = new TVector<IUpdate>(new TUpdateChannel { ChannelId = channelReadModel.ChannelId }),
            Users = new TVector<IUser>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public IUpdates ToCreateChannelUpdates(ChannelCreatedEvent eventData,
        SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var updateList = ToChannelMessageServiceUpdates(aggregateEvent.MessageItem.MessageId,
            eventData.RandomId,
            aggregateEvent.Pts,
            null,
            aggregateEvent.MessageItem.OwnerPeer with { PeerType = PeerType.Channel },
            new TMessageActionChannelCreate { Title = eventData.Title },
            aggregateEvent.MessageItem.Date,
            0);
        var updateChannel = new TUpdateChannel { ChannelId = eventData.ChannelId };
        updateList.Insert(1, updateChannel);
        var channel = GetChatConverter().ToChannel(eventData);
        channel.Creator = true;

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Date = aggregateEvent.MessageItem.Date,
            Updates = new TVector<IUpdate>(updateList),
            Users = new TVector<IUser>()
        };
        return updates;
    }

    public IUpdates ToCreateChatUpdates(ChatCreatedEvent eventData,
        SendOutboxMessageCompletedEvent aggregateEvent, IChatReadModel chatReadModel)
    {
        var updates = ToSelfMessageServiceUpdates(aggregateEvent.MessageItem.MessageId,
            eventData.RandomId,
            aggregateEvent.Pts,
            new Peer(PeerType.User, eventData.CreatorUid),
            new Peer(PeerType.Chat, eventData.ChatId),
            new TMessageActionChatCreate
            {
                Title = chatReadModel.Title,
                Users = new TVector<long>(eventData.MemberUidList.Select(p => p.UserId).ToList())
            },
            eventData.Date,
            0
        );
        var chatParticipant = ToChatParticipants(eventData.ChatId,
            eventData.MemberUidList,
            eventData.Date,
            eventData.CreatorUid,
            0);
        var updateChatParticipants = new TUpdateChatParticipants { Participants = chatParticipant };
        var chat = GetChatConverter().ToChat(aggregateEvent.RequestInfo.UserId, chatReadModel, null);
        updates.Add(updateChatParticipants);

        return new TUpdates
        {
            Chats = new TVector<IChat>(chat),
            Date = eventData.Date,
            Seq = 0,
            Updates = new TVector<IUpdate>(updates),
            Users = new TVector<IUser>()
        };
    }

    public IUpdates ToCreateChatUpdates(ChatCreatedEvent eventData,
        ReceiveInboxMessageCompletedEvent aggregateEvent, IChatReadModel chatReadModel)
    {
        var update = ToMessageServiceUpdate(aggregateEvent.MessageItem.MessageId,
            //eventData.RandomId,
            aggregateEvent.Pts,
            aggregateEvent.MessageItem.SenderPeer with { PeerType = PeerType.User },
            aggregateEvent.MessageItem.ToPeer,
            new TMessageActionChatCreate
            {
                Title = chatReadModel.Title,
                Users = new TVector<long>(eventData.MemberUidList.Select(p => p.UserId).ToList())
            },
            eventData.Date,
            0,
            null
        );
        var chat = GetChatConverter().ToChat(aggregateEvent.MessageItem.OwnerPeer.PeerId, chatReadModel, null);
        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(chat),
            Date = aggregateEvent.MessageItem.Date,
            Users = new TVector<IUser>(),
            Updates = new TVector<IUpdate>(update)
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
                    Messages = new TVector<int>(item.DeletedMessageIdList),
                    Pts = item.Pts,
                    PtsCount = item.PtsCount
                }
            };
        }

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(new TUpdateDeleteMessages
            {
                Messages = new TVector<int>(item.DeletedMessageIdList),
                Pts = item.Pts,
                PtsCount = item.PtsCount
            }),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = date,
            Seq = 0
        };
    }

    public virtual IUpdates ToDraftsUpdates(IReadOnlyCollection<IDraftReadModel> draftReadModels)
    {
        var draftUpdates = draftReadModels.Select(p => new TUpdateDraftMessage
        {
            Draft = new TDraftMessage
            {
                Date = p.Draft.Date,
                Message = p.Draft.Message,
                Entities = p.Draft.Entities.ToTObject<TVector<IMessageEntity>>(),
                NoWebpage = p.Draft.NoWebpage,
                ReplyTo = new TInputReplyToMessage
                {
                    ReplyToMsgId = p.Draft.ReplyToMsgId ?? 0
                }
                //ReplyToMsgId = p.Draft.ReplyToMsgId
            },
            Peer = p.Peer.ToPeer(),
            TopMsgId = p.Draft.TopMsgId
        });

        return new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Users = new TVector<IUser>(),
            Updates = new TVector<IUpdate>(draftUpdates)
        };
    }

    public IUpdates ToEditUpdates(OutboxMessageEditCompletedEvent aggregateEvent,
        long selfUserId)
    {
        IUpdate update = aggregateEvent.ToPeer.PeerType switch
        {
            PeerType.Channel => new TUpdateEditChannelMessage
            {
                Pts = aggregateEvent.Pts,
                PtsCount = 1,
                Message = GetMessageConverter().ToMessage(aggregateEvent, selfUserId)
            },
            _ => new TUpdateEditMessage
            {
                Message = GetMessageConverter().ToMessage(aggregateEvent, selfUserId),
                Pts = aggregateEvent.Pts,
                PtsCount = 1
            }
        };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public IUpdates ToEditUpdates(InboxMessageEditCompletedEvent aggregateEvent)
    {
        var update = new TUpdateEditMessage
        { Message = GetMessageConverter().ToMessage(aggregateEvent), Pts = aggregateEvent.Pts, PtsCount = 1 };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        return ToInboxForwardMessageUpdates(aggregateEvent.MessageItem, aggregateEvent.Pts);
    }

    public IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedEvent aggregateEvent,
        StartInviteToChannelEvent startInviteToChannelEvent,
        IChannelReadModel channelReadModel,
        //IChat channel,
        bool createUpdatesForSelf)
    {
        var item = aggregateEvent.MessageItem;
        var updateList = ToChannelMessageServiceUpdates(item.MessageId,
            item.RandomId,
            aggregateEvent.Pts,
            item.SenderPeer,
            item.ToPeer,
            new TMessageActionChatAddUser { Users = new TVector<long>(startInviteToChannelEvent.MemberUidList) },
            item.Date,
            0,
            createUpdatesForSelf
        );
        // privacy restricted updates
        if (startInviteToChannelEvent.PrivacyRestrictedUserId?.Count > 0)
        {
            updateList.AddRange(startInviteToChannelEvent.PrivacyRestrictedUserId.Select(p =>
                new TUpdateGroupInvitePrivacyForbidden
                {
                    UserId = p
                }));
        }

#warning Set channel photo
        var channel = GetChatConverter().ToChannel(
            createUpdatesForSelf ? item.SenderPeer.PeerId : 0,
            channelReadModel,
            null,
            null, false);

        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Date = item.Date,
            Updates = new TVector<IUpdate>(updateList),
            Users = new TVector<IUser>()
        };
    }

    public IUpdates ToInviteToChannelUpdates(
        //IChannelReadModel channelReadModel,
        IChat channel,
        IUserReadModel senderUserReadModel,
        int date)
    {
        var update = new TUpdateChannel { ChannelId = channel.Id };
        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Users = new TVector<IUser>(),
            Date = date,
            Updates = new TVector<IUpdate>(update)
        };
    }

    public IUpdates ToMigrateChatUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent, long channelId)
    {
        var m = aggregateEvent.MessageItem;
        var updateNewMessage = new TUpdateNewMessage
        {
            Pts = aggregateEvent.Pts,
            PtsCount = 1,
            Message = new TMessageService
            {
                Id = m.MessageId,
                FromId = new TPeerUser { UserId = m.SenderPeer.PeerId },
                PeerId = new TPeerChat { ChatId = m.ToPeer.PeerId },
                Date = m.Date,
                Action = new TMessageActionChatMigrateTo { ChannelId = channelId }
            }
        };
        var updateReadHistoryInbox = new TUpdateReadHistoryInbox
        {
            Peer = new TPeerChat { ChatId = m.ToPeer.PeerId },
            MaxId = m.MessageId,
            Pts = aggregateEvent.Pts + 1,
            PtsCount = 1
        };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateNewMessage, updateReadHistoryInbox),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = m.Date
        };

        return updates;
    }
    public IUpdates ToMigrateChatUpdates(SendOutboxMessageCompletedEvent aggregateEvent,
        IChannelReadModel channelReadModel, IChatReadModel chatReadModel)
    {
        // send from chat
        var m = aggregateEvent.MessageItem;
        var chatId = chatReadModel.ChatId;
        var channelId = channelReadModel.ChannelId;
        var updateChannel = new TUpdateChannel { ChannelId = channelId };
        var updateNotifySettings = new TUpdateNotifySettings
        {
            Peer = new TNotifyPeer
            {
                Peer = new TPeerChannel { ChannelId = channelId }
            },
            NotifySettings = new TPeerNotifySettings()
        };
        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = channelId,
            MaxId = 1,
            Pts = 2
        };
        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = 2,
            PtsCount = 1,
            Message = new TMessageService
            {
                Out = true,
                Id = 1,
                PeerId = new TPeerChannel { ChannelId = channelId },
                Date = aggregateEvent.MessageItem.Date,
                Action = new TMessageActionChannelMigrateFrom
                {
                    ChatId = chatId,
                    Title = channelReadModel.Title
                }
            }
        };
        var updateNewMessage = new TUpdateNewMessage
        {
            Pts = aggregateEvent.Pts,
            PtsCount = 1,
            Message = new TMessageService
            {
                Out = true,
                Id = m.MessageId,
                FromId = new TPeerUser { UserId = m.SenderPeer.PeerId },
                PeerId = new TPeerChat { ChatId = chatId },
                Date = m.Date,
                Action = new TMessageActionChatMigrateTo { ChannelId = channelId }
            }
        };
        var updateReadHistoryOutbox = new TUpdateReadHistoryOutbox
        {
            Peer = new TPeerChat { ChatId = chatId },
            MaxId = m.MessageId,
            Pts = aggregateEvent.Pts,
            PtsCount = 1
        };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChannel, updateNotifySettings, updateReadChannelInbox,
                updateNewChannelMessage,
                updateNewMessage,
                updateReadHistoryOutbox
            ),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = aggregateEvent.MessageItem.Date
        };

        return updates;
    }
    public IUpdates ToReadHistoryUpdates(ReadHistoryCompletedEvent eventData)
    {
        var peer = eventData.ReaderToPeer.PeerType == PeerType.User
            ? new TPeerUser { UserId = eventData.ReaderUserId }
            : eventData.ReaderToPeer.ToPeer();
        var updateReadHistoryOutbox = new TUpdateReadHistoryOutbox
        {
            Pts = eventData.SenderPts,
            MaxId = eventData.SenderMessageId,
            PtsCount = 1,
            Peer = peer
        };

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Updates = new TVector<IUpdate>(updateReadHistoryOutbox),
            Users = new TVector<IUser>(),
            Seq = 0
        };

        return updates;
    }

    public IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (aggregateEvent.MessageItem.Media?.Length > 0)
        {
            return ToSelfUpdates(aggregateEvent);
        }

        var item = aggregateEvent.MessageItem;
        switch (item.ToPeer.PeerType)
        {
            case PeerType.Self:
            case PeerType.User:
                {
                    var updates = new TUpdateShortMessage
                    {
                        Out = true,
                        Id = item.MessageId,
                        UserId = item.ToPeer.PeerId,
                        Message = item.Message,
                        Pts = aggregateEvent.Pts,
                        PtsCount = 1,
                        Date = item.Date,
                        FwdFrom =
                            item.FwdHeader == null
                                ? null
                                : _objectMapper.Map<MessageFwdHeader, TMessageFwdHeader>(item.FwdHeader),
                        ReplyTo = GetMessageConverter().ToMessageReplyHeader(item.InputReplyTo),
                        Entities = item.Entities.ToTObject<TVector<IMessageEntity>>()
                    };

                    return updates;
                }
            //break;
            case PeerType.Chat:
                //case PeerType.Channel:
                {
                    var updates = new TUpdateShortChatMessage
                    {
                        Out = true,
                        Id = item.MessageId,
                        FromId = item.SenderPeer.PeerId,
                        ChatId = item.ToPeer.PeerId,
                        Message = item.Message,
                        Pts = aggregateEvent.Pts,
                        PtsCount = 1,
                        Date = item.Date,
                        FwdFrom =
                            item.FwdHeader == null
                                ? null
                                : _objectMapper.Map<MessageFwdHeader, TMessageFwdHeader>(item.FwdHeader),
                        ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                        Entities = item.Entities.ToTObject<TVector<IMessageEntity>>()
                    };
                    return updates;
                }
            //break;
            case PeerType.Channel:
                {
                    var updateReadChannelInbox = new TUpdateReadChannelInbox
                    {
                        ChannelId = item.ToPeer.PeerId,
                        MaxId = item.MessageId,
                        // FolderId = 0,
                        Pts = aggregateEvent.Pts,
                        StillUnreadCount = 0
                    };
                    IUpdate updateNewChannelMessage = new TUpdateNewChannelMessage
                    {
                        Pts = aggregateEvent.Pts,
                        PtsCount = 1,
                        Message = GetMessageConverter().ToMessage(item,
                            linkedChannelId: aggregateEvent.LinkedChannelId,
                            pts: aggregateEvent.Pts)
                    };
                    if (item.MessageSubType == MessageSubType.ClearHistory)
                    {
                        updateNewChannelMessage = new TUpdateEditChannelMessage
                        {
                            Pts = aggregateEvent.Pts,
                            PtsCount = 1,
                            Message = GetMessageConverter().ToMessage(item)
                        };
                    }

                    return new TUpdates
                    {
                        Updates = new TVector<IUpdate>(updateReadChannelInbox, updateNewChannelMessage),
                        Users = new TVector<IUser>(),
                        Chats = new TVector<IChat>(),
                        Date = item.Date,
                        Seq = 0
                    };
                }
            default:
                throw new NotSupportedException($"Unsupported peer {item.ToPeer}");
                //break;
        }
    }

    public IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent)
    {
        return ToUpdatePinnedMessageUpdates(aggregateEvent, true);
    }

    public IUpdates ToSelfUpdates(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (aggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            return ToSelfChannelUpdates(aggregateEvent);
        }

        var item = aggregateEvent.MessageItem;
        if (item.Media?.Length > 0 ||
            item.MessageSubType == MessageSubType.ForwardMessage ||
            item.SendMessageType == SendMessageType.MessageService
           )
        {
            var updateMessageId = new TUpdateMessageID { Id = item.MessageId, RandomId = item.RandomId };
            IUpdate updateNewMessage = new TUpdateNewMessage
            {
                Pts = aggregateEvent.Pts,
                PtsCount = 1,
                Message = GetMessageConverter().ToMessage(item)
            };

            if (item.MessageSubType == MessageSubType.ClearHistory)
            {
                updateNewMessage = new TUpdateEditMessage
                {
                    Pts = aggregateEvent.Pts,
                    PtsCount = 1,
                    Message = GetMessageConverter().ToMessage(item)
                };
            }

            return new TUpdates
            {
                Updates = new TVector<IUpdate>(updateMessageId, updateNewMessage),
                Users = new TVector<IUser>(),
                Chats = new TVector<IChat>(),
                Date = item.Date,
                Seq = 0
            };
        }

        var updates = new TUpdateShortSentMessage
        {
            Date = item.Date,
            Entities = item.Entities.ToTObject<TVector<IMessageEntity>>(),
            Id = item.MessageId,
            Out = item.IsOut,
            Pts = aggregateEvent.Pts,
            PtsCount = 1
        };

        return updates;
    }

    public IUpdates ToUpdatePinnedMessageServiceUpdates(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var update = ToMessageServiceUpdate(item.MessageId,
            aggregateEvent.Pts,
            item.Post ? null : item.SenderPeer,
            item.ToPeer,
            new TMessageActionPinMessage(),
            item.Date,
            0,
            item.InputReplyTo);
        return new TUpdates
        {
            Date = item.Date,
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Seq = 0,
            Updates = new TVector<IUpdate>(update)
        };
    }

    public IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent)
    {
        return ToUpdatePinnedMessageUpdates(aggregateEvent, false);
    }

    public IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var updateMessageId = new TUpdateMessageID
        {
            Id = item.MessageId,
            RandomId = item.RandomId
        };

        var updatePinnedMessages = new TUpdatePinnedMessages
        {
            Pinned = true,
            Peer = item.ToPeer.ToPeer(),
            Messages = new TVector<int> { item.InputReplyTo.ToReplyToMsgId() ?? 0 },
            Pts = aggregateEvent.Pts,
            PtsCount = 1
        };

        var messageServiceUpdate = ToMessageServiceUpdate(item.MessageId,
            aggregateEvent.Pts,
            item.Post ? null : item.SenderPeer,
            item.ToPeer,
            new TMessageActionPinMessage(),
            item.Date,
            0,
            item.InputReplyTo);
        return new TUpdates
        {
            Date = item.Date,
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Seq = 0,
            Updates = new TVector<IUpdate>(updateMessageId, updatePinnedMessages, messageServiceUpdate)
        };
    }

    public IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        //var fromPeer = item.SenderPeer;
        //if (item.ToPeer.PeerType == PeerType.Channel)
        //{
        //    fromPeer = null;
        //}

        var update = ToMessageServiceUpdate(item.MessageId,
            aggregateEvent.Pts,
            null,
            item.ToPeer,
            new TMessageActionPinMessage(),
            item.Date,
            item.OwnerPeer.PeerId,
            item.InputReplyTo);
        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Chats = new TVector<IChat>(),
            Date = item.Date,
            Seq = 0,
            Users = new TVector<IUser>()
        };
    }

    public IUpdates ToUpdates(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;

        if (item.SendMessageType == SendMessageType.MessageService)
        {
            if (string.IsNullOrEmpty(item.MessageActionData))
            {
                throw new ArgumentNullException(nameof(aggregateEvent),
                    "MessageActionData can not be null for service message");
            }

            var messageAction = item.MessageActionData.ToBytes().ToTObject<IMessageAction>();
            var update = ToMessageServiceUpdate(item.MessageId,
                aggregateEvent.Pts,
                item.SenderPeer,
                item.ToPeer,
                messageAction,
                item.Date,
                item.OwnerPeer.PeerId,
                item.InputReplyTo);

            var r = new TUpdates
            {
                Chats = new TVector<IChat>(),
                Users = new TVector<IUser>(),
                Date = item.Date,
                Updates = new TVector<IUpdate>(update)
            };
            if (item.MessageSubType == MessageSubType.DeleteChatUser)
            {
                var action = (TMessageActionChatDeleteUser)messageAction;
                if (action.UserId == item.OwnerPeer.PeerId)
                {
                    r.Chats = new TVector<IChat>(new TChatForbidden
                    {
                        Id = item.ToPeer.PeerId,
                        Title = aggregateEvent.ChatTitle
                    });
                }
            }

            return r;
        }

        if (item.Media?.Length > 0)
        {
            // media message and forward message use the same output updates
            return ToInboxForwardMessageUpdates(item, aggregateEvent.Pts);
        }

        if (aggregateEvent.MessageItem.ReplyMarkup != null)
        {
            var update = new TUpdateNewMessage
            {
                Message = GetMessageConverter().ToMessage(item, item.ToPeer.PeerId),
                Pts = aggregateEvent.Pts,
                PtsCount = 1
            };

            return new TUpdates
            {
                Chats = new TVector<IChat>(),
                Date = DateTime.UtcNow.ToTimestamp(),
                Seq = 0,
                Updates = new TVector<IUpdate>(update),
                Users = new TVector<IUser>()
            };
        }

        switch (item.ToPeer.PeerType)
        {
            case PeerType.Self:
            case PeerType.User:
                {
                    var updates = new TUpdateShortMessage
                    {
                        Out = false,
                        Message = item.Message,
                        Date = item.Date,
                        Entities = item.Entities.ToTObject<TVector<IMessageEntity>>(),
                        Id = item.MessageId,
                        UserId = item.SenderPeer.PeerId,
                        Pts = aggregateEvent.Pts,
                        PtsCount = 1,
                        Mentioned = true,
                        ReplyTo = GetMessageConverter().ToMessageReplyHeader(item.InputReplyTo)
                    };

                    return updates;
                }
            case PeerType.Chat:
                {
                    var updates = new TUpdateShortChatMessage
                    {
                        Out = false,
                        Message = item.Message,
                        Date = item.Date,
                        Entities = item.Entities.ToTObject<TVector<IMessageEntity>>(),
                        Id = item.MessageId,
                        //UserId = aggregateEvent.SenderPeerId,
                        FromId = item.SenderPeer.PeerId,
                        ChatId = item.ToPeer.PeerId,
                        Pts = aggregateEvent.Pts,
                        PtsCount = 1,
                        ReplyTo = GetMessageConverter().ToMessageReplyHeader(item.InputReplyTo)
                    };
                    return updates;
                }
            default:
                throw new NotImplementedException();
        }
    }

    protected virtual IChatConverter GetChatConverter()
    {
        return _chatConverter ??= _layeredChatService.GetConverter(GetLayer());
    }

    protected virtual IMessageConverter GetMessageConverter()
    {
        return _messageConverter ??= _layeredMessageService.GetConverter(GetLayer());
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
            Participants = new TVector<IChatParticipant>(participants),
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
        bool createUpdatesForSelf = true)
    {
        var updateMessageId = new TUpdateMessageID { Id = messageId, RandomId = randomId };
        //only in create channel
        //var updateChannel = new TUpdateChannel { ChannelId = peerId};

        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = toPeer.PeerId,
            MaxId = messageId,
            // FolderId = 0,
            Pts = pts,
            StillUnreadCount = 0
        };
        var isOut = createUpdatesForSelf;
        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = pts,
            PtsCount = 1,
            Message = new TMessageService
            {
                Action = messageAction,
                Date = date,
                FromId = fromPeer.ToPeer(),
                Out = isOut,
                PeerId = toPeer.ToPeer(),
                Id = messageId,
                ReplyTo = GetMessageConverter().ToMessageReplyHeader(replyToMsgId, null)
            }
        };

        return new List<IUpdate> { updateMessageId, updateReadChannelInbox, updateNewChannelMessage };
    }
    private IUpdates ToEditUpdates(
        List<ReactionCount>? oldReactions,
        List<Reaction>? recentReactions,
        MessageItem item,
        int pts,
        long selfUserId = 0,
        int? editDate = null,
        bool editHide = false,
        List<UserReaction>? userReactions = null,
        long? linkedChannelId = null
    )
    {
        var reactions = oldReactions; // GetAllReactions(oldReactions, addedReactions, removedReactions);
        var newRecentReactions = recentReactions; // GetRecentReactions(recentReactions, addedReactions);
        IUpdate update = item.ToPeer.PeerType switch
        {
            PeerType.Channel => new TUpdateEditChannelMessage
            {
                Pts = pts,
                PtsCount = 1,
                Message = GetMessageConverter().ToMessage(item,
                    selfUserId,
                    linkedChannelId,
                    reactions: reactions,
                    recentReactions: newRecentReactions,
                    editDate: editDate,
                    editHide: editHide,
                    userReactions: userReactions
                )
            },
            _ => new TUpdateEditMessage
            {
                Pts = pts,
                PtsCount = 1,
                Message = GetMessageConverter().ToMessage(item,
                    reactions: reactions,
                    recentReactions: newRecentReactions,
                    editDate: editDate,
                    editHide: editHide
                )
            }
        };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update, new TUpdateRecentReactions()),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    private IUpdates ToInboxForwardMessageUpdates(MessageItem aggregateEvent,
        int pts)
    {
        var updateNewMessage =
            new TUpdateNewMessage
            { Message = GetMessageConverter().ToMessage(aggregateEvent), Pts = pts, PtsCount = 1 };
        return new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = aggregateEvent.Date,
            Users = new TVector<IUser>(),
            Seq = 0,
            Updates = new TVector<IUpdate>(updateNewMessage)
        };
    }

    private IUpdate ToMessageServiceUpdate(int messageId,
        //long randomId,
        int pts,
        Peer? fromPeer,
        Peer toPeer,
        IMessageAction messageAction,
        int date,
        long selfUserId,
        IInputReplyTo? inputReplyTo)
    {
        var isOut = false;
        if (fromPeer != null)
        {
            isOut = selfUserId == fromPeer.PeerId;
        }

        var m = new TMessageService
        {
            Action = messageAction,
            Date = date,
            FromId = fromPeer.ToPeer(),
            Out = isOut,
            PeerId = toPeer.ToPeer(),
            Id = messageId,
            ReplyTo = GetMessageConverter().ToMessageReplyHeader(inputReplyTo)
        };

        if (toPeer.PeerType == PeerType.Channel)
        {
            if (messageAction is TMessageActionHistoryClear)
            {
                return new TUpdateEditChannelMessage { Message = m, Pts = pts, PtsCount = 1 };
            }

            return new TUpdateNewChannelMessage { Message = m, Pts = pts, PtsCount = 1 };
        }

        IUpdate updateNewMessage = new TUpdateNewMessage { Pts = pts, PtsCount = 1, Message = m };
        if (messageAction is TMessageActionHistoryClear)
        {
            updateNewMessage = new TUpdateEditMessage { Pts = pts, PtsCount = 1, Message = m };
        }

        return updateNewMessage;
    }

    private IUpdates ToSelfChannelUpdates(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var updateMessageId = new TUpdateMessageID { Id = item.MessageId, RandomId = item.RandomId };
        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = item.ToPeer.PeerId,
            MaxId = item.MessageId,
            // FolderId = 0,
            Pts = aggregateEvent.Pts,
            StillUnreadCount = 0
        };
        IUpdate updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = aggregateEvent.Pts,
            PtsCount = 1,
            Message = GetMessageConverter().ToMessage(aggregateEvent.MessageItem,
                linkedChannelId: aggregateEvent.LinkedChannelId,
                pts: aggregateEvent.Pts)
        };
        if (item.MessageSubType == MessageSubType.ClearHistory)
        {
            updateNewChannelMessage = new TUpdateEditChannelMessage
            {
                Pts = aggregateEvent.Pts,
                PtsCount = 1,
                Message = GetMessageConverter().ToMessage(aggregateEvent.MessageItem)
            };
        }

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(updateMessageId, updateReadChannelInbox, updateNewChannelMessage),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = item.Date,
            Seq = 0
        };
    }

    private List<IUpdate> ToSelfMessageServiceUpdates(int messageId,
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
                ReplyTo = GetMessageConverter().ToMessageReplyHeader(replyToMsgId, null)
            }
        };

        return new List<IUpdate> { updateMessageId, updateNewMessage };
    }

    private IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedEvent aggregateEvent,
        bool createForSelf)
    {
        if (aggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            var updatePinnedChannelMessages = new TUpdatePinnedChannelMessages
            {
                Pinned = aggregateEvent.Pinned,
                ChannelId = aggregateEvent.ToPeer.PeerId,
                Messages = new TVector<int>(aggregateEvent.MessageId),
                Pts = aggregateEvent.Pts,
                PtsCount = 1
            };
            return new TUpdateShort { Update = updatePinnedChannelMessages, Date = aggregateEvent.Date };
        }

        var toPeer = aggregateEvent.ToPeer;
        if (!createForSelf)
        {
            toPeer = new Peer(PeerType.User, aggregateEvent.SenderPeerId);
        }

        var updatePinnedMessages = new TUpdatePinnedMessages
        {
            Messages = new TVector<int>(aggregateEvent.MessageId),
            Pts = aggregateEvent.Pts,
            Peer = toPeer.ToPeer(),
            Pinned = aggregateEvent.Pinned,
            PtsCount = 1
        };
        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = aggregateEvent.Date,
            Updates = new TVector<IUpdate>(updatePinnedMessages),
            Seq = 0,
            Users = new TVector<IUser>()
        };
        return updates;
    }
}