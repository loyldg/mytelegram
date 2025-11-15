namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Fetch the full list of only the IDs of <a href="https://corefork.telegram.org/api/profile#music">songs currently added to the profile, see here »</a> for more info.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getSavedMusicIds"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedMusicIdsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetSavedMusicIds, MyTelegram.Schema.Account.ISavedMusicIds>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Account.ISavedMusicIds> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetSavedMusicIds obj)
    {
        return new TSavedMusicIds
        {
            Ids = []
        };
    }
}