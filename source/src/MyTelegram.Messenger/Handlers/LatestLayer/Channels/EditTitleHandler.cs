namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Edit the name of a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// See <a href="https://corefork.telegram.org/method/channels.editTitle" />
///</summary>
internal sealed class EditTitleHandler(
    ICommandBus commandBus,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditTitle, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditTitle obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            var command = new EditChannelTitleCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                obj.Title,
                new TMessageActionChatEditTitle { Title = obj.Title },
                randomHelper.NextInt64()
            );
            await commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
