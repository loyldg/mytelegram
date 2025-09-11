namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.setCustomVerification" />
///</summary>
internal sealed class SetCustomVerificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetCustomVerification, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetCustomVerification obj)
    {
        throw new NotImplementedException();
    }
}
