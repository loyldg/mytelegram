namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.invokeWebViewCustomMethod" />
///</summary>
internal sealed class InvokeWebViewCustomMethodHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestInvokeWebViewCustomMethod, MyTelegram.Schema.IDataJSON>
{
    protected override Task<MyTelegram.Schema.IDataJSON> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestInvokeWebViewCustomMethod obj)
    {
        throw new NotImplementedException();
    }
}
