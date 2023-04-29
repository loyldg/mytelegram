// -----------------------------------------------------------------------
// <copyright file="GzipStreamSerializer.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.IO.Compression;

namespace Microsoft.Cuckoo;

/// <summary>
///     Serializer that creates a gzipped stream.
/// </summary>
public class GzipStreamSerializer : SimpleStreamSerializer
{
    private readonly CompressionLevel _compressionLevel;

    /// <summary>
    ///     Creates a new serialize that serializes into a gzipped string.
    /// </summary>
    /// <param name="compressionLevel">Gzip compression level</param>
    public GzipStreamSerializer(CompressionLevel compressionLevel = CompressionLevel.Optimal)
    {
        _compressionLevel = compressionLevel;
    }

    /// <summary>
    ///     Deserializes the CuckooFilter to from a stream.
    /// </summary>
    /// <param name="source">Source stream to read</param>
    /// <param name="hashAlgorithm">Hash algorithm</param>
    public override CuckooFilter Deserialize(Stream source,
        IHashAlgorithm hashAlgorithm = null)
    {
        var compressionStream = new GZipStream(source, CompressionMode.Decompress);
        return base.Deserialize(compressionStream, hashAlgorithm);
    }

    /// <summary>
    ///     Serializes the CuckooFilter to a stream.
    /// </summary>
    /// <param name="target">Target stream to write to</param>
    /// <param name="filter">Filter to serialize</param>
    public override void Serialize(Stream target,
        CuckooFilter filter)
    {
        var compressionStream = new GZipStream(target, _compressionLevel);
        base.Serialize(compressionStream, filter);
        compressionStream.Flush();
    }
}
