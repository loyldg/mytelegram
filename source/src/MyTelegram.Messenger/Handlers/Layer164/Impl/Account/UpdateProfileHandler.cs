// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Updates user profile.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ABOUT_TOO_LONG About string too long.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// See <a href="https://corefork.telegram.org/method/account.updateProfile" />
///</summary>
internal sealed class UpdateProfileHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateProfile, MyTelegram.Schema.IUser>,
    Account.IUpdateProfileHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateProfile obj)
    {
        throw new NotImplementedException();
    }
}
