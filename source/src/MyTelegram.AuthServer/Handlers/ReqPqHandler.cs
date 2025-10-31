namespace MyTelegram.AuthServer.Handlers;

public class ReqPqHandler(
    IStep1Helper step1ServerHelper,
    ILogger<ReqPqHandler> logger,
    ICacheManager<AuthCacheItem> cacheManager
) : BaseObjectHandler<RequestReqPq, IResPQ>, IReqPqHandler
{
    protected override async Task<IResPQ> HandleCoreAsync(IRequestInput input, RequestReqPq obj)
    {
        var dto = step1ServerHelper.GetResponse(obj.Nonce);
        var authCacheItem = new AuthCacheItem(obj.Nonce, dto.ServerNonce, dto.P, dto.Q, false);

        var key = AuthCacheItem.GetCacheKey(dto.ServerNonce);

        await cacheManager.SetAsync(
            key,
            authCacheItem,
            MyTelegramConsts.AuthKeyExpireSeconds
        );
        logger.HandshakeStep1(input.ConnectionId, input.ReqMsgId);

        return dto.ResPq;
    }
}