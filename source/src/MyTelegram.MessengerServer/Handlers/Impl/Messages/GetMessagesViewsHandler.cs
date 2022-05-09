using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IMessageViews = MyTelegram.Schema.Messages.IMessageViews;
using TMessageViews = MyTelegram.Schema.Messages.TMessageViews;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetMessagesViewsHandler : RpcResultObjectHandler<RequestGetMessagesViews, IMessageViews>,
    IGetMessagesViewsHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly IChannelMessageViewsAppService _channelMessageViewsAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetMessagesViewsHandler(
        IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        IChannelMessageViewsAppService channelMessageViewsAppService)
    {
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _channelMessageViewsAppService = channelMessageViewsAppService;
    }

    protected override async Task<IMessageViews> HandleCoreAsync(IRequestInput input,
        RequestGetMessagesViews obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        if (peer.PeerType == PeerType.Channel)
        {
            if (obj.Id.Max() < 0)
            {
                return new TMessageViews
                {
                    Views = new TVector<Schema.IMessageViews>(obj.Id.Select(_ => new Schema.TMessageViews { Views = 1 })
                        .ToList()),
                    Chats = new TVector<IChat>(),
                    Users = new TVector<IUser>()
                };
            }

            var views = await _channelMessageViewsAppService
                .GetMessageViewsAsync(input.UserId, input.PermAuthKeyId, peer.PeerId, obj.Id.ToList())
                .ConfigureAwait(false);
            return new TMessageViews
            {
                Chats = new TVector<IChat>(),
                Users = new TVector<IUser>(),
                Views = new TVector<Schema.IMessageViews>(views)
            };
        }

        var messageIdList = obj.Id.Select(p => MessageId.Create(input.UserId, p).Value).ToList();
        var messages = await _queryProcessor
            .ProcessAsync(new GetMessagesByIdListQuery(messageIdList), default).ConfigureAwait(false);
        var dict = messages.ToDictionary(k => k.MessageId, v => v);
        return new TMessageViews
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
    }
}
