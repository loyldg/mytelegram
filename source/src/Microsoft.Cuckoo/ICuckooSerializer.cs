// -----------------------------------------------------------------------
// <copyright file="ICuckooSerializer.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace Microsoft.Cuckoo;

public interface ICuckooSerializer
{
    /// <summary>
    ///     Deserializes the CuckooFilter to from a stream.
    /// </summary>
    /// <param name="source">Source stream to read</param>
    /// <param name="hashAlgorithm">Hash algorithm</param>
    CuckooFilter Deserialize(Stream source,
        IHashAlgorithm hashAlgorithm = null);

    /// <summary>
    ///     Serializes the CuckooFilter to a stream.
    /// </summary>
    /// <param name="target">Target stream to write to</param>
    /// <param name="filter">Filter to serialize</param>
    void Serialize(Stream target,
        CuckooFilter filter);
}
