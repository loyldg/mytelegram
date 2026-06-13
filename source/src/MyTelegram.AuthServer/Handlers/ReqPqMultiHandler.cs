using System.Diagnostics;

namespace MyTelegram.AuthServer.Handlers;

public class ReqPqMultiHandler(
    IStep1Helper step1ServerHelper,
    ILogger<ReqPqMultiHandler> logger,
    ICacheManager<AuthCacheItem> cacheManager) : BaseObjectHandler<RequestReqPqMulti, IResPQ>, IReqPqMultiHandler
{
    protected override async Task<IResPQ> HandleCoreAsync(
        IRequestInput input,
        RequestReqPqMulti obj
    )
    {
        var sw = Stopwatch.StartNew();
        var nonce = obj.Nonce;
        var dto = step1ServerHelper.GetResponse(nonce);

        var authCacheItem = new AuthCacheItem(nonce, dto.ServerNonce, dto.P, dto.Q, false);
        var key = AuthCacheItem.GetCacheKey(dto.ServerNonce);
        await cacheManager.SetAsync(
            key,
            authCacheItem,
            MyTelegramConsts.AuthKeyExpireSeconds
        );
        sw.Stop();
        logger.HandshakeReqMultiStep1(input.ConnectionId, input.ReqMsgId, input.AuthKeyId, sw.Elapsed.TotalMilliseconds);

        return dto.ResPq;
    }
}