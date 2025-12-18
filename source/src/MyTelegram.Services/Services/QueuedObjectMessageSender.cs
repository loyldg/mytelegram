namespace MyTelegram.Services.Services;

public class QueuedObjectMessageSender(
    IMessageQueueProcessor<ISessionMessage> sessionMessageQueueProcessor,
    IAccessHashHelper2 accessHashHelper2)
    : IObjectMessageSender, ITransientDependency
{
    public Task PushSessionMessageToAuthKeyIdAsync<TData>(long authKeyId,
        TData data,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0) where TData : IObject
    {
        sessionMessageQueueProcessor.Enqueue(new LayeredAuthKeyIdMessageCreatedIntegrationEvent(
            authKeyId,
                data.ToBytes(),
                pts,
                qts,
                globalSeqNo),
            authKeyId);

        return Task.CompletedTask;
    }

    public Task PushMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0,
        PushData? pushData = null,
        List<long>? excludeUserIds = null
        ) where TData : IObject
    {
        sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent(peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                qts,
                globalSeqNo,
                PushData: pushData,
                excludeUserIds
            ),
            peer.PeerId);

        return Task.CompletedTask;
    }

    public Task SendMessageToPeerAsync<TData>(RequestInfo requestInfo,
        TData data) where TData : IObject
    {
        UpdateAccessHashIfNeeded(requestInfo, data);

        sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(requestInfo.ReqMsgId, Array.Empty<byte>())
        {
            DataObject = data
        },
            requestInfo.PermAuthKeyId);

        return Task.CompletedTask;
    }

    public Task SendFileDataToPeerAsync<TData>(RequestInfo requestInfo,
        TData data) where TData : IObject
    {
        UpdateAccessHashIfNeeded(requestInfo, data);

        sessionMessageQueueProcessor.Enqueue(new FileDataResultResponseReceivedEvent(requestInfo.ReqMsgId, data.ToBytes()),
            requestInfo.PermAuthKeyId);

        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(RequestInfo requestInfo,
        TData data,
        int pts = 0) where TData : IObject
    {
        UpdateAccessHashIfNeeded(requestInfo, data);

        return SendRpcMessageToClientAsync(requestInfo.ReqMsgId, data, pts, requestInfo.PermAuthKeyId);
    }

    public Task SendRpcMessageToClientAsync<TData>(long reqMsgId, TData data, int pts = 0, long permAuthKeyId = 0) where TData : IObject
    {
        var rpcResult = CreateRpcResult(reqMsgId, data);

        sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, Array.Empty<byte>())
        {
            DataObject = rpcResult
        },
            permAuthKeyId);

        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(RequestInfo requestInfo, TData data,
        long authKeyId, long permAuthKeyId, long userId,
        int pts = 0) where TData : IObject
    {
        UpdateAccessHashIfNeeded(requestInfo, data);

        var rpcResult = CreateRpcResult(requestInfo.ReqMsgId, data);
        sessionMessageQueueProcessor.Enqueue(
            new DataResultResponseWithUserIdReceivedEvent(requestInfo.ReqMsgId, rpcResult.ToBytes(), userId, authKeyId,
                permAuthKeyId),
            requestInfo.PermAuthKeyId);

        return Task.CompletedTask;
    }

    private TRpcResult CreateRpcResult<TData>(long reqMsgId, TData data) where TData : IObject
    {
        //var newData = data;
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };

        //var length = data.GetLength();
        //if (length > 500)
        //{
        //    var gzipPacked = new TGzipPacked
        //    {
        //        PackedData = gzipHelper.Compress(newData.ToBytes())
        //    };
        //    rpcResult.Result = gzipPacked;
        //}

        return rpcResult;
    }

    private void UpdateAccessHashIfNeeded(RequestInfo requestInfo, IObject data)
    {
        if (data is IAccessHashOwner o)
        {
            foreach (var hasAccessHash in o.GetAccessHashes())
            {
                hasAccessHash.AccessHash = accessHashHelper2.GenerateAccessHash(requestInfo.UserId,
                    requestInfo.AccessHashKeyId, hasAccessHash.Id, (AccessHashType)hasAccessHash.AccessHashType2);

                //Console.WriteLine($"Update access hash:UserId:{requestInfo.UserId} AccessHashKeyId:{requestInfo.AccessHashKeyId} Id:{hasAccessHash.Id} {hasAccessHash.AccessHashType2} {hasAccessHash.AccessHash}");
            }
        }
    }
}