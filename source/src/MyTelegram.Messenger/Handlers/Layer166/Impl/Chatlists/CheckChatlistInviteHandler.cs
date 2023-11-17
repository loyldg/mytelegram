// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INVITE_SLUG_EMPTY The specified invite slug is empty.
/// See <a href="https://corefork.telegram.org/method/chatlists.checkChatlistInvite" />
///</summary>
internal sealed class CheckChatlistInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestCheckChatlistInvite, MyTelegram.Schema.Chatlists.IChatlistInvite>,
    Chatlists.ICheckChatlistInviteHandler
{
    protected override Task<MyTelegram.Schema.Chatlists.IChatlistInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestCheckChatlistInvite obj)
    {
        throw new NotImplementedException();
    }
}
