namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Report a story.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.report"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReportHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestReport, MyTelegram.Schema.IReportResult>
{
    protected override Task<MyTelegram.Schema.IReportResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestReport obj)
    {
        throw new NotImplementedException();
    }
}