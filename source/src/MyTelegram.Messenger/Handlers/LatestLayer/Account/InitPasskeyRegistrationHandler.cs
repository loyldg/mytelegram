namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class InitPasskeyRegistrationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInitPasskeyRegistration, MyTelegram.Schema.Account.IPasskeyRegistrationOptions>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Account.IPasskeyRegistrationOptions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestInitPasskeyRegistration obj)
    {
        throw new NotImplementedException();
    }
}