// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateColor" />
///</summary>
internal sealed class UpdateColorHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateColor, IBool>,
    Account.IUpdateColorHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateColor obj)
    {
        return new TBoolTrue();
    }
}
