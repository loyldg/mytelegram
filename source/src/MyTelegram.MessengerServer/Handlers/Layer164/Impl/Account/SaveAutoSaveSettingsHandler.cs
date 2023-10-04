// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

internal sealed class SaveAutoSaveSettingsHandler : RpcResultObjectHandler<RequestSaveAutoSaveSettings, IBool>,
    Account.ISaveAutoSaveSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveAutoSaveSettings obj)
    {
        throw new NotImplementedException();
    }
}