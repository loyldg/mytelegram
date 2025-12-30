namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class FinishPasskeyLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestFinishPasskeyLogin, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestFinishPasskeyLogin obj)
    {
        throw new NotImplementedException();
    }
}

