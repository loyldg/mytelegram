namespace MyTelegram.Services.Services;

public interface IObjectMessageSender
{
    Task PushMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0,
        PushData? pushData = null,
        List<long>? excludeUserIds = null) where TData : IObject;


    Task PushSessionMessageToAuthKeyIdAsync<TData>(long authKeyId,
        TData data,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0
    ) where TData : IObject;

    Task SendFileDataToPeerAsync<TData>(RequestInfo requestInfo,
        TData data) where TData : IObject;

    Task SendMessageToPeerAsync<TData>(RequestInfo requestInfo,
        TData data) where TData : IObject;

    Task SendRpcMessageToClientAsync<TData>(
        RequestInfo requestInfo,
        TData data,
        int pts = 0) where TData : IObject;

    Task SendRpcMessageToClientAsync<TData>(
        string connectionId,
        long tempAuthKeyId,
        long sessionId,
        long reqMsgId,
        TData data,
        int pts = 0,
        long permAuthKeyId = 0
    ) where TData : IObject;

    Task SendRpcMessageToClientAsync<TData>(
        RequestInfo requestInfo,
        TData data,
        long authKeyId,
        long permAuthKeyId,
        long userId,
        int pts = 0) where TData : IObject;
}