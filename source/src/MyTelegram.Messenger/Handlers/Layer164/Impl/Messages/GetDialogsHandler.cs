// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns the current user dialog list.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 OFFSET_PEER_ID_INVALID The provided offset peer is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDialogs" />
///</summary>
internal sealed class GetDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogs, MyTelegram.Schema.Messages.IDialogs>,
    Messages.IGetDialogsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDialogs obj)
    {
        throw new NotImplementedException();
    }
}
