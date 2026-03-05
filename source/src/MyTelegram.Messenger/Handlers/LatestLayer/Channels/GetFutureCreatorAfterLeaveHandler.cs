
namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetFutureCreatorAfterLeaveHandler(IChannelAppService channelAppService, IQueryProcessor queryProcessor, IUserConverterService userConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetFutureCreatorAfterLeave, MyTelegram.Schema.IUser>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestGetFutureCreatorAfterLeave obj)
    {
        var peer = obj.Channel.ToChannelPeer();
        var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
        if (channelReadModel.CreatorId != input.UserId)
        {
            RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        }

        var newAdmin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId != input.UserId);
        long? newAdminUserId;
        if (newAdmin == null)
        {
            newAdminUserId =
              await queryProcessor.ProcessAsync(
                  new GetFutureCreatorAfterLeaveQuery(peer.PeerId, channelReadModel.CreatorId));
        }
        else
        {
            newAdminUserId = newAdmin.UserId;
        }

        return await userConverterService.GetUserAsync(input, newAdminUserId ?? 0, false, false, input.Layer);
    }
}
