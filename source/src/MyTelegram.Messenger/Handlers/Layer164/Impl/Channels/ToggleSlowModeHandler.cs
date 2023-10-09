// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Toggle supergroup slow mode: if enabled, users will only be able to send one message every <code>seconds</code> seconds
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 SECONDS_INVALID Invalid duration provided.
/// See <a href="https://corefork.telegram.org/method/channels.toggleSlowMode" />
///</summary>
internal sealed class ToggleSlowModeHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleSlowMode, MyTelegram.Schema.IUpdates>,
    Channels.IToggleSlowModeHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleSlowMode obj)
    {
        throw new NotImplementedException();
    }
}
