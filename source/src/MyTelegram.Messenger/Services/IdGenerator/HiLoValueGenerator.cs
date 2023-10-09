namespace MyTelegram.Messenger.Services.IdGenerator;

public abstract class HiLoValueGenerator<TValue> : ValueGenerator<TValue>
{
    private readonly HiLoValueGeneratorState _generatorState;

    protected HiLoValueGenerator(HiLoValueGeneratorState generatorState)
    {
        _generatorState = generatorState;
    }

    public override TValue Next(IdType idType,
        long key)
    {
        return _generatorState.Next<TValue>(idType, key, GetNewLowValue);
    }

    public override ValueTask<TValue> NextAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        return _generatorState.NextAsync<TValue>(idType, key, GetNewLowValueAsync, cancellationToken);
    }

    //protected abstract HiLoValueGeneratorState GetGeneratorState(IdType idType,
    //    long key);

    /// <summary>
    ///     Gets the low value for the next block of values to be used.
    /// </summary>
    /// <returns>The low value for the next block of values to be used.</returns>
    protected abstract long GetNewLowValue(IdType idType, long key);


    /// <summary>
    ///     Gets the low value for the next block of values to be used.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="idType"></param>
    /// <returns>The low value for the next block of values to be used.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    protected virtual Task<long> GetNewLowValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
        => Task.FromResult(GetNewLowValue(idType, key));
}