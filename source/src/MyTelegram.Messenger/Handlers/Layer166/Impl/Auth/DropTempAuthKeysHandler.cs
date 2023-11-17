// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Delete all temporary authorization keys <strong>except for</strong> the ones specified
/// See <a href="https://corefork.telegram.org/method/auth.dropTempAuthKeys" />
///</summary>
internal sealed class DropTempAuthKeysHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestDropTempAuthKeys, IBool>,
    Auth.IDropTempAuthKeysHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestDropTempAuthKeys obj)
    {
        throw new NotImplementedException();
    }
}
