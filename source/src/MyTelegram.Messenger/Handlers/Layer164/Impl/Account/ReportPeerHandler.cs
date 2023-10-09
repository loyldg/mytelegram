// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Report a peer for violation of telegram's Terms of Service
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.reportPeer" />
///</summary>
internal sealed class ReportPeerHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReportPeer, IBool>,
    Account.IReportPeerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReportPeer obj)
    {
        throw new NotImplementedException();
    }
}
