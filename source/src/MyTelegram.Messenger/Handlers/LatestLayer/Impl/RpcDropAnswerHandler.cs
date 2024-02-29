// ReSharper disable All

namespace MyTelegram.Handlers.Impl;

internal sealed class RpcDropAnswerHandler : RpcResultObjectHandler<RequestRpcDropAnswer, IRpcDropAnswer>,
    IRpcDropAnswerHandler
{
    protected override Task<IRpcDropAnswer> HandleCoreAsync(IRequestInput input,
        RequestRpcDropAnswer obj)
    {
        //Logger.LogDebug($"drop answer:ReqMsgId={obj.ReqMsgId}");

        return Task.FromResult<IRpcDropAnswer>(new TRpcAnswerDropped
        {
            MsgId = obj.ReqMsgId
        });
    }
}
