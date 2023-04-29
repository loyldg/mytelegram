using System.Globalization;

namespace MyTelegram.MessengerServer.Services.IdGenerator;

public class HiLoValueGeneratorState : IDisposable
{
    private readonly SemaphoreSlim _semaphoreSlim = new(1);
    private HiLoValue _currentValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HiLoValueGeneratorState" /> class.
    /// </summary>
    /// <param name="blockSize">
    ///     The number of sequential values that can be used, starting from the low value, before
    ///     a new low value must be fetched from the database.
    /// </param>
    public HiLoValueGeneratorState(int blockSize)
    {
        if (blockSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(blockSize), $"Invalid block size {blockSize} for HiLoValue");
        }

        BlockSize = blockSize;
        _currentValue = new HiLoValue(-1, 0);
    }

    public int BlockSize { get; }

    /// <summary>
    ///     Releases the allocated resources for this instance.
    /// </summary>
    public virtual void Dispose()
    {
        _semaphoreSlim.Dispose();
        GC.SuppressFinalize(this);
    }

    private static TValue ConvertResult<TValue>(HiLoValue newValue)
    {
        return (TValue)Convert.ChangeType(newValue.Low, typeof(TValue), CultureInfo.InvariantCulture);
    }

    private HiLoValue GetNextValue()
    {
        HiLoValue originalValue;
        HiLoValue newValue;
        do
        {
            originalValue = _currentValue;
            newValue = originalValue.NextValue();
        } while (Interlocked.CompareExchange(ref _currentValue, newValue, originalValue) != originalValue);

        return newValue;
    }

    /// <summary>
    ///     Gets a value to be assigned to a property.
    /// </summary>
    /// <typeparam name="TValue">The type of values being generated.</typeparam>
    /// <param name="key"></param>
    /// <param name="getNewLowValue">
    ///     A function to get the next low value if needed.
    /// </param>
    /// <param name="idType"></param>
    /// <returns>The value to be assigned to a property.</returns>
    public virtual TValue Next<TValue>(IdType idType,
        long key,
        Func<IdType, long, long> getNewLowValue)
    {
        var newValue = GetNextValue();

        // If the chosen value is outside of the current block then we need a new block.
        // It is possible that other threads will use all of the new block before this thread
        // gets a chance to use the new value, so use a while here to do it all again.
        while (newValue.Low >= newValue.High)
        {
            _semaphoreSlim.Wait();
            try
            {
                // Once inside the lock check to see if another thread already got a new block, in which
                // case just get a value out of the new block instead of requesting one.
                if (newValue.High == _currentValue.High)
                {
                    var newCurrent = getNewLowValue(idType, key);
                    newValue = new HiLoValue(newCurrent, newCurrent + BlockSize);
                    _currentValue = newValue;
                }
                else
                {
                    newValue = GetNextValue();
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        return ConvertResult<TValue>(newValue);
    }

    /// <summary>
    ///     Gets a value to be assigned to a property.
    /// </summary>
    /// <typeparam name="TValue">The type of values being generated.</typeparam>
    /// <param name="key"></param>
    /// <param name="getNewLowValue">
    ///     A function to get the next low value if needed.
    /// </param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="idType"></param>
    /// <returns>The value to be assigned to a property.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    public virtual async ValueTask<TValue> NextAsync<TValue>(
        IdType idType,
        long key,
        Func<IdType, long, CancellationToken, Task<long>> getNewLowValue,
        CancellationToken cancellationToken = default)
    {
        var newValue = GetNextValue();

        // If the chosen value is outside of the current block then we need a new block.
        // It is possible that other threads will use all of the new block before this thread
        // gets a chance to use the new value, so use a while here to do it all again.
        while (newValue.Low >= newValue.High)
        {
            await _semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                // Once inside the lock check to see if another thread already got a new block, in which
                // case just get a value out of the new block instead of requesting one.
                if (newValue.High == _currentValue.High)
                {
                    var newCurrent = await getNewLowValue(idType, key, cancellationToken);
                    newValue = new HiLoValue(newCurrent, newCurrent + BlockSize);
                    _currentValue = newValue;
                }
                else
                {
                    newValue = GetNextValue();
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        return ConvertResult<TValue>(newValue);
    }

    private sealed class HiLoValue
    {
        public HiLoValue(long low,
            long high)
        {
            Low = low;
            High = high;
        }

        public long Low { get; }

        public long High { get; }

        public HiLoValue NextValue()
        {
            return new HiLoValue(Low + 1, High);
        }
    }
}
