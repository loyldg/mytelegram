using MyTelegram.Schema.Users;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;
/// <summary>
/// Check if the passed songs are still pinned to the user's profile, or refresh the file references of songs pinned on a user's profile <a href="https://corefork.telegram.org/api/profile#music">see here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/users.getSavedMusicByID"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedMusicByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetSavedMusicByID, MyTelegram.Schema.Users.ISavedMusic>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Users.ISavedMusic> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Users.RequestGetSavedMusicByID obj)
    {
        return new TSavedMusic
        {
            Count = 0,
            Documents = []
        };
    }
}