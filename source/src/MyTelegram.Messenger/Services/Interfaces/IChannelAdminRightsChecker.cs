namespace MyTelegram.Messenger.Services.Interfaces;

public interface IChannelAdminRightsChecker
{
    Task<bool> HasChatAdminRightAsync(long channelId, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc);

    Task CheckAdminRightAsync(IInputChannel channel, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc, RpcError? rpcError = null);
    Task CheckAdminRightAsync(long channelId, long userId, Func<ChatAdminRights, bool> checkAdminRightsFunc, RpcError? rpcError = null);

    Task ThrowIfNotChannelOwnerAsync(IInputChannel channel, long userId);
}