// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.report" />
///</summary>
internal sealed class ReportHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestReport, IBool>,
    Stories.IReportHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestReport obj)
    {
        throw new NotImplementedException();
    }
}
