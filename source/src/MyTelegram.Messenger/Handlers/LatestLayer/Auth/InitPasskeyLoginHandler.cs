namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class InitPasskeyLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestInitPasskeyLogin, MyTelegram.Schema.Auth.IPasskeyLoginOptions>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Auth.IPasskeyLoginOptions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestInitPasskeyLogin obj)
    {
        throw new NotImplementedException();
    }
}

