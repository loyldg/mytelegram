
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ReportMusicListenHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportMusicListen, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReportMusicListen obj)
    {
        throw new NotImplementedException();
    }
}

