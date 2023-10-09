// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns chat basic info on their IDs.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getChats" />
///</summary>
internal sealed class GetChatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetChats, MyTelegram.Schema.Messages.IChats>,
    Messages.IGetChatsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetChats obj)
    {
        throw new NotImplementedException();
    }
}
