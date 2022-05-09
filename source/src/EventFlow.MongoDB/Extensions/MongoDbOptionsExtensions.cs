// The MIT License (MIT)
// 
// Copyright (c) 2015-2021 Rasmus Mikkelsen
// Copyright (c) 2015-2021 eBay Software Foundation
// https://github.com/eventflow/EventFlow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EventFlow.MongoDB.Extensions;

public static class MongoDbOptionsExtensions
{
    public static IEventFlowOptions ConfigureMongoDb(
        this IEventFlowOptions eventFlowOptions,
        string url,
        string database)
    {
        var mongoUrl = new MongoUrl(url);
        var mongoClient = new MongoClient(mongoUrl);
        return eventFlowOptions
            .ConfigureMongoDb(mongoClient, database);
    }

    public static IEventFlowOptions ConfigureMongoDb(
        this IEventFlowOptions eventFlowOptions,
        string database)
    {
        var mongoClient = new MongoClient();
        return eventFlowOptions
            .ConfigureMongoDb(mongoClient, database);
    }

    public static IEventFlowOptions ConfigureMongoDb(
        this IEventFlowOptions eventFlowOptions,
        IMongoClient mongoClient,
        string database)
    {
        var mongoDatabase = mongoClient.GetDatabase(database);
        return eventFlowOptions.ConfigureMongoDb(() => mongoDatabase);
    }

    public static IEventFlowOptions ConfigureMongoDb(
        this IEventFlowOptions eventFlowOptions,
        Func<IMongoDatabase> mongoDatabaseFactory)
    {
        eventFlowOptions.ServiceCollection.AddSingleton(_ => mongoDatabaseFactory())
            .AddSingleton<IReadModelDescriptionProvider, ReadModelDescriptionProvider>()
            .AddSingleton<IMongoDbEventSequenceStore, MongoDbEventSequenceStore>()
            ;
        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModelWithContext<TReadModel, TMongoDbContext>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TMongoDbContext : class, IMongoDbContext
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>,
                MyMongoDbReadModelStore<TReadModel, TMongoDbContext>>()
            .AddTransient<IReadModelStore<TReadModel>>(f =>
                f.GetRequiredService<IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>>())
            ;

        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel>();

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModelWithContext<TReadModel, TMongoDbContext, TReadModelLocator>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
        where TMongoDbContext : class, IMongoDbContext
        where TReadModelLocator : IReadModelLocator
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>,
                MyMongoDbReadModelStore<TReadModel, TMongoDbContext>>()
            .AddTransient<IReadModelStore<TReadModel>>(f =>
                f.GetRequiredService<IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>>())
            ;

        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel, TReadModelLocator>();

        return eventFlowOptions;
    }

    public static IEventFlowOptions UseMongoDbReadModel<TAggregate,TIdentity,TReadModel>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
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


    public static IEventFlowOptions UseMongoDbReadModel<TReadModel>(
        this IEventFlowOptions eventFlowOptions)
        where TReadModel : class, IMongoDbReadModel
    {
        eventFlowOptions.ServiceCollection
            .AddTransient<IMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            .AddTransient<IMyMongoDbReadModelStore<TReadModel>, MyMongoDbReadModelStore<TReadModel>>()
            ;
        eventFlowOptions.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(f =>
            f.GetRequiredService<IMongoDbReadModelStore<TReadModel>>());
        eventFlowOptions.UseReadStoreFor<IMongoDbReadModelStore<TReadModel>, TReadModel>();

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
}
