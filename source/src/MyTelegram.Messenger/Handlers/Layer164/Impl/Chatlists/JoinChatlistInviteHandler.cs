// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Import a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>, joining some or all the chats in the folder.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INVITE_SLUG_EMPTY The specified invite slug is empty.
/// See <a href="https://corefork.telegram.org/method/chatlists.joinChatlistInvite" />
///</summary>
internal sealed class JoinChatlistInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestJoinChatlistInvite, MyTelegram.Schema.IUpdates>,
    Chatlists.IJoinChatlistInviteHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestJoinChatlistInvite obj)
    {
        throw new NotImplementedException();
    }
}
