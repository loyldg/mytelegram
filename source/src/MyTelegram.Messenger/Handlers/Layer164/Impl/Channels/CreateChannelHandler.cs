// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Create a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_LOCATED_TOO_MUCH The user has reached the limit of public geogroups.
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 500 CHANNEL_ID_GENERATE_FAILED &nbsp;
/// 400 CHAT_ABOUT_TOO_LONG Chat about too long.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// 406 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/channels.createChannel" />
///</summary>
internal sealed class CreateChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCreateChannel, MyTelegram.Schema.IUpdates>,
    Channels.ICreateChannelHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestCreateChannel obj)
    {
        throw new NotImplementedException();
    }
}
