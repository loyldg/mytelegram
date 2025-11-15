namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Report a message in a chat for violation of telegram's Terms of Service
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 OPTION_INVALID Invalid option selected.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.report"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReportHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReport, MyTelegram.Schema.IReportResult>
{
    protected override Task<MyTelegram.Schema.IReportResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReport obj)
    {
        return Task.FromResult<MyTelegram.Schema.IReportResult>(new TReportResultReported());
    }
}