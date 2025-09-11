namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get info about <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getChannels" />
///</summary>
internal sealed class GetChannelsHandler(
    IChatConverterService chatConverterService,
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<RequestGetChannels, IChats>
{
    protected override async Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetChannels obj)
    {
        var channelIds = new List<long>();
        foreach (var inputChannel in obj.Id)
        {
            if (inputChannel is TInputChannel tInputChannel)
            {
                channelIds.Add(tInputChannel.ChannelId);
                await accessHashHelper.CheckAccessHashAsync(input, tInputChannel.ChannelId, tInputChannel.AccessHash, AccessHashType.Channel);
            }
        }

        if (channelIds.Count > 0)
        {
            var channelMemberReadModels =
                await queryProcessor.ProcessAsync(
                    new GetChannelMemberListByChannelIdListQuery(input.UserId, channelIds));
            var channels =
                await chatConverterService.GetChannelListAsync(input, channelIds, channelMemberReadModels, layer: input.Layer);
            return new TChats
            {
                Chats = [.. channels]
            };
        }

        RpcErrors.RpcErrors400.ChannelInvalid.ThrowRpcError();

        throw new NotImplementedException();
    }
}
