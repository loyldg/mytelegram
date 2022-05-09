namespace MyTelegram.MessengerServer.Services.IdGenerator;

// copied from https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/ValueGenerator.cs

public abstract class ValueGenerator<TValue> : ValueGenerator
{
    public new abstract TValue Next(IdType idType, long key);

    public new virtual ValueTask<TValue> NextAsync(IdType idType, long key,
        CancellationToken cancellationToken = default) => new(Next(idType, key));

    protected override object? NextValue(IdType idType, long key) => Next(idType, key);

    protected override async ValueTask<object?> NextValueAsync(IdType idType, long key,
        CancellationToken cancellationToken = default) =>
        await NextAsync(idType, key, cancellationToken).ConfigureAwait(false);
}

public abstract class ValueGenerator
{
    public virtual object? Next(IdType idType, long key) => NextValue(idType, key);

    protected abstract object? NextValue(IdType idType, long key);

    public virtual ValueTask<object?> NextAsync(IdType idType, long key,
        CancellationToken cancellationToken = default) => NextValueAsync(idType, key, cancellationToken);

    protected virtual ValueTask<object?> NextValueAsync(IdType idType, long key,
        CancellationToken cancellationToken = default) => new(NextValue(idType, key));
}