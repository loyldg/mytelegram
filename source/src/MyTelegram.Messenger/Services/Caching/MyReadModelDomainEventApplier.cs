using EventFlow.ReadStores;

namespace MyTelegram.Messenger.Services.Caching;

public class MyReadModelDomainEventApplier : IReadModelDomainEventApplier, ISingletonDependency
{
    private const string ApplyMethodName = "ApplyAsync";

    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, ApplyMethod>> ApplyMethods = new();

    public async Task<bool> UpdateReadModelAsync<TReadModel>(
        TReadModel readModel,
        IReadOnlyCollection<IDomainEvent> domainEvents,
        IReadModelContext readModelContext,
        CancellationToken cancellationToken)
        where TReadModel : IReadModel
    {
        var readModelType = typeof(TReadModel);
        if (readModelType.IsInterface)
        {
            readModelType = readModel.GetType();
        }

        var appliedAny = false;

        foreach (var domainEvent in domainEvents)
        {
            var applyMethods = ApplyMethods.GetOrAdd(
                readModelType,
                _ => new ConcurrentDictionary<Type, ApplyMethod>());
            var applyMethod = applyMethods.GetOrAdd(
                domainEvent.EventType,
                t =>
                {
                    var identityType = domainEvent.GetIdentity().GetType();
                    var aggregateType = domainEvent.AggregateType;
                    var eventType = typeof(IDomainEvent<,,>).MakeGenericType(aggregateType, identityType, t);

                    // first try: does it implement the synchronous 'Apply' method?

                    var interfaceType = typeof(IAmReadModelFor<,,>).MakeGenericType(aggregateType, identityType, t);
                    var methodParams = new[] { typeof(IReadModelContext), eventType, typeof(CancellationToken) };
                    var methodInfo = GetMethod(readModelType, interfaceType, ApplyMethodName, methodParams);

                    if (methodInfo != null)
                    {
                        var method = ReflectionHelper
                            .CompileMethodInvocation<Func<IReadModel, IReadModelContext, IDomainEvent, CancellationToken, Task>>(methodInfo);
                        return new ApplyMethod(method);
                    }

                    // no matching 'Apply' method found

                    return null!;
                });

            if (applyMethod != null!)
            {
                await applyMethod.Apply(readModel, readModelContext, domainEvent, cancellationToken).ConfigureAwait(false);
                appliedAny = true;
            }
        }

        return appliedAny;
    }

    private static MethodInfo? GetMethod(Type instanceType, Type interfaceType, string name, Type[] parameters)
    {
        var methodInfo = instanceType
            .GetTypeInfo()
            .GetMethod(name, parameters);

        if (methodInfo != null)
        {
            return methodInfo;
        }

        var type = interfaceType.GetTypeInfo();
        return type.IsAssignableFrom(instanceType)
            ? type.GetMethod(name, parameters)
            : null;
    }

    private class ApplyMethod(Func<IReadModel, IReadModelContext, IDomainEvent, CancellationToken, Task> method)
    {
        public Task Apply(IReadModel readModel, IReadModelContext context, IDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            return method(readModel, context, domainEvent, cancellationToken);
        }
    }
}