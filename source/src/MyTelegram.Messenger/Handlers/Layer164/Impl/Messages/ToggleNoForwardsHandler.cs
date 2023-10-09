// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Enable or disable <a href="https://telegram.org/blog/protected-content-delete-by-date-and-more">content protection</a> on a channel or chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.toggleNoForwards" />
///</summary>
internal sealed class ToggleNoForwardsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleNoForwards, MyTelegram.Schema.IUpdates>,
    Messages.IToggleNoForwardsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleNoForwards obj)
    {
        throw new NotImplementedException();
    }
}
