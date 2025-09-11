namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get a list of <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a> we left, requires a <a href="https://corefork.telegram.org/api/takeout">takeout session, see here » for more info</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 TAKEOUT_REQUIRED A <a href="https://corefork.telegram.org/api/takeout">takeout</a> session needs to be initialized first, <a href="https://corefork.telegram.org/api/takeout">see here » for more info</a>.
/// See <a href="https://corefork.telegram.org/method/channels.getLeftChannels" />
///</summary>
internal sealed class GetLeftChannelsHandler(IQueryProcessor queryProcessor, IChatConverterService chatConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetLeftChannels, MyTelegram.Schema.Messages.IChats>
{
    protected override async Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetLeftChannels obj)
    {
        var pageSize = 100;
        var leftChannelIds = await queryProcessor.ProcessAsync(new GetLeftChannelIdsQuery(input.UserId, OffsetChannelId: obj.Offset, Limit: pageSize));
        if (leftChannelIds?.Count > 0)
        {
            var channels =
                await chatConverterService.GetChannelListAsync(input, [.. leftChannelIds], layer: input.Layer);
            if (leftChannelIds.Count == pageSize)
            {
                var totalCount = await queryProcessor.ProcessAsync(new GetLeftChannelCountQuery(input.UserId));
                return new TChatsSlice
                {
                    Chats = [.. channels],
                    Count = totalCount
                };
            }

            return new TChats
            {
                Chats = [.. channels]
            };
        }

        return new TChats
        {
            Chats = []
        };
    }
}
