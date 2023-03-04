// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

internal sealed class RequestFirebaseSmsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestFirebaseSms, IBool>,
    Auth.IRequestFirebaseSmsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestRequestFirebaseSms obj)
    {
        throw new NotImplementedException();
    }
}
