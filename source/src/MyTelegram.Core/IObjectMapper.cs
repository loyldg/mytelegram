namespace MyTelegram.Core;
// Copied from Abp

/// <summary>
///     Defines a simple interface to automatically map objects for a specific context.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface IObjectMapper<TContext> : IObjectMapper
{
}

/// <summary>
///     Defines a simple interface to automatically map objects.
/// </summary>
public interface IObjectMapper
{
    /// <summary>
    ///     Converts an object to another. Creates a new object of <see cref="TDestination" />.
    /// </summary>
    /// <typeparam name="TDestination">Type of the destination object</typeparam>
    /// <typeparam name="TSource">Type of the source object</typeparam>
    /// <param name="source">Source object</param>
    [return: NotNullIfNotNull("source")]
    TDestination? Map<TSource, TDestination>(TSource source);

    /// <summary>
    ///     Execute a mapping from the source object to the existing destination object
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="source">Source object</param>
    /// <param name="destination">Destination object</param>
    /// <returns>Returns the same <see cref="destination" /> object after mapping operation</returns>
    [return: NotNullIfNotNull("source")]
    TDestination? Map<TSource, TDestination>(TSource source,
        TDestination destination);
}

/// <summary>
///     Maps an object to another.
///     Implement this interface to override object to object mapping for specific types.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TDestination"></typeparam>
public interface IObjectMapper<in TSource, TDestination>
{
    /// <summary>
    ///     Converts an object to another. Creates a new object of <see cref="TDestination" />.
    /// </summary>
    /// <param name="source">Source object</param>
    [return: NotNullIfNotNull("source")]
    TDestination? Map(TSource source);

    /// <summary>
    ///     Execute a mapping from the source object to the existing destination object
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="destination">Destination object</param>
    /// <returns>Returns the same <see cref="destination" /> object after mapping operation</returns>
    [return: NotNullIfNotNull("source")]
    TDestination? Map(TSource source,
        TDestination destination);
}
