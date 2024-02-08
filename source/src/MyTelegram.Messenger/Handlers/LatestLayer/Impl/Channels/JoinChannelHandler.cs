// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Join a channel/supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_INVALID Invalid chat.
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// 400 INVITE_REQUEST_SENT You have successfully requested to join this chat or channel.
/// 500 MEMBER_CHAT_ADD_FAILED &nbsp;
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// See <a href="https://corefork.telegram.org/method/channels.joinChannel" />
///</summary>
internal sealed class JoinChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestJoinChannel, MyTelegram.Schema.IUpdates>,
    Channels.IJoinChannelHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;
    public JoinChannelHandler(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestJoinChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);
            var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);

            var userIdList = new[] { input.UserId };
            var command = new StartInviteToChannelCommand(
                ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                inputChannel.ChannelId,
                input.UserId,
                channelReadModel!.TopMessageId,
                userIdList,
                null,
                new List<long>(),
                CurrentDate,
                _randomHelper.NextLong(),
                new TMessageActionChatAddUser { Users = new TVector<long>(userIdList) }.ToBytes().ToHexString(),
                ChatJoinType.ByRequest
                );

            await _commandBus.PublishAsync(command, default);
            return null!;
        }

        throw new NotImplementedException();
    }
}
