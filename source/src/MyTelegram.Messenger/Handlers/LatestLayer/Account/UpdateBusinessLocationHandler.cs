namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessLocation" />
///</summary>
internal sealed class UpdateBusinessLocationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessLocation, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessLocation obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
