// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class GetSavedRingtonesHandler :
    RpcResultObjectHandler<Schema.Account.RequestGetSavedRingtones, Schema.Account.ISavedRingtones>,
    Account.IGetSavedRingtonesHandler, IProcessedHandler
{
    protected override Task<Schema.Account.ISavedRingtones> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestGetSavedRingtones obj)
    {
        return Task.FromResult<Schema.Account.ISavedRingtones>(new TSavedRingtones
        {
            Ringtones = new TVector<IDocument>()
        });
    }
}
