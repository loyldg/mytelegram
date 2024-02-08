namespace MyTelegram.Messenger.Services.Interfaces;

public interface IChannelAdminRightsChecker
{
    Task CheckAdminRightAsync(long channelId, long userId, Func<IChatAdminReadModel, bool> checkAdminRightsFunc, RpcError rpcError);
    Task<bool> HasChatAdminRightAsync(long channelId, long userId, Func<IChatAdminReadModel, bool> checkAdminRightsFunc);
}