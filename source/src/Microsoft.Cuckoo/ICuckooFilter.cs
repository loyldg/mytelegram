// -----------------------------------------------------------------------
// <copyright file="ICuckooFilter.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Cuckoo;

/// <summary>
///     Cuckoo filter interface.
/// </summary>
public interface ICuckooFilter
{
    /// <summary>
    ///     Gets the total size, in memory, of the filter.
    /// </summary>
    uint Size { get; }

    /// <summary>
    ///     Returns a value indicating whether the filter probably contains
    ///     the given item.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <returns>True if the filter contains the value, false otherwise.</returns>
    bool Contains(byte[] value);

    /// <summary>
    ///     Inserts the value into the filter. Whereas <see cref="CuckooFilter.TryInsert" />
    ///     returns false if the value cannot be inserted, this throws.
    /// </summary>
    /// <param name="value">Value to insert</param>
    /// <exception cref="FilterFullException">
    ///     Thrown if the filter is
    ///     too full to accept the value.
    /// </exception>
    void Insert(byte[] value);

    /// <summary>
    ///     Removes a value from the filter.
    /// </summary>
    /// <param name="value">Value to remove</param>
    /// <returns>True if the filter contained the value, false otherwise.</returns>
    bool Remove(byte[] value);

    /// <summary>
    ///     Attempts to insert the value into the filter.
    /// </summary>
    /// <param name="value">Value to insert</param>
    /// <returns>
    ///     True if it was inserted successfully, false if the
    ///     filter was too full to do so.
    /// </returns>
    bool TryInsert(byte[] value);
}
