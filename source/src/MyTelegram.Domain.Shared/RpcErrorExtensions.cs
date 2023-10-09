namespace MyTelegram;

public static class RpcErrorExtensions
{
    public static void ThrowRpcError(this RpcError rpcError)
    {
        throw new RpcException(rpcError);
    }

    public static void ThrowRpcError(this RpcError rpcError, int xToReplace)
    {
        throw new RpcException(rpcError with { Message = string.Format(rpcError.Message, xToReplace) });
    }
}