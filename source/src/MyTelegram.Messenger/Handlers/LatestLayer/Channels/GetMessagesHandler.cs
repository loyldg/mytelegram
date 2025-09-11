namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(
    IMessageAppService messageAppService,
    IAccessHashHelper accessHashHelper,
    IChannelAppService channelAppService,
    IGetHistoryConverterService getHistoryConverterService)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetMessages, MyTelegram.Schema.Messages.IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetMessages obj)
    {
        long channelId = 0;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
            if (channelReadModel == null)
            {
                RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
            }

            // Only check accessHash for private channel
            if (string.IsNullOrEmpty(channelReadModel!.UserName))
            {
                await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            }
        }
        else
        {
            RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        }
        var idList = new List<int>();
        foreach (var inputMessage in obj.Id)
        {
            if (inputMessage is TInputMessageID inputMessageId)
            {
                idList.Add(inputMessageId.Id);
            }
        }

        var getMessageOutput = await messageAppService
            .GetMessagesAsync(
                new GetMessagesInput(input.UserId,
                    channelId,
                    idList,
                    new Peer(PeerType.Channel, channelId))
                { Limit = 50 });

        return getHistoryConverterService.ToMessages(input, getMessageOutput, input.Layer);
    }
}