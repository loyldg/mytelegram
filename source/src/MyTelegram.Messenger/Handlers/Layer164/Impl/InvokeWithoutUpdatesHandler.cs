// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invoke a request without subscribing the used connection for <a href="https://corefork.telegram.org/api/updates">updates</a> (this is enabled by default for <a href="https://corefork.telegram.org/api/files">file queries</a>).
/// See <a href="https://corefork.telegram.org/method/invokeWithoutUpdates" />
///</summary>
internal sealed class InvokeWithoutUpdatesHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithoutUpdates, IObject>,
    IInvokeWithoutUpdatesHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithoutUpdates obj)
    {
        throw new NotImplementedException();
    }
}
