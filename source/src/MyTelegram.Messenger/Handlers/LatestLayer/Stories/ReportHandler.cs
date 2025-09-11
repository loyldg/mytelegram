namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.report" />
///</summary>
internal sealed class ReportHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestReport, MyTelegram.Schema.IReportResult>
{
    protected override Task<MyTelegram.Schema.IReportResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestReport obj)
    {
        throw new NotImplementedException();
    }
}
