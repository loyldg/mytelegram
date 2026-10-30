
namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CancelWebTokenAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCancelWebTokenAuthorization, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestCancelWebTokenAuthorization obj)
    {
        throw new NotImplementedException();
    }
}

