// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Set whether all users <a href="https://corefork.telegram.org/api/discussion#requiring-users-to-join-the-group">should join a discussion group in order to comment on a post »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.toggleJoinToSend" />
///</summary>
internal sealed class ToggleJoinToSendHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleJoinToSend, MyTelegram.Schema.IUpdates>,
    Channels.IToggleJoinToSendHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleJoinToSend obj)
    {
        throw new NotImplementedException();
    }
}
