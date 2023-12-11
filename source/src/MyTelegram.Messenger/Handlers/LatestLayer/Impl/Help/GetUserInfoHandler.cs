// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Can only be used by TSF members to obtain internal information.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 USER_INVALID Invalid user provided.
/// See <a href="https://corefork.telegram.org/method/help.getUserInfo" />
///</summary>
internal sealed class GetUserInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetUserInfo, MyTelegram.Schema.Help.IUserInfo>,
    Help.IGetUserInfoHandler
{
    protected override Task<MyTelegram.Schema.Help.IUserInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetUserInfo obj)
    {
        throw new NotImplementedException();
    }
}
