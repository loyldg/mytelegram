// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Fetch saved notification sounds
/// See <a href="https://corefork.telegram.org/method/account.getSavedRingtones" />
///</summary>
internal sealed class GetSavedRingtonesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSavedRingtones, MyTelegram.Schema.Account.ISavedRingtones>,
    Account.IGetSavedRingtonesHandler
{
    protected override Task<MyTelegram.Schema.Account.ISavedRingtones> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetSavedRingtones obj)
    {
        throw new NotImplementedException();
    }
}
