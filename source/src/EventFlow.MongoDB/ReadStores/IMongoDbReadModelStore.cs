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

using System.Linq.Expressions;
using EventFlow.ReadStores;
using MongoDB.Driver;

namespace EventFlow.MongoDB.ReadStores;

public interface IMyMongoDbReadModelStore<TReadModel, TMongoDbContext> :
    IMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IReadModel
    where TMongoDbContext : IMongoDbContext
{
    Task<IAsyncCursor<TResult>> FindAsync<TResult>(
        Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default);
}

public interface IMyMongoDbReadModelStore<TReadModel> : IMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IReadModel
{
    Task<IAsyncCursor<TResult>> FindAsync<TResult>(
        Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default);
}

public interface IMongoDbReadModelStore<TReadModel> : IReadModelStore<TReadModel>
    where TReadModel : class, IReadModel
{
    Task<IAsyncCursor<TReadModel>> FindAsync(
        Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TReadModel>? options = null,
        CancellationToken cancellationToken = default);

    IQueryable<TReadModel> AsQueryable();
}
