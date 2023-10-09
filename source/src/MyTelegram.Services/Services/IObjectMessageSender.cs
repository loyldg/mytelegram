using MyTelegram.Core;
using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public interface IObjectMessageSender
{
    Task PushMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId=null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null) where TData : IObject;

    //Task PushMessageToPeerAsync<TData>(Peer peer,
    //    TData data,
    //    IList<long>? excludeAuthKeyIdList = null,
    //    IList<long>? excludeUserIdList = null,
    //    IList<long>? onlySendToAuthKeyIdList = null,
    //    int pts = 0,
    //    PtsType ptsType = PtsType.Unknown,
    //    long globalSeqNo = 0,
    //    LayeredData<TData>? layeredData = null) where TData : IObject;

    Task PushMessageToPeerAsync<TData, TExtraData>(Peer peer,
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
        TExtraData? extraData = default) where TData : IObject;

    //Task PushMessageToPeerAsync<TData, TExtraData>(Peer peer,
    //    TData data,
    //    IList<long>? excludeAuthKeyIdList = null,
    //    IList<long>? excludeUserIdList = null,
    //    IList<long>? onlySendToAuthKeyIdList = null,
    //    int pts = 0,
    //    PtsType ptsType = PtsType.Unknown,
    //    long globalSeqNo = 0,
    //    LayeredData<TData>? layeredData = null,
    //    TExtraData? extraData = default) where TData : IObject;

    Task PushSessionMessageToAuthKeyIdAsync<TData>(long authKeyId,
        TData data,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null
    ) where TData : IObject;

    /// <summary>
    ///     Push message to peer,receive server is session server(send from session server)
    /// </summary>
    /// <returns></returns>
    Task PushSessionMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        //PtsType ptsType = PtsType.Unknown,
        //UpdatesType updatesType = UpdatesType.Updates,
        long globalSeqNo = 0, LayeredData<TData>? layeredData = null) where TData : IObject;

    Task SendFileDataToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject;

    Task SendMessageToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject;

    Task SendRpcMessageToClientAsync<TData>(
        long reqMsgId,
        TData data,
        int pts = 0) where TData : IObject;

    Task SendRpcMessageToClientAsync<TData>(
        long reqMsgId,
        TData data,
        long authKeyId,
        long permAuthKeyId,
        long userId,
        int pts = 0) where TData : IObject;
}