// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// Returns extended user info by ID.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 500 USERNAME_UNSYNCHRONIZED &nbsp;
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/users.getFullUser" />
///</summary>
internal sealed class GetFullUserHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetFullUser, MyTelegram.Schema.Users.IUserFull>,
    Users.IGetFullUserHandler
{
    protected override Task<MyTelegram.Schema.Users.IUserFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetFullUser obj)
    {
        throw new NotImplementedException();
    }
}
