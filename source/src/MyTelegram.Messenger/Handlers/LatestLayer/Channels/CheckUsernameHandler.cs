namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Check if a username is free and can be assigned to a channel/supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
/// See <a href="https://corefork.telegram.org/method/channels.checkUsername" />
///</summary>
internal sealed class CheckUsernameHandler(
    IQueryProcessor queryProcessor,
    IUsernameHelper usernameHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCheckUsername, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestCheckUsername obj)
    {
        if (!string.IsNullOrEmpty(obj.Username))
        {
            if (!usernameHelper.IsValidUsername(obj.Username))
            {
                RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
            }
        }

        switch (obj.Channel)
        {
            case TInputChannel inputChannel1:
                await accessHashHelper.CheckAccessHashAsync(input, inputChannel1.ChannelId, inputChannel1.AccessHash, AccessHashType.Channel);
                break;
            case TInputChannelEmpty _:
                break;
        }

        var userNameReadModel = await queryProcessor.ProcessAsync(new GetUserNameByNameQuery(obj.Username.ToLower()));
        if (userNameReadModel == null)
        {
            return new TBoolTrue();
        }

        return new TBoolFalse();
    }
}
