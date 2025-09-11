namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Hide or display the participants list in a <a href="https://corefork.telegram.org/api/channel">supergroup</a>.The supergroup must have at least <code>hidden_members_group_size_min</code> participants in order to use this method, as specified by the <a href="https://corefork.telegram.org/api/config#client-configuration">client configuration parameters »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PARTICIPANTS_TOO_FEW Not enough participants.
/// See <a href="https://corefork.telegram.org/method/channels.toggleParticipantsHidden" />
///</summary>
internal sealed class ToggleParticipantsHiddenHandler(ICommandBus commandBus,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<RequestToggleParticipantsHidden, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleParticipantsHidden obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            await channelAdminRightsChecker.CheckAdminRightAsync(inputChannel.ChannelId, input.UserId,
                p => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);
            var command = new ToggleParticipantsHiddenCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                obj.Enabled);
            await commandBus.PublishAsync(command);

            return null!;
        }

        throw new NotImplementedException();
    }
}
