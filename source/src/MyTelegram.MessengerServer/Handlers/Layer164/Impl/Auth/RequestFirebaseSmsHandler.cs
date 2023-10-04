// ReSharper disable All

using MyTelegram.Schema.Auth;

namespace MyTelegram.Handlers.Auth;

internal sealed class RequestFirebaseSmsHandler : RpcResultObjectHandler<RequestRequestFirebaseSms, IBool>,
    Auth.IRequestFirebaseSmsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestRequestFirebaseSms obj)
    {
        throw new NotImplementedException();
    }
}