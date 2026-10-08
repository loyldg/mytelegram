using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Services;

public interface IRpcResultCacheAppService
{
    bool TryAdd(long userId,
        long reqMsgId,
        IObject rpcResult);

    bool TryGetRpcResult(long userId,
        long reqMsgId,
        [NotNullWhen(true)] out IObject? rpcResult);
}