// -----------------------------------------------------------------------
// <copyright file="FilterFullException.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Microsoft.Cuckoo;

/// <summary>
///     Exception thrown in <see cref="CuckooFilter.Insert" /> if the filter is
///     too full to accept more items.
/// </summary>
[Serializable]
public class FilterFullException : Exception
{
    /// <summary>
    ///     Creates a new instance of the exception.
    /// </summary>
    public FilterFullException()
        : base("The cuckoo filter is too full to accept more items")
    {
    }
}