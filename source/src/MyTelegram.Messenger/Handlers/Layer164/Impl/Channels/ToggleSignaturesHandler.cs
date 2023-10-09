// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Enable/disable message signatures in channels
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/channels.toggleSignatures" />
///</summary>
internal sealed class ToggleSignaturesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleSignatures, MyTelegram.Schema.IUpdates>,
    Channels.IToggleSignaturesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleSignatures obj)
    {
        throw new NotImplementedException();
    }
}
