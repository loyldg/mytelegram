using System.Diagnostics;

namespace MyTelegram.AuthServer.Handlers;

public class ReqPqMultiHandler(
    IStep1Helper step1ServerHelper,
    ILogger<ReqPqMultiHandler> logger,
    ICacheHelper<string, byte[]> cacheHelper,
    ICacheManager<AuthCacheItem> cacheManager) : BaseObjectHandler<RequestReqPqMulti, IResPQ>, IReqPqMultiHandler
{
    private static long _count;
    protected override async Task<IResPQ> HandleCoreAsync(
        IRequestInput input,
        RequestReqPqMulti obj
    )
    {
        Interlocked.Increment(ref _count);
        if (cacheHelper.TryRemove(input.ConnectionId, out var oldNonce))
        {
            //logger.LogInformation("[{Count}] Old nonce:{OldNonce}->{NewNonce}", _count, oldNonce.ToHexString(), obj.Nonce.ToHexString());
        }
        cacheHelper.TryAdd(input.ConnectionId, obj.Nonce);

        var sw = Stopwatch.StartNew();
        //await Task.Delay(50);

        if (!cacheHelper.TryGetValue(input.ConnectionId, out var nonce))
        {
            nonce = obj.Nonce;
        }
        //var nonce = obj.Nonce;
        var dto = step1ServerHelper.GetResponse(nonce);

        var authCacheItem = new AuthCacheItem(nonce, dto.ServerNonce, dto.P, dto.Q, false);
        var key = AuthCacheItem.GetCacheKey(dto.ServerNonce);
        await cacheManager.SetAsync(
            key,
            authCacheItem,
            MyTelegramConsts.AuthKeyExpireSeconds
        );
        sw.Stop();
        logger.LogInformation(
            "[Step1] ReqPqMultiHandler, connectionId={ConnectionId}, nonce: {Nonce} {Nonce2} reqMsgId: {ReqMsgId}, authKeyId: {AuthKeyId} {TimeSpan}ms",
            input.ConnectionId,
            obj.Nonce.ToHexString(),
            nonce.ToHexString(),
            input.ReqMsgId,
            input.AuthKeyId,
            sw.Elapsed.TotalMilliseconds
        );

        return dto.ResPq;
    }
}