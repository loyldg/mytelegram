namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessWorkHours" />
///</summary>
internal sealed class UpdateBusinessWorkHoursHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
