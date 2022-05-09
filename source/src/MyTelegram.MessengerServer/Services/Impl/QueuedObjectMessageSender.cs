namespace MyTelegram.MessengerServer.Services.Impl;

public class QueuedObjectMessageSender : IObjectMessageSender
{
    private readonly IMessageQueueProcessor<ISessionMessage> _sessionMessageQueueProcessor;

    public QueuedObjectMessageSender(IMessageQueueProcessor<ISessionMessage> sessionMessageQueueProcessor)
    {
        _sessionMessageQueueProcessor = sessionMessageQueueProcessor;
    }

    public Task PushSessionMessageToAuthKeyIdAsync(long authKeyId,
        IObject data,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        _sessionMessageQueueProcessor.Enqueue(new PushSessionMessageToAuthKeyIdEvent(authKeyId,
                data.ToBytes(),
                pts,
                ptsType,
                globalSeqNo),
            Random.Shared.Next());
        return Task.CompletedTask;
    }

    public Task PushSessionMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        _sessionMessageQueueProcessor.Enqueue(new PushSessionMessageToPeerEvent((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId,
                pts,
                ptsType,
                globalSeqNo),
            Random.Shared.Next());
        return Task.CompletedTask;
    }

    public Task PushMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0)
    {
        _sessionMessageQueueProcessor.Enqueue(new PushSessionMessageToPeerEvent((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId,
                pts,
                ptsType,
                globalSeqNo),
            Random.Shared.Next());
        return Task.CompletedTask;
    }

    public Task SendMessageToPeerAsync(long reqMsgId,
        IObject data)
    {
        _sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            Random.Shared.Next());
        return Task.CompletedTask;
    }

    public Task SendFileDataToPeerAsync(long reqMsgId,
        IObject data)
    {
        _sessionMessageQueueProcessor.Enqueue(new FileDataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            Random.Shared.Next());
        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync(long reqMsgId,
        IObject data,
        int pts = 0)
    {
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };
        _sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, rpcResult.ToBytes()),
            Random.Shared.Next());
        return Task.CompletedTask;
    }
}