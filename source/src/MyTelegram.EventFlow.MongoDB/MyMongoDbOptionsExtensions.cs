using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory.Queries;
using EventFlow.ReadStores.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace MyTelegram.EventFlow.MongoDB;

public static class MyMongoDbOptionsExtensions
{
    public static IEventFlowOptions AddMyMongoDbReadModel(this IEventFlowOptions options)
    {
        options.ServiceCollection.AddSingleton<DefaultReadModelMongoDbContext>();
        options.ServiceCollection.AddSingleton<IMongoDbContext, DefaultReadModelMongoDbContext>();
        options.ServiceCollection.AddSingleton(typeof(IMongoDbContextFactory<>),
            typeof(DefaultMongoDbContextFactory<>));
        options.ServiceCollection.AddTransient<IReadModelCacheStrategy, DefaultReadModelCacheStrategy>();
        options.ServiceCollection.AddTransient<IReadModelUpdateStrategy, ReadModelUpdateStrategy>();
        options.ServiceCollection.AddTransient<IReadModelUpdateManager, ReadModelUpdateManager>();
        //options.ServiceCollection.AddSingleton(typeof(IMyInMemoryReadStore<>), typeof(MyInMemoryReadStore<>));

        return options;
    }

    public static IEventFlowOptions UseMongoDbReadModel<TAggregate, TIdentity, TReadModel>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            ;
        eventFlowOptions.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(f =>
            f.GetRequiredService<IMongoDbReadModelStore<TReadModel>>());
        //eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel>();
#pragma warning disable CS0618
        eventFlowOptions.UseReadStoreFor<TAggregate, TIdentity, IMongoDbReadModelStore<TReadModel>, TReadModel>();
#pragma warning restore CS0618

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModel<TReadModel, TReadModelLocator>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TReadModelLocator : IReadModelLocator
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>();

        eventFlowOptions.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(f =>
            f.GetRequiredService<IMongoDbReadModelStore<TReadModel>>());
        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel, TReadModelLocator>();

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModel<TAggregate, TIdentity, TReadModel, TDbContext>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TIdentity : IIdentity
        where TAggregate : IAggregateRoot<TIdentity>
        where TDbContext : class, IMongoDbContext
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel, TDbContext>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>,
                MyMongoDbReadModelStore<TReadModel, TDbContext>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel, TDbContext>>()
            ;
        eventFlowOptions.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(f =>
            f.GetRequiredService<IMyMongoDbReadModelStore<TReadModel>>());
        //eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel>();
#pragma warning disable CS0618
        eventFlowOptions
            .UseReadStoreFor<TAggregate, TIdentity, IMyMongoDbReadModelStore<TReadModel>, TReadModel>();
#pragma warning restore CS0618

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModelWithContext<TReadModel, TMongoDbContext>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TMongoDbContext : class, IMongoDbContext
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>,
                MyMongoDbReadModelStore<TReadModel, TMongoDbContext>>()
            .AddTransient<IReadModelStore<TReadModel>>(f =>
                f.GetRequiredService<IMyMongoDbReadModelStore<TReadModel>>())
            ;

        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel>();

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModelWithContext<TReadModel, TReadModelLocator, TMongoDbContext>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TMongoDbContext : class, IMongoDbContext
        where TReadModelLocator : IReadModelLocator
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>,
                MyMongoDbReadModelStore<TReadModel, TMongoDbContext>>()
            .AddTransient<IReadModelStore<TReadModel>>(f =>
                f.GetRequiredService<IMyMongoDbReadModelStore<TReadModel>>())
            ;

        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel, TReadModelLocator>();

        return eventFlowOptions;
    }
}
