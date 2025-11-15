using MyTelegram.Schema.Users;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;
/// <summary>
/// Get songs <a href="https://corefork.telegram.org/api/profile#music">pinned to the user's profile, see here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/users.getSavedMusic"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedMusicHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetSavedMusic, MyTelegram.Schema.Users.ISavedMusic>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Users.ISavedMusic> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Users.RequestGetSavedMusic obj)
    {
        return new TSavedMusic
        {
            Count = 0,
            Documents = []
        };
    }
}