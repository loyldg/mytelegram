namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.toggleNoPaidMessagesException" />
///</summary>
internal sealed class ToggleNoPaidMessagesExceptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleNoPaidMessagesException, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestToggleNoPaidMessagesException obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
