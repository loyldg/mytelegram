namespace MyTelegram.Messenger.Extensions;

public static class QueryProcessorExtensions
{
    public static Task<TResult> ProcessAsync<TResult>(this IQueryProcessor queryProcessor, IQuery<TResult> query)
    {
        return queryProcessor.ProcessAsync(query, default);
    }
}