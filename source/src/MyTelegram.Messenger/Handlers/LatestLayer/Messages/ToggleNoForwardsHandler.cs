namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Enable or disable <a href="https://telegram.org/blog/protected-content-delete-by-date-and-more">content protection</a> on a channel or chat
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleNoForwards"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleNoForwardsHandler(ICommandBus commandBus) : RpcResultObjectHandler<RequestToggleNoForwards, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestToggleNoForwards obj)
    {
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
                {
                    var command = new ToggleChannelNoForwardsCommand(ChannelId.Create(inputPeerChannel.ChannelId), input.ToRequestInfo(), obj.Enabled);
                    await commandBus.PublishAsync(command);
                }

                return null!;
        }

        throw new NotImplementedException();
    }
}