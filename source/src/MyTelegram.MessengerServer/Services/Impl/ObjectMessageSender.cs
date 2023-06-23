namespace MyTelegram.MessengerServer.Services.Impl;

public class ObjectMessageSender : IObjectMessageSender
{
    private readonly IEventBus _eventBus;

    public ObjectMessageSender(
        IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task PushSessionMessageToAuthKeyIdAsync(long authKeyId,
        IObject data,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        return _eventBus.PublishAsync(
            new LayeredAuthKeyIdMessageCreatedIntegrationEvent(authKeyId,
                data.ToBytes(),
                pts,
                ptsType,
                globalSeqNo));
    }

    /// <summary>
    ///     Push message to peer,receive server is session server(send from session server)
    /// </summary>
    /// <param name="peer"></param>
    /// <param name="data"></param>
    /// <param name="excludeAuthKeyId"></param>
    /// <param name="excludeUid"></param>
    /// <param name="onlySendToThisAuthKeyId"></param>
    /// <param name="pts"></param>
    /// <param name="ptsType"></param>
    /// <param name="globalSeqNo"></param>
    /// <returns></returns>
    public Task PushSessionMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        return _eventBus.PublishAsync(new LayeredPushMessageCreatedIntegrationEvent((int)peer.PeerType,
            peer.PeerId,
            data.ToBytes(),
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo));
    }

    /// <summary>
    ///     Push message to peer,receive server is push server(send from push server)
    /// </summary>
    /// <param name="peer"></param>
    /// <param name="data"></param>
    /// <param name="excludeAuthKeyId"></param>
    /// <param name="excludeUid"></param>
    /// <param name="onlySendToThisAuthKeyId"></param>
    /// <param name="pts"></param>
    /// <param name="ptsType"></param>
    /// <param name="globalSeqNo"></param>
    /// <returns></returns>
    public Task PushMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        // First save push data to database

        return _eventBus.PublishAsync(new PushMessageToPeerEvent((int)peer.PeerType,
            peer.PeerId,
            data.ToBytes(),
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo));
    }

    public Task SendMessageToPeerAsync(long reqMsgId,
        IObject data)
    {
        return _eventBus.PublishAsync(new DataResultResponseReceivedEvent(reqMsgId, data.ToBytes()));
    }

    public Task SendFileDataToPeerAsync(long reqMsgId,
        IObject data)
    {
        return _eventBus.PublishAsync(new FileDataResultResponseReceivedEvent(reqMsgId, data.ToBytes()));
    }

    public Task SendRpcMessageToClientAsync(long reqMsgId,
        IObject data,
        int pts = 0)
    {
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };

        return _eventBus.PublishAsync(
            new DataResultResponseReceivedEvent(reqMsgId, rpcResult.ToBytes()));
    }
}