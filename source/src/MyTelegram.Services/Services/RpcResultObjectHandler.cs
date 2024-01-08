using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public abstract class RpcResultObjectHandler<TInput, TOutput> : BaseObjectHandler<TInput, TOutput>
    where TInput : IRequest<TOutput>
    where TOutput : IObject
{
    private readonly IGZipHelper _gzipHelper = new GZipHelper();
    public override async Task<IObject> HandleAsync(IRequestInput request,
        IObject obj)
    {
        var r = await base.HandleAsync(request, obj);
        if (r == null!)
        {
            return null!;
        }

        var rpcResult = new TRpcResult { ReqMsgId = request.ReqMsgId, Result = r };
        var length = r.GetLength();
        if (length > 500)
        {
            var gzipPacked = new TGzipPacked
            {
                PackedData = _gzipHelper.Compress(r.ToBytes())
            };
            rpcResult.Result = gzipPacked;
        }
        return rpcResult;
    }
}