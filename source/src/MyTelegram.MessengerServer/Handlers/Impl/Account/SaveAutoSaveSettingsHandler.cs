// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class SaveAutoSaveSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoSaveSettings, IBool>,
    Account.ISaveAutoSaveSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveAutoSaveSettings obj)
    {
        throw new NotImplementedException();
    }
}
