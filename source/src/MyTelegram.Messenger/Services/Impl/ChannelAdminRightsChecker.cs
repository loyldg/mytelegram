namespace MyTelegram.Messenger.Services.Impl;

public class ChannelAdminRightsChecker : IChannelAdminRightsChecker
{
    private readonly IQueryProcessor _queryProcessor;

    public ChannelAdminRightsChecker(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    public async Task CheckAdminRightAsync(long channelId, long userId, Func<IChatAdminReadModel, bool> checkAdminRightsFunc, RpcError rpcError)
    {
        if (!await HasChatAdminRightAsync(channelId, userId, checkAdminRightsFunc))
        {
            rpcError.ThrowRpcError();
        }
    }

    public async Task<bool> HasChatAdminRightAsync(long channelId, long userId, Func<IChatAdminReadModel, bool> checkAdminRightsFunc)
    {
        var chatAdmin = await _queryProcessor.ProcessAsync(new GetChatAdminQuery(channelId, userId));
        if(chatAdmin==null)
        {
            return false;
        }

        return checkAdminRightsFunc(chatAdmin);
    }
}

//public class ChatInviteLinkHelper : IChatInviteLinkHelper
//{
//    private readonly IRandomHelper _randomHelper;

//    public ChatInviteLinkHelper(IRandomHelper randomHelper)
//    {
//        _randomHelper = randomHelper;
//    }

//    public string GenerateLink()
//    {
//        var bytes = new byte[12];
//        _randomHelper.NextBytes(bytes);
//        var inviteHash = $"{Convert.ToBase64String(bytes)
//            .Replace($"+", "-")
//            .Replace("/", ".")
//            .Replace("=", string.Empty)}";

//        return inviteHash;
//    }
//}