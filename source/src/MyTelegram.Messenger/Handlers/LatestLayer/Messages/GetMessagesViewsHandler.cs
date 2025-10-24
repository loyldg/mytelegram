namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

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
internal sealed class GetMessagesViewsHandler(
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IChannelMessageViewsAppService channelMessageViewsAppService,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesViews,
            MyTelegram.Schema.Messages.IMessageViews>
{
    protected override async Task<MyTelegram.Schema.Messages.IMessageViews> HandleCoreAsync(IRequestInput input,
        RequestGetMessagesViews obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        if (peer.PeerType == PeerType.Channel)
        {
            if (obj.Id.Max() < 0)
            {
                return new MyTelegram.Schema.Messages.TMessageViews
                {
                    Views = [.. obj.Id.Select(p => new Schema.TMessageViews { Views = 1 })
                        .ToList()],
                    Chats = [],
                    Users = []
                };
            }

            var views = await channelMessageViewsAppService
                .GetMessageViewsAsync(input.UserId, input.PermAuthKeyId, peer.PeerId, obj.Id.ToList());
            return new MyTelegram.Schema.Messages.TMessageViews
            {
                Chats = [],
                Users = [],
                Views = [.. views]
            };
        }

        var boxIdList = obj.Id.Select(p => MessageId.Create(input.UserId, p).Value).ToList();
        var messages = await queryProcessor
            .ProcessAsync(new GetMessagesByIdListQuery(boxIdList));
        var dict = messages.ToDictionary(k => k.MessageId, v => v);
        return new MyTelegram.Schema.Messages.TMessageViews
        {
            Chats = [],
            Users = [],
            Views = [.. obj.Id.Select(p =>
            {
                dict.TryGetValue(p, out var box);

                return new Schema.TMessageViews
                {
                    Views = box?.Views ?? 0
                };
            })]
        };
    }
}
