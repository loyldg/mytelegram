// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Gets current notification settings for a given user/group, from all users/all groups.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getNotifySettings" />
///</summary>
internal sealed class GetNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetNotifySettings, MyTelegram.Schema.IPeerNotifySettings>,
    Account.IGetNotifySettingsHandler
{
    protected override Task<MyTelegram.Schema.IPeerNotifySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetNotifySettings obj)
    {
        throw new NotImplementedException();
    }
}
