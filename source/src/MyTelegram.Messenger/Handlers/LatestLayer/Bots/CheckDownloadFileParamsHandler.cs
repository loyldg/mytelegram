namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.checkDownloadFileParams" />
///</summary>
internal sealed class CheckDownloadFileParamsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestCheckDownloadFileParams, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestCheckDownloadFileParams obj)
    {
        throw new NotImplementedException();
    }
}
