namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Report a peer for violation of telegram's Terms of Service
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.reportPeer" />
///</summary>
internal sealed class ReportPeerHandler(ILogger<ReportPeerHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReportPeer, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReportPeer obj)
    {
        logger.LogInformation("ReportPeerHandler peer: {Peer}, reason: {@Reason}, message: {Message}", obj.Peer,obj.Reason,obj.Message);

        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
