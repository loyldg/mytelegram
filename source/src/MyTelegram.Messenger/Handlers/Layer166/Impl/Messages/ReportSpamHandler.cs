// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Report a new incoming chat for spam, if the <a href="https://corefork.telegram.org/constructor/peerSettings">peer settings</a> of the chat allow us to do that
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reportSpam" />
///</summary>
internal sealed class ReportSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportSpam, IBool>,
    Messages.IReportSpamHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReportSpam obj)
    {
        throw new NotImplementedException();
    }
}
