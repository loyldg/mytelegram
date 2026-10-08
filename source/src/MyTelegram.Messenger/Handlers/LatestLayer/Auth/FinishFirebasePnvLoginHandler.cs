
namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class FinishFirebasePnvLoginHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestFinishFirebasePnvLogin, MyTelegram.Schema.Auth.IAuthorization>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestFinishFirebasePnvLogin obj)
    {
        throw new NotImplementedException();
    }
}

