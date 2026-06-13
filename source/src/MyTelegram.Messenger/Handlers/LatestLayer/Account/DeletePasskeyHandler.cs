namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.deletePasskey"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeletePasskeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeletePasskey, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestDeletePasskey obj)
    {
        return new TBoolTrue();
    }
}