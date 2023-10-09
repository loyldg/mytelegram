namespace MyTelegram.Messenger.Services.Caching;

internal sealed class AccessHashHelper : IAccessHashHelper
{
    private readonly ConcurrentDictionary<long, long> _accessHashCaches = new();
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public AccessHashHelper(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
    }

    public void AddAccessHash(long id, long accessHash)
    {
        _accessHashCaches.TryAdd(id, accessHash);
    }

    public async Task<bool> IsAccessHashValidAsync(long id,
        long accessHash)
    {
        if (_accessHashCaches.TryGetValue(id, out var cachedAccessHash))
        {
            return accessHash == cachedAccessHash;
        }

        var accessHashReadModel = await _queryProcessor.ProcessAsync(new GetAccessHashQueryByIdQuery(id), default);

        if (accessHashReadModel != null)
        {
            _accessHashCaches.TryAdd(accessHashReadModel.AccessId, accessHashReadModel.AccessHash);
            return accessHash == accessHashReadModel.AccessHash;
        }

        var peer = _peerHelper.GetPeer(id);
        switch (peer.PeerType)
        {
            case PeerType.User:
                var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(id), default);
                if (userReadModel != null)
                {
                    _accessHashCaches.TryAdd(id, userReadModel.AccessHash);
                    return accessHash == userReadModel.AccessHash;
                }

                break;

            case PeerType.Channel:
                var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(id), default);
                if (channelReadModel != null)
                {
                    _accessHashCaches.TryAdd(id, channelReadModel.AccessHash);
                    return accessHash == channelReadModel.AccessHash;
                }

                break;
        }

        return false;
    }

    public async Task CheckAccessHashAsync(long id,
        long accessHash)
    {
        if (!await IsAccessHashValidAsync(id, accessHash))
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }
    }

    public Task CheckAccessHashAsync(IInputPeer? inputPeer) =>
        inputPeer switch
        {
            TInputPeerChannel inputPeerChannel => CheckAccessHashAsync(inputPeerChannel.ChannelId,
                inputPeerChannel.AccessHash),
            TInputPeerChannelFromMessage inputPeerChannelFromMessage => CheckAccessHashAsync(inputPeerChannelFromMessage
                .Peer),
            TInputPeerUser inputPeerUser => CheckAccessHashAsync(inputPeerUser.UserId, inputPeerUser.AccessHash),
            TInputPeerUserFromMessage inputPeerUserFromMessage => CheckAccessHashAsync(inputPeerUserFromMessage.Peer),
            _ => Task.CompletedTask
        };

    public Task CheckAccessHashAsync(IInputUser inputUser)
    {
        if (inputUser is TInputUser tInputUser)
        {
            return CheckAccessHashAsync(tInputUser.UserId, tInputUser.AccessHash);
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(IInputChannel inputChannel)
    {
        if (inputChannel is TInputChannel tInputChannel)
        {
            return CheckAccessHashAsync(tInputChannel.ChannelId, tInputChannel.AccessHash);
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(Peer peer)
    {
        return CheckAccessHashAsync(peer.PeerId, peer.AccessHash);
    }
}