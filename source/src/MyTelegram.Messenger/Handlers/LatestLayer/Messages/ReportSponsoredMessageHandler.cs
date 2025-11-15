namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Report a <a href="https://corefork.telegram.org/api/sponsored-messages">sponsored message »</a>, see <a href="https://corefork.telegram.org/api/sponsored-messages#reporting-sponsored-messages">here »</a> for more info on the full flow.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.reportSponsoredMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReportSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportSponsoredMessage, MyTelegram.Schema.Channels.ISponsoredMessageReportResult>
{
    protected override Task<MyTelegram.Schema.Channels.ISponsoredMessageReportResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReportSponsoredMessage obj)
    {
        return Task.FromResult<MyTelegram.Schema.Channels.ISponsoredMessageReportResult>(new TSponsoredMessageReportResultReported());
    }
}