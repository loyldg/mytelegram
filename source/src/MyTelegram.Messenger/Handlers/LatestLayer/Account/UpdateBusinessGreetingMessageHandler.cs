namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessGreetingMessage" />
///</summary>
internal sealed class UpdateBusinessGreetingMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessGreetingMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessGreetingMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
