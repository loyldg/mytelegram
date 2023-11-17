// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Change the chat theme of a certain chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOJI_INVALID The specified theme emoji is valid.
/// 400 EMOJI_NOT_MODIFIED The theme wasn't changed.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.setChatTheme" />
///</summary>
internal sealed class SetChatThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetChatTheme, MyTelegram.Schema.IUpdates>,
    Messages.ISetChatThemeHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetChatTheme obj)
    {
        throw new NotImplementedException();
    }
}
