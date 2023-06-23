// ReSharper disable All

using MyTelegram.Schema.Account;
using IAutoSaveSettings = MyTelegram.Schema.Account.IAutoSaveSettings;

namespace MyTelegram.Handlers.Account;

internal sealed class GetAutoSaveSettingsHandler :
    RpcResultObjectHandler<RequestGetAutoSaveSettings, IAutoSaveSettings>,
    Account.IGetAutoSaveSettingsHandler
{
    protected override Task<IAutoSaveSettings> HandleCoreAsync(IRequestInput input,
        RequestGetAutoSaveSettings obj)
    {
        throw new NotImplementedException();
    }
}