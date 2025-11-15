namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Fetch saved notification sounds
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getSavedRingtones"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedRingtonesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSavedRingtones, MyTelegram.Schema.Account.ISavedRingtones>
{
    protected override Task<MyTelegram.Schema.Account.ISavedRingtones> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetSavedRingtones obj)
    {
        return Task.FromResult<Schema.Account.ISavedRingtones>(new TSavedRingtones { Ringtones = [] });
    }
}