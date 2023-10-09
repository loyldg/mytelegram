// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getFullChat" />
///</summary>
internal sealed class GetFullChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFullChat, MyTelegram.Schema.Messages.IChatFull>,
    Messages.IGetFullChatHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFullChat obj)
    {
        throw new NotImplementedException();
    }
}
