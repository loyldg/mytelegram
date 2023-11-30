// -----------------------------------------------------------------------
// <copyright file="SimpleStreamSerializer.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace Microsoft.Cuckoo;

/// <summary>
///     Simple stream serializer that writes the cuckoo filter directly out
///     to a stream. Investigate <see cref="GzipStreamSerializer" /> for a more
///     storage-friendly version.
/// </summary>
public class SimpleStreamSerializer : ICuckooSerializer
{
    /// <summary>
    ///     Deserializes the CuckooFilter to from a stream.
    /// </summary>
    /// <param name="source">Source stream to read</param>
    /// <param name="hashAlgorithm">Hash algorithm</param>
    public virtual CuckooFilter Deserialize(Stream source, IHashAlgorithm hashAlgorithm = null)
    {
        var paramsReadBuffer = new byte[16];
        source.Read(paramsReadBuffer, 0, 16);

        var entriesPerBucket = ReadBigEndianUint(paramsReadBuffer, 0);
        var fingerprintLength = ReadBigEndianUint(paramsReadBuffer, 4);
        var buckets = ReadBigEndianUint(paramsReadBuffer, 8);
        var maxKicks = ReadBigEndianUint(paramsReadBuffer, 12);

        var contents = new byte[buckets * entriesPerBucket * fingerprintLength];
        source.Read(contents, 0, contents.Length);

        return new CuckooFilter(
            contents,
            entriesPerBucket,
            fingerprintLength,
            maxKicks,
            hashAlgorithm);
    }

    /// <summary>
    ///     Serializes the CuckooFilter to a stream.
    /// </summary>
    /// <param name="target">Target stream to write to</param>
    /// <param name="filter">Filter to serialize</param>
    public virtual void Serialize(Stream target, CuckooFilter filter)
    {
        var writeBuffer = new byte[16];
        WriteBigEndianUint(writeBuffer, filter.EntriesPerBucket, 0);
        WriteBigEndianUint(writeBuffer, filter.FingerprintLength, 4);
        WriteBigEndianUint(writeBuffer, filter.Buckets, 8);
        WriteBigEndianUint(writeBuffer, filter.MaxKicks, 12);
        target.Write(writeBuffer, 0, 16);
        target.Write(filter.Contents, 0, filter.Contents.Length);
    }

    private static uint ReadBigEndianUint(byte[] target, int offset)
    {
        uint x = 0;
        x |= (uint)(target[offset] << 24);
        x |= (uint)(target[offset + 1] << 16);
        x |= (uint)(target[offset + 2] << 8);
        x |= target[offset + 3];

        return x;
    }

    private static void WriteBigEndianUint(byte[] target, uint number, int offset)
    {
        target[offset] = (byte)(number >> 24);
        target[offset + 1] = (byte)(number >> 16);
        target[offset + 2] = (byte)(number >> 8);
        target[offset + 3] = (byte)number;
    }
}