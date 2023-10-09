namespace MyTelegram;

public class ThrowHelper
{
    public static void ThrowRpcError(RpcError rpcError)
    {
        throw new RpcException(rpcError);
    }
}   