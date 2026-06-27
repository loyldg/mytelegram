namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get chats in common with a user
/// Possible errors
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getCommonChats"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetCommonChatsHandler(IQueryProcessor queryProcessor, IChatConverterService chatConverterService, IPhotoAppService photoAppService, IChannelAppService channelAppService, IPeerHelper peerHelper) : RpcResultObjectHandler<Schema.Messages.RequestGetCommonChats, Schema.Messages.IChats>
{
    protected override async Task<Schema.Messages.IChats> HandleCoreAsync(IRequestInput input, Schema.Messages.RequestGetCommonChats obj)
    {
        var peer = peerHelper.GetPeer(obj.UserId);
        var limit = obj.Limit;
        if (limit < 0)
        {
            limit = 40;
        }

        var commonChannelIds = await queryProcessor.ProcessAsync(new GetCommonChatChannelIdsQuery(input.UserId, peer.PeerId, obj.MaxId, limit));
        if (commonChannelIds.Count > 0)
        {
            var channelReadModels = await channelAppService.GetListAsync(commonChannelIds.ToList());
            var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);
            var chats = chatConverterService.ToChannelList(input, channelReadModels, photoReadModels, [], commonChannelIds, layer: input.Layer);
            return new TChats
            {
                Chats = [.. chats]
            };
        }

        return new TChats
        {
            Chats = []
        };
    }
}