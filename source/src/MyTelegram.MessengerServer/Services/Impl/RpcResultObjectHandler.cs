namespace MyTelegram.MessengerServer.Services.Impl;

public abstract class RpcResultObjectHandler<TInput, TOutput> : BaseObjectHandler<TInput, TOutput>
    where TInput : IRequest<TOutput>
    where TOutput : IObject
{
    public override async Task<IObject> HandleAsync(IRequestInput request,
        IObject obj)
    {
        var r = await base.HandleAsync(request, obj);
        if (r == null!)
        {
            return null!;
        }

        return new TRpcResult { ReqMsgId = request.ReqMsgId, Result = r };
    }
}