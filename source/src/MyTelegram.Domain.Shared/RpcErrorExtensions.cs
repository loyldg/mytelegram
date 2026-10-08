using System.Diagnostics.CodeAnalysis;

namespace MyTelegram;

public static class RpcErrorExtensions
{
    [DoesNotReturn]
    public static void ThrowRpcError(this RpcError rpcError, long reqMsgId = 0)
    {
        throw new RpcException(rpcError, reqMsgId);
    }

    [DoesNotReturn]
    public static void ThrowRpcError(this RpcError rpcError, int xToReplace, long reqMsgId = 0)
    {
        throw new RpcException(rpcError with { Message = string.Format(rpcError.Message, xToReplace) }, reqMsgId);
    }

    [DoesNotReturn]
    public static void ThrowRpcErrorX(this RpcError rpcError, long xToReplace, long reqMsgId = 0)
    {
        throw new RpcException(rpcError with { Message = string.Format(rpcError.Message, xToReplace) }, reqMsgId);
    }
}