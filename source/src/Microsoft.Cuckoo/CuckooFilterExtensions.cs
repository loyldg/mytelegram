// -----------------------------------------------------------------------
// <copyright file="CuckooFilterExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;

namespace Microsoft.Cuckoo;

/// <summary>
///     Extension methods for the Cuckoo filter.
/// </summary>
public static class CuckooFilterExtensions
{
    /// <summary>
    ///     Returns a value indicating whether the filter probably contains
    ///     the given item.
    /// </summary>
    /// <param name="filter">Cuckoo filter instance.</param>
    /// <param name="value">Value to check</param>
    /// <returns>True if the filter contains the value, false otherwise.</returns>
    public static bool ContainsString(this CuckooFilter filter,
        string value)
    {
        return filter.Contains(Encoding.ASCII.GetBytes(value));
    }

    /// <summary>
    ///     Inserts the value into the filter. Whereas <see cref="TryInsertString" />
    ///     returns false if the value cannot be inserted, this throws.
    /// </summary>
    /// <param name="filter">Cuckoo filter instance.</param>
    /// <param name="value">Value to insert</param>
    /// <exception cref="FilterFullException">
    ///     Thrown if the filter is
    ///     too full to accept the value.
    /// </exception>
    public static bool InsertString(this CuckooFilter filter,
        string value)
    {
        return filter.TryInsert(Encoding.ASCII.GetBytes(value));
    }

    /// <summary>
    ///     Attempts to insert the value into the filter.
    /// </summary>
    /// <param name="filter">Cuckoo filter instance.</param>
    /// <param name="value">Value to insert</param>
    /// <returns>
    ///     True if it was inserted successfully, false if the
    ///     filter was too full to do so.
    /// </returns>
    public static bool TryInsertString(this CuckooFilter filter,
        string value)
    {
        return filter.TryInsert(Encoding.ASCII.GetBytes(value));
    }
}
