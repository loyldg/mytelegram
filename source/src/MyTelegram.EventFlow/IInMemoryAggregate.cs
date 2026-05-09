using EventFlow.ReadStores;

namespace MyTelegram.EventFlow;

public interface IInMemoryAggregate
{
}

public class QueryFilterContext
{
    public bool IgnoreSoftDelete { get; set; }
}

public interface IUserContext
{
    long? UserId { get; }
}

public interface IUserContextScope
{
    IDisposable Use(long? userId);
}

public interface IQueryFilterScope
{
    IDisposable DisableSoftDelete();
}

public interface IUserContextAccessor
{
    long? UserId { get; set; }
}

public interface IReadModelWriteInterceptor
{
    void OnUpdate<TReadModel>(IReadModelContext readModelContext, TReadModel readModel, IReadOnlyCollection<IDomainEvent> domainEvents) where TReadModel : IReadModel;
}
public class UserContextAccessor : IUserContext, IUserContextAccessor
{
    private static readonly AsyncLocal<long?> _userId = new();

    public long? UserId
    {
        get => _userId.Value;
        set => _userId.Value = value;
    }
}
public class UserContextScope(IUserContextAccessor accessor) : IUserContextScope
{
    public IDisposable Use(long? userId)
    {
        var previous = accessor.UserId;
        accessor.UserId = userId;

        return new DisposeAction(() =>
        {
            accessor.UserId = previous;
        });
    }
}

public class QueryFilterScope : IQueryFilterScope
{
    private static readonly AsyncLocal<QueryFilterContext?> _current = new();

    public static QueryFilterContext Current => _current.Value ??= new QueryFilterContext();

    public IDisposable DisableSoftDelete()
    {
        var previous = Current.IgnoreSoftDelete;
        Current.IgnoreSoftDelete = true;

        return new DisposeAction(() =>
        {
            Current.IgnoreSoftDelete = previous;
        });
    }

}
internal class DisposeAction(Action action) : IDisposable
{
    public void Dispose() => action();
}