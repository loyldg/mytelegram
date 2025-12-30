namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class RegisterPasskeyHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestRegisterPasskey, MyTelegram.Schema.IPasskey>
{
    protected override async Task<MyTelegram.Schema.IPasskey> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestRegisterPasskey obj)
    {
        throw new NotImplementedException();
    }
}
