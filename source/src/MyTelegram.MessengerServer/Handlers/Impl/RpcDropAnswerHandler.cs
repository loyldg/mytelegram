namespace MyTelegram.MessengerServer.Handlers.Impl;

public class RpcDropAnswerHandler : BaseObjectHandler<RequestRpcDropAnswer, IRpcDropAnswer>,
    IRpcDropAnswerHandler, IProcessedHandler
{
    protected override Task<IRpcDropAnswer> HandleCoreAsync(IRequestInput input,
        RequestRpcDropAnswer obj)
    {
        //Logger.LogDebug($"drop answer:ReqMsgId={obj.ReqMsgId}");

        return Task.FromResult<IRpcDropAnswer>(new TRpcAnswerDropped());
    }
}