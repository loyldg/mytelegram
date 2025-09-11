namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessAwayMessage" />
///</summary>
internal sealed class UpdateBusinessAwayMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
