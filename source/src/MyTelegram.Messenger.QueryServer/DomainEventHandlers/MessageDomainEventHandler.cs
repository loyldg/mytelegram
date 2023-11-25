using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;
using SendOutboxMessageCompletedEvent = MyTelegram.Domain.Sagas.Events.SendOutboxMessageCompletedEvent;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class MessageDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>,

    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent>


{
    private readonly IChatEventCacheHelper _chatEventCacheHelper;
    private readonly ILayeredService<IChatConverter> _chatLayeredService;

    private readonly ILogger<MessageDomainEventHandler> _logger;

    //private readonly ITlMessageConverter _messageConverter;
    private readonly ILayeredService<IMessageConverter> _messageLayeredService;
    private readonly IPhotoAppService _photoAppService;

    private readonly IQueryProcessor _queryProcessor;

    //private readonly ITlUpdatesConverter _updatesConverter;
    private readonly ILayeredService<IUpdatesConverter> _updatesLayeredService;
    private readonly ILayeredService<IUserConverter> _userLayeredService;
    private readonly IPrivacyAppService _privacyAppService;
    public MessageDomainEventHandler(
        IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        //ITlUpdatesConverter updatesConverter,
        ILayeredService<IMessageConverter> messageLayeredService,
        IChatEventCacheHelper chatEventCacheHelper,
        ILogger<MessageDomainEventHandler> logger,
        IQueryProcessor queryProcessor,
        ILayeredService<IUpdatesConverter> updatesLayeredService, ILayeredService<IChatConverter> chatLayeredService,
        IPhotoAppService photoAppService, ILayeredService<IUserConverter> userLayeredService, IPrivacyAppService privacyAppService) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        //_updatesConverter = updatesConverter;
        _messageLayeredService = messageLayeredService;
        _chatEventCacheHelper = chatEventCacheHelper;
        _logger = logger;
        _queryProcessor = queryProcessor;
        _updatesLayeredService = updatesLayeredService;
        _chatLayeredService = chatLayeredService;
        _photoAppService = photoAppService;
        _userLayeredService = userLayeredService;
        _privacyAppService = privacyAppService;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        //var toPeer = domainEvent.AggregateEvent.ToPeer with { PeerId = domainEvent.AggregateEvent.OwnerPeerId };
        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId);
        var updates = _updatesLayeredService.Converter.ToEditUpdates(domainEvent.AggregateEvent);
        var layeredData = _updatesLayeredService.GetLayeredData(c => c.ToEditUpdates(domainEvent.AggregateEvent));
        return PushUpdatesToPeerAsync(toPeer,
            updates,
            pts: domainEvent.AggregateEvent.Pts,
            //ptsType: PtsType.NewMessages,
            updatesType: UpdatesType.NewMessages,
            layeredData: layeredData
        );
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updates =
            _updatesLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
                .ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId);
        var layeredData = _updatesLayeredService.GetLayeredData(c =>
            c.ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId));
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            updates,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.ToPeer.PeerType
        );
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
            updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.Pts,
            //ptsType: PtsType.NewMessages,
            updatesType: UpdatesType.NewMessages,
            layeredData: layeredData);

        // Channel message shares the same message,edit out message should notify channel member
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            var channelEditUpdates = _updatesLayeredService.Converter.ToEditUpdates(domainEvent.AggregateEvent, 0);
            var layeredChannelEditUpdates =
                _updatesLayeredService.GetLayeredData(c => c.ToEditUpdates(domainEvent.AggregateEvent, 0));
            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                channelEditUpdates,
                pts: domainEvent.AggregateEvent.Pts,
                layeredData: layeredChannelEditUpdates);
        }
    }

    private Task HandleReceiveMessageCompletedAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        return aggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(aggregateEvent),
            MessageSubType.MigrateChat => HandleMigrateChatAsync(aggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(aggregateEvent),
            MessageSubType.ForwardMessage => HandleForwardMessageAsync(aggregateEvent),
            _ => HandleReceiveMessageAsync(aggregateEvent)
        };
    }

    private Task HandleSendOutboxMessageCompletedAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        return aggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(aggregateEvent),
            MessageSubType.CreateChannel => HandleCreateChannelAsync(aggregateEvent),
            MessageSubType.InviteToChannel => HandleInviteToChannelAsync(aggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(aggregateEvent),
            MessageSubType.MigrateChat => HandleMigrateChatAsync(aggregateEvent),
            _ => HandleSendMessageAsync(aggregateEvent)
        };
    }


    private async Task HandleMigrateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var chatId = aggregateEvent.MessageItem.ToPeer.PeerId;
        if (_chatEventCacheHelper.TryGetMigrateChannelId(chatId, out var channelId))
        {
            var chatReadModel = await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(chatId));
            var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
            var chatPhoto = await _photoAppService.GetPhotoAsync(chatReadModel!.PhotoId);

            var updates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToMigrateChatUpdates(aggregateEvent, channelReadModel, chatReadModel!);

            var channel = _chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChannel(aggregateEvent.RequestInfo.UserId, channelReadModel, chatPhoto, null, false);
            var chat = _chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChat(aggregateEvent.RequestInfo.UserId, chatReadModel, chatPhoto);
            if (updates is TUpdates tUpdates)
            {
                tUpdates.Chats.Add(channel);
                tUpdates.Chats.Add(chat);
            }

            var layeredData = _updatesLayeredService.GetLayeredData(c =>
            {
                var selfOtherDeviceUpdates = c.ToMigrateChatUpdates(aggregateEvent, channelReadModel, chatReadModel);
                if (selfOtherDeviceUpdates is TUpdates tSelfOtherDeviceUpdates)
                {
                    tSelfOtherDeviceUpdates.Chats.Add(channel);
                    tSelfOtherDeviceUpdates.Chats.Add(chat);
                }

                return selfOtherDeviceUpdates;
            });

            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                updates,
                pts: aggregateEvent.Pts
            );

            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private async Task HandleCreateChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryRemoveChannelCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId,
                out var eventData))
        {
            //// migrate chat will send two messages,only process send normal group message
            //// 1.create channel->send channel message
            //// 2.migrate chat->send normal group message
            //if (eventData.MigratedFromChat&&aggregateEvent.MessageItem.ToPeer.PeerType== PeerType.Channel)
            //{
            //    _chatEventCacheHelper.Add(eventData);
            //    return;
            //}

            var channelId = eventData.ChannelId;
            var updates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToCreateChannelUpdates(eventData, aggregateEvent);
            var layeredData =
                _updatesLayeredService.GetLayeredData(c => c.ToCreateChannelUpdates(eventData, aggregateEvent));
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                updates,
                aggregateEvent.MessageItem.SenderPeer.PeerId
            );
            await PushUpdatesToChannelSingleMemberAsync(channelId, aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
        else
        {
            _logger.LogWarning("Can not find create channel cache info,channelId={ChannelId}",
                aggregateEvent.MessageItem.ToPeer.PeerId);
        }
    }

    private async Task HandleCreateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var chat = await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));
            var updates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToCreateChatUpdates(eventData, aggregateEvent, chat!);

            var layeredData =
                _updatesLayeredService.GetLayeredData(c => c.ToCreateChatUpdates(eventData, aggregateEvent, chat!));
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                updates,
                pts: aggregateEvent.Pts);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private async Task HandleMigrateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryGetMigrateChannelId(aggregateEvent.MessageItem.ToPeer.PeerId, out var channelId))
        {
            var userId = aggregateEvent.MessageItem.OwnerPeer.PeerId;
            var chatReadModel =
                await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));
            var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
            var chatPhoto = await _photoAppService.GetPhotoAsync(chatReadModel!.PhotoId);

            var updates = _updatesLayeredService.Converter.ToMigrateChatUpdates(aggregateEvent, channelId);
            var latestLayerChannel = _chatLayeredService.Converter
                .ToChannel(userId, channelReadModel, chatPhoto, null, false);
            var latestLayerChat = _chatLayeredService.Converter
                .ToChat(userId, chatReadModel, chatPhoto);

            if (updates is TUpdates tUpdates)
            {
                tUpdates.Chats.Add(latestLayerChannel);
                tUpdates.Chats.Add(latestLayerChat);
            }

            var layeredData =
                _updatesLayeredService.GetLayeredData(c =>
                {
                    var channel = _chatLayeredService.GetConverter(c.RequestLayer)
                        .ToChannel(userId, channelReadModel, chatPhoto, null, false);
                    var chat = _chatLayeredService.GetConverter(c.RequestLayer)
                        .ToChat(userId, chatReadModel, chatPhoto);

                    var layeredUpdates = c.ToMigrateChatUpdates(aggregateEvent, channelId);
                    if (layeredUpdates is TUpdates tLayeredUpdates)
                    {
                        tLayeredUpdates.Chats.Add(channel);
                        tLayeredUpdates.Chats.Add(chat);
                    }

                    return layeredUpdates;
                });
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private async Task HandleCreateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var chat = await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));

            var updates = _updatesLayeredService.Converter.ToCreateChatUpdates(eventData, aggregateEvent, chat);
            var layeredData =
                _updatesLayeredService.GetLayeredData(c => c.ToCreateChatUpdates(eventData, aggregateEvent, chat));
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private Task HandleForwardMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesLayeredService.Converter.ToInboxForwardMessageUpdates(aggregateEvent);
        var layeredData = _updatesLayeredService.GetLayeredData(c => c.ToInboxForwardMessageUpdates(aggregateEvent));
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts,
            layeredData: layeredData);
    }

    private async Task HandleInviteToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryRemoveStartInviteToChannelEvent(aggregateEvent.MessageItem.ToPeer.PeerId,
                out var startInviteToChannelEvent))
        {
            var channelId = aggregateEvent.MessageItem.ToPeer.PeerId;
            var item = aggregateEvent.MessageItem;
            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(item.ToPeer.PeerId));

            var updates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToInviteToChannelUpdates(
                    aggregateEvent,
                    startInviteToChannelEvent,
                    channelReadModel,
                    true);
            var layeredData = _updatesLayeredService.GetLayeredData(c =>
                c.ToInviteToChannelUpdates(aggregateEvent, startInviteToChannelEvent, channelReadModel, true));

            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                updates,
                item.SenderPeer.PeerId);
            // notify self other devices
            await PushUpdatesToChannelSingleMemberAsync(
                    channelId,
                    item.SenderPeer,
                    updates,
                    excludeAuthKeyId: aggregateEvent.RequestInfo.AuthKeyId,
                    onlySendToUserId: aggregateEvent.RequestInfo.UserId,
                    layeredData: layeredData);

            var updatesForChannelMember = _updatesLayeredService.Converter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel,
                false);
            var layeredUpdatesForChannelMember = _updatesLayeredService.GetLayeredData(c =>
                c.ToInviteToChannelUpdates(aggregateEvent,
                    startInviteToChannelEvent,
                    channelReadModel,
                    false));

            foreach (var channelMemberUserId in startInviteToChannelEvent.MemberUidList)
            {
                await PushUpdatesToChannelSingleMemberAsync(channelId, new Peer(PeerType.User, channelMemberUserId),
                    updatesForChannelMember,
                    onlySendToUserId: channelMemberUserId,
                    pts: aggregateEvent.Pts,
                    layeredData: layeredUpdatesForChannelMember
                );
            }

            // notify channel members
            await PushUpdatesToChannelMemberAsync(
                item.SenderPeer,
                item.ToPeer,
                updatesForChannelMember,
                excludeUserId: item.SenderPeer.PeerId,
                pts: aggregateEvent.Pts,
                layeredData: layeredUpdatesForChannelMember);
        }
    }

    private async Task HandleReceiveMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesLayeredService.Converter.ToUpdates(aggregateEvent);

        var layeredData = _updatesLayeredService.GetLayeredData(c => c.ToUpdates(aggregateEvent));
        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts,
            layeredData: layeredData,
            senderUserId: aggregateEvent.MessageItem.SenderPeer.PeerId
        );
    }

    private async Task HandleSendMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel)
        {
            await HandleSendMessageToChannelAsync(aggregateEvent);
            return;
        }

        var selfUpdates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfUpdates(aggregateEvent);
        //var ptsType = PtsType.OtherUpdates;
        UpdatesType updatesType = UpdatesType.Updates;
        if (item.MessageSubType == MessageSubType.Normal ||
            item.MessageSubType == MessageSubType.ForwardMessage)
        {
            updatesType = UpdatesType.NewMessages;
        }

        var selfLayeredData = _updatesLayeredService.GetLayeredData(c => c.ToSelfUpdates(aggregateEvent));

        // when reqMsgId==0?
        // forward message/bot message
        if (aggregateEvent.RequestInfo.ReqMsgId == 0 || item.MessageSubType == MessageSubType.PhoneCall)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                //ptsType: ptsType,
                updatesType: updatesType,
                layeredData: selfLayeredData
            );
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo,
                item.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                item.SenderPeer.PeerId,
                aggregateEvent.Pts
            );
        }

        var selfOtherDeviceUpdates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfOtherDeviceUpdates(aggregateEvent);
        var layeredSelfOtherDeviceUpdates =
            _updatesLayeredService.GetLayeredData(c => c.ToSelfOtherDeviceUpdates(aggregateEvent));

        await PushUpdatesToPeerAsync(item.SenderPeer,
            selfOtherDeviceUpdates,
            aggregateEvent.RequestInfo.AuthKeyId,
            pts: aggregateEvent.Pts,
            //newMessage: newMessage,
            updatesType: updatesType,
            layeredData: layeredSelfOtherDeviceUpdates
        );
    }

    private async Task HandleSendMessageToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var selfUpdates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfUpdates(aggregateEvent);
        var layeredSelfUpdates = _updatesLayeredService.GetLayeredData(c => c.ToSelfUpdates(aggregateEvent));
        var channelUpdates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToChannelMessageUpdates(aggregateEvent);
        var layeredChannelUpdates =
            _updatesLayeredService.GetLayeredData(c => c.ToChannelMessageUpdates(aggregateEvent));
        //var ptsType = PtsType.OtherUpdates;
        var updatesType = UpdatesType.Updates;
        if (item.MessageSubType == MessageSubType.Normal || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            updatesType = UpdatesType.NewMessages;
        }

        var globalSeqNo = await SavePushUpdatesAsync(item.ToPeer.PeerId,
            channelUpdates,
            aggregateEvent.Pts,
            aggregateEvent.RequestInfo.AuthKeyId,
            aggregateEvent.RequestInfo.UserId,
            messageId: aggregateEvent.MessageItem.MessageId
        );
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(aggregateEvent.RequestInfo.ReqMsgId, item.SenderPeer.PeerId,
                globalSeqNo)
            ;

        if (aggregateEvent.RequestInfo.ReqMsgId == 0 || item.MessageSubType == MessageSubType.ForwardMessage)
        {

            await PushUpdatesToPeerAsync(new Peer(PeerType.User, aggregateEvent.RequestInfo.UserId),
                selfUpdates,
                pts: aggregateEvent.Pts,
                //ptsType: ptsType,
                updatesType: updatesType,
                layeredData: layeredSelfUpdates
            );
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo,
                aggregateEvent.MessageItem.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                aggregateEvent.RequestInfo.UserId,
                aggregateEvent.Pts
            );

            await PushUpdatesToPeerAsync(item.SenderPeer,
                _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                    .ToSelfOtherDeviceUpdates(aggregateEvent),
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                updatesType: updatesType,
                //newMessage: newMessage,
                layeredData: _updatesLayeredService.GetLayeredData(c => c.ToSelfOtherDeviceUpdates(aggregateEvent))
            );
        }

        // notify mentioned users
        if (aggregateEvent.MentionedUserIds?.Count > 0)
        {
            var mentionedUpdates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChannelMessageUpdates(aggregateEvent, true);
            var layeredMentionUpdates =
                _updatesLayeredService.GetLayeredData(c => c.ToChannelMessageUpdates(aggregateEvent, true));
            foreach (var mentionedUserId in aggregateEvent.MentionedUserIds)
            {
                if (mentionedUserId != aggregateEvent.RequestInfo.UserId)
                {
                    await PushUpdatesToPeerAsync(new Peer(PeerType.User, mentionedUserId), mentionedUpdates,
                        layeredData: layeredMentionUpdates);
                }
            }
        }

        await PushUpdatesToPeerAsync(item.ToPeer,
                channelUpdates,
                aggregateEvent.RequestInfo.AuthKeyId,
                updatesType: updatesType,
                layeredData: layeredChannelUpdates,
                skipSaveUpdates: true
                )
            ;
    }

    private Task HandleUpdatePinnedMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesLayeredService.Converter.ToUpdatePinnedMessageUpdates(aggregateEvent);
        var layeredData = _updatesLayeredService.GetLayeredData(c => c.ToUpdatePinnedMessageUpdates(aggregateEvent));
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts,
            layeredData: layeredData);
    }

    private async Task HandleUpdatePinnedMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToUpdatePinnedMessageUpdates(aggregateEvent);
        await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
            updates,
            aggregateEvent.MessageItem.SenderPeer.PeerId,
            aggregateEvent.Pts,
            aggregateEvent.MessageItem.ToPeer.PeerType
        );

        var layeredUpdates = _updatesLayeredService.GetLayeredData(c => c.ToUpdatePinnedMessageUpdates(aggregateEvent));
        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
            updates,
            aggregateEvent.RequestInfo.AuthKeyId,
            pts: aggregateEvent.Pts,
            layeredData: layeredUpdates);

        if (aggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            var channelUpdates = _updatesLayeredService.Converter.ToUpdatePinnedMessageServiceUpdates(aggregateEvent);
            var layeredChannelUpdates =
                _updatesLayeredService.GetLayeredData(c => c.ToUpdatePinnedMessageServiceUpdates(aggregateEvent));
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer,
                channelUpdates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredChannelUpdates);
        }
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return HandleSendOutboxMessageCompletedAsync(domainEvent.AggregateEvent);
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return HandleReceiveMessageCompletedAsync(domainEvent.AggregateEvent);
    }
}