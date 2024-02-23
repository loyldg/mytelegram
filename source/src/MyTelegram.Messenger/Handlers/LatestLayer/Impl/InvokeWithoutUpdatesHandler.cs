// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invoke a request without subscribing the used connection for <a href="https://corefork.telegram.org/api/updates">updates</a> (this is enabled by default for <a href="https://corefork.telegram.org/api/files">file queries</a>).
/// See <a href="https://corefork.telegram.org/method/invokeWithoutUpdates" />
///</summary>
internal sealed class InvokeWithoutUpdatesHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeWithoutUpdates, IObject>,
    IInvokeWithoutUpdatesHandler
{
    private readonly IHandlerHelper _handlerHelper;

    public InvokeWithoutUpdatesHandler(IHandlerHelper handlerHelper)
    {
        _handlerHelper = handlerHelper;
    }

    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithoutUpdates obj)
    {
        if (_handlerHelper.TryGetHandler(obj.Query.ConstructorId, out var handler))
        {
            return handler.HandleAsync(input, obj.Query)!;
        }

        throw new NotImplementedException();
    }
}
