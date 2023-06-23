// ReSharper disable All

using MyTelegram.MessengerServer.DomainEventHandlers.Converters;
using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class GetSendAsHandler : RpcResultObjectHandler<Schema.Channels.RequestGetSendAs, Schema.Channels.ISendAsPeers>,
    Channels.IGetSendAsHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlUserConverter _userConverter;

    public GetSendAsHandler(IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        ITlChatConverter chatConverter,
        ITlUserConverter userConverter)
    {
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _chatConverter = chatConverter;
        _userConverter = userConverter;
    }

    protected override async Task<Schema.Channels.ISendAsPeers> HandleCoreAsync(IRequestInput input,
        Schema.Channels.RequestGetSendAs obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer);
        if (peer.PeerType == PeerType.Channel)
        {
            var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(peer.PeerId), default)
                ;
            // Only channel creator can send as channel peer
            if (channelReadModel.CreatorId == input.UserId)
            {
                var channel = _chatConverter.ToChannel(channelReadModel, null, input.UserId, false);
                return new TSendAsPeers
                {
                    Chats = new TVector<IChat>(channel),
                    Peers = new TVector<ISendAsPeer>(new TSendAsPeer
                    {
                        Peer = new TPeerChannel { ChannelId = channelReadModel.ChannelId }
                    }),
                    Users = new TVector<IUser>()
                };
            }
            else
            {
                var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(input.UserId), default)
                    ;
                return new TSendAsPeers
                {
                    Chats = new TVector<IChat>(),
                    Peers = new TVector<ISendAsPeer>(new TSendAsPeer
                    {
                        Peer = new TPeerUser() { UserId = input.UserId }
                    }),
                    Users = new TVector<IUser>(_userConverter.ToUser(userReadModel!, input.UserId))
                };
            }
        }

        throw new NotImplementedException();
    }
}