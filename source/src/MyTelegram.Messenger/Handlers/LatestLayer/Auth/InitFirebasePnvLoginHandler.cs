
namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class InitFirebasePnvLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestInitFirebasePnvLogin, MyTelegram.Schema.Auth.IFirebasePnvIntent>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Auth.IFirebasePnvIntent> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestInitFirebasePnvLogin obj)
    {
        throw new NotImplementedException();
    }
}

