using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace MyTelegram.Domain.EventFlow;

public class MyAggregateFactory : IAggregateFactory
{
    private static readonly ConcurrentDictionary<Type, AggregateConstruction> AggregateConstructions = new();
    private readonly IMemoryCache _memoryCache;
    private readonly IServiceProvider _serviceProvider;

    public MyAggregateFactory(
        IServiceProvider serviceProvider,
        IMemoryCache memoryCache)
    {
        _serviceProvider = serviceProvider;
        _memoryCache = memoryCache;
    }

    public Task<TAggregate> CreateNewAggregateAsync<TAggregate, TIdentity>(TIdentity id)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        var aggregateConstruction = AggregateConstructions.GetOrAdd(
            typeof(TAggregate),
            _ => CreateAggregateConstruction<TAggregate, TIdentity>());

        return Task.FromResult(
            aggregateConstruction.CreateInstance<TAggregate, TIdentity>(id, _memoryCache, _serviceProvider));
    }

    private static AggregateConstruction CreateAggregateConstruction<TAggregate, TIdentity>()
    {
        var constructorInfos = typeof(TAggregate)
            .GetTypeInfo()
            .GetConstructors()
            .ToList();

        if (constructorInfos.Count != 1)
        {
            throw new ArgumentException(
                $"Aggregate type '{typeof(TAggregate).PrettyPrint()}' doesn't have just one constructor");
        }

        var constructorInfo = constructorInfos.Single();

        var parameterInfos = constructorInfo.GetParameters();
        var identityType = typeof(TIdentity);

        return new AggregateConstruction(
            parameterInfos,
            constructorInfo,
            identityType);
    }

    private class AggregateConstruction
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly Type _identityType;
        private readonly IReadOnlyCollection<ParameterInfo> _parameterInfos;

        public AggregateConstruction(
            IReadOnlyCollection<ParameterInfo> parameterInfos,
            ConstructorInfo constructorInfo,
            Type identityType)
        {
            _parameterInfos = parameterInfos;
            _constructorInfo = constructorInfo;
            _identityType = identityType;
        }

        public TAggregate CreateInstance<TAggregate, TIdentity>(TIdentity identity,
            IMemoryCache memoryCache,
            IServiceProvider serviceProvider) where TIdentity : IIdentity
        {
            var typeOfTAggregate = typeof(TAggregate);
            switch (_parameterInfos.Count)
            {
                case 1: // the TAggregate's constructor looks like `public TAggregate(TIdentity id)...`
                {
                    var arg1Type = _parameterInfos.ElementAt(0).ParameterType;
                    var arg1 = arg1Type == _identityType ? identity : serviceProvider.GetRequiredService(arg1Type);
                    if (arg1Type == _identityType)
                    {
                        var createAggregateFunc = memoryCache.GetOrCreate(typeOfTAggregate,
                            _ => MyReflectionHelper.CompileConstructor<TIdentity, TAggregate>());
                        return createAggregateFunc(identity);
                    }
                    else
                    {
                        var createAggregateFunc = memoryCache.GetOrCreate(typeOfTAggregate,
                            _ => MyReflectionHelper.CompileConstructor<TAggregate>(typeOfTAggregate, arg1Type));
                        return createAggregateFunc(arg1);
                    }
                }
                case 2: // the TAggregate's constructor looks like `public TAggregate(T1 t1,T2 t2)...`
                {
                    var arg1Type = _parameterInfos.ElementAt(0).ParameterType;
                    var arg2Type = _parameterInfos.ElementAt(1).ParameterType;
                    var arg1 = arg1Type == _identityType ? identity : serviceProvider.GetRequiredService(arg1Type);
                    var arg2 = arg2Type == _identityType ? identity : serviceProvider.GetRequiredService(arg2Type);

                    var createAggregateFunc = memoryCache.GetOrCreate(typeOfTAggregate,
                        _ => MyReflectionHelper.CompileConstructor<TAggregate>(typeOfTAggregate, arg1Type, arg2Type));
                    return createAggregateFunc(arg1, arg2);
                }
                case 3: // the TAggregate's constructor looks like `public TAggregate(T1 t1,T2 t2,T3 t3)...`
                {
                    var arg1Type = _parameterInfos.ElementAt(0).ParameterType;
                    var arg2Type = _parameterInfos.ElementAt(1).ParameterType;
                    var arg3Type = _parameterInfos.ElementAt(2).ParameterType;
                    var arg1 = arg1Type == _identityType ? identity : serviceProvider.GetRequiredService(arg1Type);
                    var arg2 = arg2Type == _identityType ? identity : serviceProvider.GetRequiredService(arg2Type);
                    var arg3 = arg3Type == _identityType ? identity : serviceProvider.GetRequiredService(arg3Type);

                    var createAggregateFunc = memoryCache.GetOrCreate(typeOfTAggregate,
                        _ => MyReflectionHelper.CompileConstructor<TAggregate>(typeOfTAggregate,
                            arg1Type,
                            arg2Type,
                            arg3Type));
                    return createAggregateFunc(arg1, arg2, arg3);
                }
                case 4: // public TAggregate(T1 t1,T2 t2,T3 t3)
                {
                    var arg1Type = _parameterInfos.ElementAt(0).ParameterType;
                    var arg2Type = _parameterInfos.ElementAt(1).ParameterType;
                    var arg3Type = _parameterInfos.ElementAt(2).ParameterType;
                    var arg4Type = _parameterInfos.ElementAt(3).ParameterType;
                    var arg1 = arg1Type == _identityType ? identity : serviceProvider.GetRequiredService(arg1Type);
                    var arg2 = arg2Type == _identityType ? identity : serviceProvider.GetRequiredService(arg2Type);
                    var arg3 = arg3Type == _identityType ? identity : serviceProvider.GetRequiredService(arg3Type);
                    var arg4 = arg4Type == _identityType ? identity : serviceProvider.GetRequiredService(arg4Type);

                    var createAggregateFunc = memoryCache.GetOrCreate(typeOfTAggregate,
                        _ => MyReflectionHelper.CompileConstructor<TAggregate>(typeOfTAggregate,
                            arg1Type,
                            arg2Type,
                            arg3Type,
                            arg4Type));
                    return createAggregateFunc(arg1, arg2, arg3, arg4);
                }

                default:
                    return (TAggregate)CreateInstance(identity, serviceProvider);
            }
        }

        private object CreateInstance(IIdentity identity,
            IServiceProvider resolver)
        {
            var parameters = new object[_parameterInfos.Count];
            foreach (var parameterInfo in _parameterInfos)
            {
                if (parameterInfo.ParameterType == _identityType)
                {
                    parameters[parameterInfo.Position] = identity;
                }
                else
                {
                    parameters[parameterInfo.Position] = resolver.GetRequiredService(parameterInfo.ParameterType);
                }
            }

            return _constructorInfo.Invoke(parameters);
        }
    }
}
