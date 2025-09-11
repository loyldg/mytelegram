namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Check the validity of a chat invite link and get basic info about it
/// <para>Possible errors</para>
/// Code Type Description
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.checkChatInvite" />
///</summary>
internal sealed class CheckChatInviteHandler(
    IQueryProcessor queryProcessor,
    IPhotoAppService photoAppService,
    IChannelAppService channelAppService,
    IChatConverterService chatConverterService,
    ILayeredService<IPhotoConverter> layeredPhotoService)
    : RpcResultObjectHandler<RequestCheckChatInvite, IChatInvite>
{
    protected override async Task<IChatInvite> HandleCoreAsync(IRequestInput input,
        RequestCheckChatInvite obj)
    {
        if (string.IsNullOrEmpty(obj.Hash))
        {
            RpcErrors.RpcErrors400.InviteHashEmpty.ThrowRpcError();
        }

        var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Hash));
        if (chatInviteReadModel == null)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        if (chatInviteReadModel!.ExpireDate > 0)
        {
            if (chatInviteReadModel.ExpireDate.Value < CurrentDate)
            {
                RpcErrors.RpcErrors400.InviteHashExpired.ThrowRpcError();
            }
        }

        var channelReadModel = await channelAppService.GetAsync(chatInviteReadModel.PeerId);
        if (channelReadModel == null!)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }
        var chatPhoto = await photoAppService.GetAsync(channelReadModel!.PhotoId);

        var channelMemberReadModel =
            await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(channelReadModel!.ChannelId,
                input.UserId));

        // Public channel/Super group
        if (!string.IsNullOrEmpty(channelReadModel.UserName) ||
            channelMemberReadModel is { Left: false, Kicked: false })
        {
            if (channelMemberReadModel != null)
            {
                var channel = chatConverterService.ToChannel(input, channelReadModel, chatPhoto,
                    channelMemberReadModel, null, input.Layer);

                return new TChatInviteAlready
                {
                    Chat = channel
                };
            }
        }

        return new TChatInvite
        {
            About = channelReadModel.About,
            Broadcast = channelReadModel.Broadcast,
            Channel = true,
            Public = !string.IsNullOrEmpty(channelReadModel.UserName),
            Megagroup = channelReadModel.MegaGroup,
            ParticipantsCount = channelReadModel.ParticipantsCount ?? 0,
            Photo = layeredPhotoService.GetConverter(input.Layer).ToPhoto(chatPhoto),
            RequestNeeded = chatInviteReadModel.RequestNeeded,
            Title = channelReadModel.Title,
        };
    }
}
