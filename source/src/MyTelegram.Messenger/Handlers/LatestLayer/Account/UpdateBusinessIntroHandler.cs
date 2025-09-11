namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessIntro" />
///</summary>
internal sealed class UpdateBusinessIntroHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessIntro, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessIntro obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
