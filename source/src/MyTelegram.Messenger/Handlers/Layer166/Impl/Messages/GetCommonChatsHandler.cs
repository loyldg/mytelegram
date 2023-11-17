// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get chats in common with a user
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getCommonChats" />
///</summary>
internal sealed class GetCommonChatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetCommonChats, MyTelegram.Schema.Messages.IChats>,
    Messages.IGetCommonChatsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetCommonChats obj)
    {
        throw new NotImplementedException();
    }
}
