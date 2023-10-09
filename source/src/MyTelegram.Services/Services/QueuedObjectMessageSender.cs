using MyTelegram.Core;
using MyTelegram.Schema;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Services.Services;

public class QueuedObjectMessageSender : IObjectMessageSender
{
    private readonly IGZipHelper _gzipHelper;
    private readonly int _maxQueueCount = 10;
    private readonly IMessageQueueProcessor<ISessionMessage> _sessionMessageQueueProcessor;

    public QueuedObjectMessageSender(IMessageQueueProcessor<ISessionMessage> sessionMessageQueueProcessor,
        IGZipHelper gzipHelper)
    {
        _sessionMessageQueueProcessor = sessionMessageQueueProcessor;
        _gzipHelper = gzipHelper;
    }

    public Task PushSessionMessageToAuthKeyIdAsync<TData>(long authKeyId,
        TData data,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0, LayeredData<TData>? layeredData = null) where TData : IObject
    {
        var layeredByteData = layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes());

        _sessionMessageQueueProcessor.Enqueue(new LayeredAuthKeyIdMessageCreatedIntegrationEvent(authKeyId,
                data.ToBytes(),
                pts,
                //updatesType,
                globalSeqNo, new LayeredData<byte[]>(layeredByteData)),
            authKeyId);
        return Task.CompletedTask;
    }

    public Task PushSessionMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null) where TData : IObject
    {
        _sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                //updatesType,
                globalSeqNo,
                new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes()))),
            peer.PeerId);
        return Task.CompletedTask;
    }

    public Task PushMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0, LayeredData<TData>? layeredData = null) where TData : IObject
    {
        _sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                //updatesType,
                globalSeqNo,
                new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes()))
            ),
            peer.PeerId);
        return Task.CompletedTask;
    }

    public Task PushMessageToPeerAsync<TData, TExtraData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null,
        TExtraData? extraData = default) where TData : IObject
    {
        if (extraData == null)
        {
            return PushMessageToPeerAsync(peer,
                data,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                //updatesType,
                globalSeqNo,
                layeredData);
        }

        _sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent<TExtraData>((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                //ptsType,
                //updatesType,
                globalSeqNo,
                new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes())),
                extraData
            ),
            peer.PeerId);
        return Task.CompletedTask;
    }

    public Task SendMessageToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject
    {
        _sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            reqMsgId % _maxQueueCount);

        return Task.CompletedTask;
    }

    public Task SendFileDataToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject
    {
        _sessionMessageQueueProcessor.Enqueue(new FileDataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(long reqMsgId,
        TData data,
        int pts = 0) where TData : IObject
    {
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };
        _sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, rpcResult.ToBytes()),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(long reqMsgId, TData data,
        long authKeyId, long permAuthKeyId, long userId,
        int pts = 0) where TData : IObject
    {
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };
        _sessionMessageQueueProcessor.Enqueue(new DataResultResponseWithUserIdReceivedEvent(reqMsgId, rpcResult.ToBytes(), userId, authKeyId, permAuthKeyId),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }
}