// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get and increase the view counter of a message sent or forwarded from a <a href="https://corefork.telegram.org/api/channel">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getMessagesViews" />
///</summary>
internal sealed class GetMessagesViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesViews, MyTelegram.Schema.Messages.IMessageViews>,
    Messages.IGetMessagesViewsHandler
{
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IChannelMessageViewsAppService _channelMessageViewsAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetMessagesViewsHandler(
        IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        IChannelMessageViewsAppService channelMessageViewsAppService,
        IAccessHashHelper accessHashHelper)
    {
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _channelMessageViewsAppService = channelMessageViewsAppService;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<MyTelegram.Schema.Messages.IMessageViews> HandleCoreAsync(IRequestInput input,
        RequestGetMessagesViews obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        // todo:increment==false,only execute query
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        if (peer.PeerType == PeerType.Channel)
        {
            if (obj.Id.Max() < 0)
            {
                return new MyTelegram.Schema.Messages.TMessageViews
                {
                    Views = new TVector<Schema.IMessageViews>(obj.Id.Select(p => new Schema.TMessageViews { Views = 1 })
                        .ToList()),
                    Chats = new TVector<IChat>(),
                    Users = new TVector<IUser>()
                };
            }
            //var id = obj.Id.First(p => p > 0);
            //var command = new StartIncrementViewsCommand(
            //    MessageBoxId.Create(peer.PeerId, id),
            //    input.ReqMsgId,
            //    input.UserId,
            //    obj.Id, Guid.NewGuid());
            //await _commandBus.PublishAsync(command, CancellationToken.None);
            //return null!;

            var views = await _channelMessageViewsAppService
                .GetMessageViewsAsync(input.UserId, input.PermAuthKeyId, peer.PeerId, obj.Id.ToList())
         ;
            return new MyTelegram.Schema.Messages.TMessageViews
            {
                Chats = new TVector<IChat>(),
                Users = new TVector<IUser>(),
                Views = new TVector<Schema.IMessageViews>(views)
            };
        }

        var boxIdList = obj.Id.Select(p => MessageId.Create(input.UserId, p).Value).ToList();
        var messages = await _queryProcessor
            .ProcessAsync(new GetMessagesByIdListQuery(boxIdList), default);
        var dict = messages.ToDictionary(k => k.MessageId, v => v);
        return new MyTelegram.Schema.Messages.TMessageViews
        {
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Views = new TVector<Schema.IMessageViews>(obj.Id.Select(p =>
            {
                dict.TryGetValue(p, out var box);

                return new Schema.TMessageViews
                {
                    Views = box?.Views ?? 0
                    //Replies = new TMessageReplies
                    //{
                    //    ChannelId = peer.PeerId,
                    //    Comments = true,
                    //}
                };
            }))
        };
        throw new NotImplementedException();
    }
}
