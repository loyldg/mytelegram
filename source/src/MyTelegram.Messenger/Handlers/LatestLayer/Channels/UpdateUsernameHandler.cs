using EventFlow.Queries;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Change or remove the username of a supergroup/channel
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
/// See <a href="https://corefork.telegram.org/method/channels.updateUsername" />
///</summary>
internal sealed class UpdateUsernameHandler(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    IUsernameHelper usernameHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateUsername, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateUsername obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            if (!string.IsNullOrEmpty(obj.Username))
            {
                if (!usernameHelper.IsValidUsername(obj.Username))
                {
                    RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
                }
            }

            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
            var oldUserName = channelReadModel.UserName;
            if (string.Equals(obj.Username, oldUserName, StringComparison.OrdinalIgnoreCase))
            {
                RpcErrors.RpcErrors400.UsernameNotModified.ThrowRpcError();
            }

            var command = new SetUserNameCommand(UserNameId.Create(obj.Username.ToLower()),
                input.ToRequestInfo(),
                inputChannel.ChannelId.ToChannelPeer(),
                obj.Username,
                oldUserName
                );
            await commandBus.PublishAsync(command);

            return new TBoolTrue();
        }

        throw new NotImplementedException();
    }
}
