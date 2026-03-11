namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 CREDENTIAL_INVALID  
/// <para><c>See <a href="https://corefork.telegram.org/method/account.registerPasskey"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RegisterPasskeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestRegisterPasskey, MyTelegram.Schema.IPasskey>
{
    protected override async Task<MyTelegram.Schema.IPasskey> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestRegisterPasskey obj)
    {
        throw new NotImplementedException();
    }
}