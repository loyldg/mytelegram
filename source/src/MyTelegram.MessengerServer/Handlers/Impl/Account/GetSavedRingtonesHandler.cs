// ReSharper disable All

using MyTelegram.Schema.Account;
namespace MyTelegram.Handlers.Account;

public class GetSavedRingtonesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSavedRingtones, MyTelegram.Schema.Account.ISavedRingtones>,
    Account.IGetSavedRingtonesHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Account.ISavedRingtones> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetSavedRingtones obj)
    {
        return Task.FromResult<Schema.Account.ISavedRingtones>(new TSavedRingtones
        {
            Ringtones = new TVector<IDocument>()
        });
    }
}
