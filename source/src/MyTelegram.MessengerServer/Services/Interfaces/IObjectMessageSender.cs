namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IObjectMessageSender
{
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
    Task PushMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0);

    Task PushSessionMessageToAuthKeyIdAsync(long authKeyId,
        IObject data,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0);

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
    Task PushSessionMessageToPeerAsync(Peer peer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        long globalSeqNo = 0);

    Task SendFileDataToPeerAsync(long reqMsgId,
        IObject data);

    Task SendMessageToPeerAsync(long reqMsgId,
        IObject data);

    Task SendRpcMessageToClientAsync(long reqMsgId,
        IObject data,
        int pts = 0);
}
