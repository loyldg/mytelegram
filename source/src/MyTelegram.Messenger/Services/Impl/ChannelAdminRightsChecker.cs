namespace MyTelegram.Messenger.Services.Impl;

public class ChannelAdminRightsChecker(IQueryProcessor queryProcessor, IChannelAppService channelAppService) : IChannelAdminRightsChecker, ITransientDependency
{
    public async Task<bool> HasChatAdminRightAsync(long channelId, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc)
    {
        var channelReadModel = await channelAppService.GetAsync(channelId);
        if (channelReadModel.CreatorId == userId)
        {
            return true;
        }

        var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == userId);
        if (admin == null)
        {
            return false;
        }

        return checkAdminRightsFunc(admin.AdminRights);
    }

    public async Task CheckAdminRightAsync(IInputChannel channel, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc, RpcError? rpcError = null)
    {
        switch (channel)
        {
            case TInputChannel inputChannel:
                await CheckAdminRightAsync(inputChannel.ChannelId, userId, checkAdminRightsFunc, rpcError);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel));
        }
    }

    public async Task CheckAdminRightAsync(long channelId, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc, RpcError? rpcError = null)
    {
        if (!await HasChatAdminRightAsync(channelId, userId, checkAdminRightsFunc))
        {
            (rpcError ?? RpcErrors.RpcErrors400.ChatAdminRequired).ThrowRpcError();
        }
    }

    public async Task ThrowIfNotChannelOwnerAsync(IInputChannel channel, long userId)
    {
        switch (channel)
        {
            case TInputChannel inputChannel:
                var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
                if (channelReadModel.CreatorId != userId)
                {
                    RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel));
        }
    }
}