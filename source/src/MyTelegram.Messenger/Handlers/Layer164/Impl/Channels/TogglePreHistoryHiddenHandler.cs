// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Hide/unhide message history for new channel/supergroup users
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_LINK_EXISTS The chat is public, you can't hide the history to new users.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden" />
///</summary>
internal sealed class TogglePreHistoryHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestTogglePreHistoryHidden, MyTelegram.Schema.IUpdates>,
    Channels.ITogglePreHistoryHiddenHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IAccessHashHelper _accessHashHelper;
    public TogglePreHistoryHiddenHandler(ICommandBus commandBus,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestTogglePreHistoryHidden obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var command = new TogglePreHistoryHiddenCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                obj.Enabled,
                input.UserId);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
