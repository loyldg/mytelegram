
namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ReportMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestReportMessage, MyTelegram.Schema.IReportResult>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IReportResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestReportMessage obj)
    {
        throw new NotImplementedException();
    }
}

