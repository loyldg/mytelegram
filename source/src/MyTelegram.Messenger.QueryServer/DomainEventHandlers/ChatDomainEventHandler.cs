using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChatDomainEventHandler : DomainEventHandlerBase,
    //ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatCreatedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatAboutEditedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatDeletedEvent>

{
    private readonly IChatEventCacheHelper _chatEventCacheHelper;

    public ChatDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IChatEventCacheHelper chatEventCacheHelper) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _chatEventCacheHelper = chatEventCacheHelper;
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatAboutEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, new TBoolTrue());
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
        //_createdChatDict.TryAdd(domainEvent.AggregateEvent.ChatId, domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChatAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChatId,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.DefaultBannedRights);
    }

    private async Task NotifyUpdateChatAsync(
        //long reqMsgId,
        RequestInfo requestInfo,
        long chatId,
        string sourceId,
        ChatBannedRights rights)
    {
        //var updates = new TUpdates()
        //{
        //    Date = DateTime.UtcNow.ToTimestamp(),
        //    Updates = new TVector<IUpdate>(new TUpdateChat
        //    {
        //        ChatId = chatId
        //    }),
        //    Chats = new TVector<IChat>(),
        //    Users = new TVector<IUser>(),
        //    //Update = new TUpdateChat
        //    //{
        //    //    ChatId=chatId
        //    //}
        //};
        var updates = new TUpdateShort
        {
            Date = DateTime.UtcNow.ToTimestamp(),
            Update = new TUpdateChat { ChatId = chatId }
            //Update = new TUpdateChatDefaultBannedRights
            //{
            //    DefaultBannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(rights),
            //    Peer = new TPeerChat { ChatId = chatId },
            //    Version = 0
            //}
        };

        // todo:这里应该返回TUpdates给发送者,只包含群信息即可
        await SendRpcMessageToClientAsync(requestInfo, updates);

        await PushUpdatesToPeerAsync(new Peer(PeerType.Chat, chatId), updates);
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updateChat = new TUpdateChat
        {
            ChatId = domainEvent.AggregateEvent.ChatId
        };
        var chatForbidden = new TChatForbidden
        {
            Id = domainEvent.AggregateEvent.ChatId,
            Title = domainEvent.AggregateEvent.Title
        };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChat),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(chatForbidden),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
        var peer = new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId);
        await SendMessageToPeerAsync(peer, updates);
    }
}
