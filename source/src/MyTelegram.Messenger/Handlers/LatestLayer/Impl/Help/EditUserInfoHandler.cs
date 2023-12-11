// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Internal use
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 403 USER_INVALID Invalid user provided.
/// See <a href="https://corefork.telegram.org/method/help.editUserInfo" />
///</summary>
internal sealed class EditUserInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestEditUserInfo, MyTelegram.Schema.Help.IUserInfo>,
    Help.IEditUserInfoHandler
{
    protected override Task<MyTelegram.Schema.Help.IUserInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestEditUserInfo obj)
    {
        throw new NotImplementedException();
    }
}
