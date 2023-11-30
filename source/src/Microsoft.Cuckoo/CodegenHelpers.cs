// -----------------------------------------------------------------------
// <copyright file="CodegenHelpers.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Microsoft.Cuckoo.Benchmark")]
[assembly: InternalsVisibleTo("Microsoft.Cuckoo.Test")]

namespace Microsoft.Cuckoo;

/// <summary>
///     Collection helper methods for generating code and comparators.
/// </summary>
internal static class CodegenHelpers
{
    public static bool BytesEquals(byte[] a, byte[] b)
    {
        return BytesEquals(a, 0, b, 0, a.Length);
    }

    /// <summary>
    ///     Returns whether the two arrays are equal. It assumes that both
    ///     are at least the "length" long.
    /// </summary>
    /// <param name="a">First array</param>
    /// <param name="offsetA">Offset in first array to look from</param>
    /// <param name="b">Second array</param>
    /// <param name="offsetB">Offset in second array to look from</param>
    /// <param name="length">Number of bytes in each to compare</param>
    /// <returns>True if all bytes are equal, false otherwise</returns>
    public static bool BytesEquals(byte[] a, int offsetA, byte[] b, int offsetB, int length)
    {
        var baseOffset = 0;
        for (; baseOffset + Vector<byte>.Count <= length; baseOffset += Vector<byte>.Count)
        {
            var aVec = new Vector<byte>(a, baseOffset + offsetA);
            var bVec = new Vector<byte>(b, baseOffset + offsetB);
            if (aVec != bVec)
            {
                return false;
            }
        }

        var remaining = length - baseOffset;
        for (var i = 0; i < remaining; i++)
        {
            if (a[baseOffset + offsetA + i] != b[baseOffset + offsetB + i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Returns whether the array is all zeroes from the given offset
    ///     and length.
    /// </summary>
    /// <param name="array">Array to check</param>
    /// <param name="offset">Offset to look at</param>
    /// <param name="length">Number of bytes to check</param>
    /// <returns>The number of bytes that are zero</returns>
    public static bool IsZero(byte[] array, int offset, int length)
    {
        var zeroVector = new Vector<byte>(0);

        var baseOffset = 0;
        for (; baseOffset + Vector<byte>.Count <= length; baseOffset += Vector<byte>.Count)
        {
            if (new Vector<byte>(array, baseOffset + offset) != zeroVector)
            {
                return false;
            }
        }

        var remaining = length - baseOffset;
        for (var i = 0; i < remaining; i++)
        {
            if (array[offset + baseOffset + i] != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Creates a function that checks if the fingerprint at the given
    ///     offset (arg 2) in the first argument (arg 1) is equal to the
    ///     fingerprint in arg 3.
    /// </summary>
    /// <param name="fingerprintSize">Number of bytes in the fingerprint</param>
    /// <param name="entriesPerBucket">Number of entries in each bucket</param>
    /// <returns>The created delegate</returns>
    public static Func<byte[], int, byte[], int> CreateFingerprintComparator(uint fingerprintSize,
        uint entriesPerBucket)
    {
        int GetFingerprintIndex(byte[] originalFingerprintBytes,
            int originalFingerprintOffset,
            byte[] newFingerprintBytes)
        {
            var size = (int)fingerprintSize;
            var span = originalFingerprintBytes.AsSpan()[originalFingerprintOffset..];
            for (var entryIndex = 0; entryIndex < entriesPerBucket; entryIndex++)
            {
                var fingerprintBytes = span.Slice(size * entryIndex, size);
                if (fingerprintBytes.SequenceEqual(newFingerprintBytes))
                {
                    return entryIndex;
                }
            }

            return -1;
        }

        return GetFingerprintIndex;
        //var e1 = Emit<Func<byte[], int, byte[], int>>.NewDynamicMethod();

        //Label nextStatement = null;
        //for (var entryIndex = 0; entryIndex < entriesPerBucket; entryIndex++)
        //{
        //    if (nextStatement != null)
        //    {
        //        e1.MarkLabel(nextStatement);
        //    }

        //    nextStatement = e1.DefineLabel();
        //    for (var byteIndex = 0; byteIndex < fingerprintSize; byteIndex++)
        //    {
        //        // Retrieve a[offset + i]
        //        e1.LoadArgument(0);
        //        e1.LoadArgument(1);

        //        var offset = (int)(fingerprintSize * entryIndex + byteIndex);
        //        if (offset > 0)
        //        {
        //            e1.LoadConstant(offset);
        //            e1.Add();
        //        }
        //        e1.LoadElement<byte>();

        //        // Retrieve b[i]
        //        e1.LoadArgument(2);
        //        e1.LoadConstant(byteIndex);
        //        e1.LoadElement<byte>();

        //        // Go to the false return of not equal
        //        e1.UnsignedBranchIfNotEqual(nextStatement);
        //    }

        //    e1.LoadConstant(entryIndex);
        //    e1.Return();
        //}

        //e1.MarkLabel(nextStatement);
        //e1.LoadConstant(-1);
        //e1.Return();

        //return e1.CreateDelegate();
    }

    /// <summary>
    ///     Creates a function that checks if the fingerprint at the given
    ///     offset (arg 2) in the first argument (arg 1) is zero.
    /// </summary>
    /// <param name="fingerprintSize">Number of bytes in the fingerprint</param>
    /// <returns>The created delegate</returns>
    public static Func<byte[], int, bool> CreateZeroChecker(uint fingerprintSize)
    {
        Func<byte[], int, bool> func = (fingerprintBytes,
            offset) =>
        {
            var bytes = fingerprintBytes.AsSpan().Slice(offset, (int)fingerprintSize);
            foreach (var b in bytes)
            {
                if (b != 0)
                {
                    return false;
                }
            }

            return true;
        };

        return func;

        //var e1 = Emit<Func<byte[], int, bool>>.NewDynamicMethod();
        //var returnFalse = e1.DefineLabel();

        //for (var byteIndex = 0; byteIndex < fingerprintSize; byteIndex++)
        //{
        //  // Load the contents array:
        //  e1.LoadArgument(0);

        //  // Set the index we want to offset + i:
        //  e1.LoadArgument(1);
        //  if (byteIndex > 0)
        //  {
        //    e1.LoadConstant(byteIndex);
        //    e1.Add();
        //  }
        //  e1.LoadElement<byte>();


        //  // Go to the next statement (the next entry check) if it's not 0.
        //  e1.LoadConstant(0);
        //  e1.UnsignedBranchIfNotEqual(returnFalse);
        //}

        //// Got down here? We're good, true true.
        //e1.LoadConstant(true);
        //e1.Return();

        //// False branch:
        //e1.MarkLabel(returnFalse);
        //e1.LoadConstant(false);
        //e1.Return();

        //return e1.CreateDelegate();
    }

    /// <summary>
    ///     Creates a function that checks inserts a fingerprint (arg 3) into
    ///     the index (arg 2) in the contents (arg 1) if there's any available
    ///     unassigned fingerprint slot. Returns true if the insertion was
    ///     made successfully.
    /// </summary>
    /// <param name="fingerprintSize">Number of bytes in the fingerprint</param>
    /// <param name="entriesPerBucket">Number of entries in each bucket</param>
    /// <returns>The created delegate</returns>
    public static Func<byte[], int, byte[], bool> CreateInsertIntoBucket(uint fingerprintSize, uint entriesPerBucket)
    {
        Func<byte[], int, byte[], bool> func = (fingerprintBytes,
            fingerprintIndex,
            newFingerprintBytes) =>
        {
            var zeroBytes = new byte[fingerprintSize];
            var bytes = fingerprintBytes.AsSpan().Slice(fingerprintIndex);
            var size = (int)fingerprintSize;
            for (var i = 0; i < bytes.Length; i += size)
            {
                var span = bytes.Slice(i, size);
                if (span.SequenceEqual(zeroBytes))
                {
                    newFingerprintBytes.CopyTo(span);
                    return true;
                }
            }

            return false;
        };
        return func;

        //var e = Emit<Func<byte[], int, byte[], bool>>.NewDynamicMethod();

        //Label nextStatement = null;
        //for (var entryIndex = 0; entryIndex < entriesPerBucket; entryIndex++)
        //{
        //    if (nextStatement != null)
        //    {
        //        e.MarkLabel(nextStatement);
        //    }

        //    nextStatement = e.DefineLabel();

        //    // First, check that all bytes in the target spot are 0.
        //    for (var byteIndex = 0; byteIndex < fingerprintSize; byteIndex++)
        //    {
        //        // Load the contents array:
        //        e.LoadArgument(0);

        //        // Set the index we want to offset + i:
        //        e.LoadArgument(1);
        //        var offset = (int)(fingerprintSize * entryIndex + byteIndex);
        //        if (offset > 0)
        //        {
        //            e.LoadConstant(offset);
        //            e.Add();
        //        }

        //        // Load the element at contents[offset]:
        //        e.LoadElement<byte>();

        //        // Go to the next statement (the next entry check) if it's not 0.
        //        e.LoadConstant(0);
        //        e.UnsignedBranchIfNotEqual(nextStatement);
        //    }

        //    // If we're here, we found a free space that we can put our
        //    // fingerprint in.
        //    for (var byteIndex = 0; byteIndex < fingerprintSize; byteIndex++)
        //    {
        //        // Element setting takes three values: the array to load
        //        // into, the offset, and the data to load.

        //        // Array to load into:
        //        e.LoadArgument(0);

        //        // Target offset in the array:
        //        e.LoadArgument(1);
        //        var offset = (int)(fingerprintSize * entryIndex + byteIndex);
        //        if (offset > 0)
        //        {
        //            e.LoadConstant(offset);
        //            e.Add();
        //        }

        //        // Load the value to be loaded from the fingerprint:
        //        e.LoadArgument(2);
        //        e.LoadConstant(byteIndex);
        //        e.LoadElement<byte>();

        //        // Store it!
        //        e.StoreElement<byte>();
        //    }

        //    // Now we're good, retur:
        //    e.LoadConstant(true);
        //    e.Return();
        //}

        //e.MarkLabel(nextStatement);
        //e.LoadConstant(false);
        //e.Return();

        //return e.CreateDelegate();
    }
}