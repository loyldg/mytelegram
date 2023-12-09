// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Set whether all users should <a href="https://corefork.telegram.org/api/invites#join-requests">request admin approval to join the group »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 CHAT_PUBLIC_REQUIRED You can only enable join requests in public groups.
/// See <a href="https://corefork.telegram.org/method/channels.toggleJoinRequest" />
///</summary>
internal sealed class ToggleJoinRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleJoinRequest, MyTelegram.Schema.IUpdates>,
    Channels.IToggleJoinRequestHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}
