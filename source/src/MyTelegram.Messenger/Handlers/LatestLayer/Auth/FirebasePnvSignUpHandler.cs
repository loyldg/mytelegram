
namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class FirebasePnvSignUpHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestFirebasePnvSignUp, MyTelegram.Schema.Auth.IAuthorization>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestFirebasePnvSignUp obj)
    {
        throw new NotImplementedException();
    }
}

