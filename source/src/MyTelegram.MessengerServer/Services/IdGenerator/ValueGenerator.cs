namespace MyTelegram.MessengerServer.Services.IdGenerator;

// copied from https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/ValueGenerator.cs

public abstract class ValueGenerator<TValue> : ValueGenerator
{
    public new abstract TValue Next(IdType idType,
        long key);

    public new virtual ValueTask<TValue> NextAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        return new ValueTask<TValue>(Next(idType, key));
    }

    protected override object? NextValue(IdType idType,
        long key)
    {
        return Next(idType, key);
    }

    protected override async ValueTask<object?> NextValueAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        return await NextAsync(idType, key, cancellationToken);
    }
}

public abstract class ValueGenerator
{
    public virtual object? Next(IdType idType,
        long key)
    {
        return NextValue(idType, key);
    }

    public virtual ValueTask<object?> NextAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        return NextValueAsync(idType, key, cancellationToken);
    }

    protected abstract object? NextValue(IdType idType,
        long key);

    protected virtual ValueTask<object?> NextValueAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        return new ValueTask<object?>(NextValue(idType, key));
    }
}