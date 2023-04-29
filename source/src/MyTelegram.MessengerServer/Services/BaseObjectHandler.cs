namespace MyTelegram.MessengerServer.Services;

public abstract class BaseObjectHandler<TInput, TOutput> : IObjectHandler
    where TInput : IRequest<TOutput>
    where TOutput : IObject
{
    protected int CurrentDate => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    protected int DefaultPageSize => 50;

    public virtual async Task<IObject> HandleAsync(IRequestInput request,
        IObject obj)
    {
        return await HandleCoreAsync(request, (TInput)obj);
    }

    protected abstract Task<TOutput> HandleCoreAsync(IRequestInput request,
        TInput obj);
}
