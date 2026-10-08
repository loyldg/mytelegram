using EventFlow.Queries;

namespace MyTelegram.EventFlow.Extensions;

public static class QueryProcessorExtensions
{
    public static Task<TResult> ProcessAsync<TResult>(
        this IQueryProcessor queryProcessor,
        IQuery<TResult> query)
    {
        return queryProcessor.ProcessAsync(query, CancellationToken.None);
    }
}