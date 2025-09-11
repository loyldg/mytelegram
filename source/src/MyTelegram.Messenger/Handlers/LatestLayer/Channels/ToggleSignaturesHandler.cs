using MyTelegram.Messenger.Services.Impl;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

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
internal sealed class ToggleSignaturesHandler(ICommandBus commandBus,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleSignatures, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleSignatures obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel);
            await channelAdminRightsChecker.CheckAdminRightAsync(inputChannel.ChannelId, input.UserId,
                p => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);
            await commandBus.PublishAsync(
                new ToggleSignatureCommand(ChannelId.Create(inputChannel.ChannelId), input.ToRequestInfo(),
                    obj.SignaturesEnabled, obj.ProfilesEnabled));

            return null!;
        }

        throw new NotImplementedException();
    }
}
