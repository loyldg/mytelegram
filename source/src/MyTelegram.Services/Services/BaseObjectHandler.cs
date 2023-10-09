using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public abstract class BaseObjectHandler<TInput, TOutput> : IObjectHandler
    where TInput : IRequest<TOutput>
    where TOutput : IObject
{
    protected int CurrentDate => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    protected int DefaultPageSize => 50;

    public virtual async Task<IObject> HandleAsync(IRequestInput requestInput,
        IObject obj)
    {
        return await HandleCoreAsync(requestInput, (TInput)obj);
    }

    protected abstract Task<TOutput> HandleCoreAsync(IRequestInput request,
        TInput obj);
}