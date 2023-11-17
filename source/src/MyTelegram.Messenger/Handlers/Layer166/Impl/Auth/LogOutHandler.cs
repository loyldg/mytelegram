// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Logs out the user.
/// See <a href="https://corefork.telegram.org/method/auth.logOut" />
///</summary>
internal sealed class LogOutHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestLogOut, MyTelegram.Schema.Auth.ILoggedOut>,
    Auth.ILogOutHandler
{
    protected override Task<MyTelegram.Schema.Auth.ILoggedOut> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestLogOut obj)
    {
        throw new NotImplementedException();
    }
}
