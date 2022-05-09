// -----------------------------------------------------------------------
// <copyright file="CuckooFilter.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cuckoo.UnitTests")]

namespace Microsoft.Cuckoo
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  /// <summary>
  /// Implementation of the Cuckoo filter.
  /// </summary>
  public class CuckooFilter : ICuckooFilter, IEquatable<CuckooFilter>
  {
    /// <summary>
    /// Number of bytes in a <see cref="uint"/>
    /// </summary>
    private const int Uint32Bytes = sizeof(uint);

    /// <summary>
    /// Hashing algorithm.
    /// </summary>
    private readonly IHashAlgorithm _hashAlgorithm;

    /// <summary>
    /// Random instance.
    /// </summary>
    internal readonly Random Random;

    /// <summary>
    /// Pre-allocated buffer we hash value indices into.
    /// </summary>
    private readonly byte[] _valuesBuffer = new byte[Uint32Bytes];

    /// <summary>
    /// Pre-allocated buffer we hash the fingerprint into.
    /// </summary>
    private byte[] _fingerprintBuffer;

    /// <summary>
    /// Pre-allocated buffer we use for swapping fingerprints into.
    /// </summary>
    private byte[] _fingerprintSwapBuffer;

    /// <summary>
    /// Comparator function, see <see cref="CodegenHelpers.CreateFingerprintComparator"/>
    /// </summary>
    private readonly Func<byte[], int, byte[], int> _comparator;

    /// <summary>
    /// Comparator function, see <see cref="CodegenHelpers.CreateZeroChecker"/>
    /// </summary>
    private readonly Func<byte[], int, bool> _zeroCheck;

    /// <summary>
    /// Comparator function, see <see cref="CodegenHelpers.CreateInsertIntoBucket"/>
    /// </summary>
    private readonly Func<byte[], int, byte[], bool> _insertIntoBucket;

    /// <summary>
    /// Creates a new CuckooFilter.
    /// </summary>
    /// <param name="buckets">Number of Buckets to store</param>
    /// <param name="entriesPerBucket">Number of fingerprints stored
    /// in each bucket.</param>
    /// <param name="fingerprintLength">Length of the fingerprint to use</param>
    /// <param name="maxKicks">Maximum number of times to relocate a value
    /// on a collision.</param>
    /// <param name="hashAlgorithm">Hashing algorithm to use</param>
    /// <param name="randomSeed">Random seed value</param>
    public CuckooFilter(
        uint buckets,
        uint entriesPerBucket,
        uint fingerprintLength,
        uint? maxKicks = null,
        IHashAlgorithm hashAlgorithm = null,
        int? randomSeed = null)
    {
      Buckets = buckets;
      EntriesPerBucket = entriesPerBucket;
      _hashAlgorithm = hashAlgorithm ?? XxHashAlgorithm.Instance;
      MaxKicks = maxKicks ?? buckets;

      if (UpperPower2(buckets) != buckets)
      {
        throw new ArgumentException("Buckets must be a power of 2", nameof(buckets));
      }

      _fingerprintBuffer = new byte[fingerprintLength];
      Contents = CreateEmptyBucketData(buckets, entriesPerBucket, fingerprintLength);
      Random = randomSeed == null
          ? new Random()
          : new Random(randomSeed.Value);
      BytesPerBucket = EntriesPerBucket * fingerprintLength;
      _fingerprintSwapBuffer = new byte[_fingerprintBuffer.Length];
      _comparator = CodegenHelpers.CreateFingerprintComparator(fingerprintLength, entriesPerBucket);
      _zeroCheck = CodegenHelpers.CreateZeroChecker(fingerprintLength);
      _insertIntoBucket = CodegenHelpers.CreateInsertIntoBucket(fingerprintLength, entriesPerBucket);
    }

    /// <summary>
    /// Creates a new CuckooFilter.
    /// </summary>
    /// <param name="contents">Contents of th efilter</param>
    /// <param name="entriesPerBucket">Number of fingerprints stored
    /// in each bucket.</param>
    /// <param name="fingerprintLength">Length of the fingerprint to use</param>
    /// <param name="maxKicks">Maximum number of times to relocate a value
    /// on a collision.</param>
    /// <param name="hashAlgorithm">Hashing algorithm to use</param>
    internal CuckooFilter(
        byte[] contents,
        uint entriesPerBucket,
        uint fingerprintLength,
        uint maxKicks,
        IHashAlgorithm hashAlgorithm = null)
    {
      Contents = contents;
      MaxKicks = maxKicks;
      EntriesPerBucket = entriesPerBucket;
      BytesPerBucket = entriesPerBucket * fingerprintLength;
      Buckets = (uint)contents.Length / BytesPerBucket;
      _fingerprintBuffer = new byte[fingerprintLength];
      _fingerprintSwapBuffer = new byte[_fingerprintBuffer.Length];
      _hashAlgorithm = hashAlgorithm ?? XxHashAlgorithm.Instance;
      _zeroCheck = CodegenHelpers.CreateZeroChecker(fingerprintLength);
      _comparator = CodegenHelpers.CreateFingerprintComparator(fingerprintLength, 4);
      _insertIntoBucket = CodegenHelpers.CreateInsertIntoBucket(fingerprintLength, entriesPerBucket);
    }

    /// <summary>
    /// Creates a new optimally-sized CuckooFilter with a target
    /// false-positive-at-capacity.
    /// </summary>
    /// <param name="capacity">Filter capacity</param>
    /// <param name="falsePositiveRate">Desired false positive rate.</param>
    /// <param name="hashAlgorithm">Hashing algorithm to use</param>
    /// <param name="randomSeed">Random seed value</param>
    public CuckooFilter(
        uint capacity,
        double falsePositiveRate,
        IHashAlgorithm hashAlgorithm = null,
        int? randomSeed = null)
    {
      _hashAlgorithm = hashAlgorithm ?? XxHashAlgorithm.Instance;

      // "In summary, we choose (2, 4)-cuckoo filter (i.e., each item has
      // two candidate Buckets and each bucket has up to four fingerprints)
      // as the default configuration, because it achieves the best or
      // close - to - best space efficiency for the false positive
      // rates that most practical applications""
      EntriesPerBucket = 4;

      // Equation here from page 8, step 6, of the paper:
      // ceil(log_2 (2b / \epsilon)
      var desiredLength = Math.Log(2 * (float)EntriesPerBucket / falsePositiveRate, 2);
      var fingerprintLength = (uint)Math.Ceiling(desiredLength / 8);
      _fingerprintBuffer = new byte[fingerprintLength];

      // Not explicitly defined in the paper, however this is the
      // algorithm used in the author's implementation:
      // https://github.com/efficient/cuckoofilter/blob/master/src/cuckoofilter.h#L89
      Buckets = UpperPower2(capacity / EntriesPerBucket);
      if ((double)capacity / Buckets / EntriesPerBucket > 0.96)
      {
        Buckets <<= 1;
      }

      MaxKicks = Buckets;
      Random = randomSeed == null
          ? new Random()
          : new Random(randomSeed.Value);
      Contents = CreateEmptyBucketData(Buckets, EntriesPerBucket, fingerprintLength);
      BytesPerBucket = EntriesPerBucket * fingerprintLength;
      _fingerprintSwapBuffer = new byte[_fingerprintBuffer.Length];
      _zeroCheck = CodegenHelpers.CreateZeroChecker(fingerprintLength);
      _comparator = CodegenHelpers.CreateFingerprintComparator(fingerprintLength, 4);
      _insertIntoBucket = CodegenHelpers.CreateInsertIntoBucket(fingerprintLength, EntriesPerBucket);
    }

    /// <summary>
    /// Gets the number of Buckets the filter contains.
    /// </summary>
    public uint Buckets { get; }

    /// <summary>
    /// Number of bytes each bucket takes.
    /// </summary>
    public uint BytesPerBucket { get; }

    /// <summary>
    /// Gets the number of fingerprints to store per bucket.
    /// </summary>
    public uint EntriesPerBucket { get; }

    /// <summary>
    /// Gets the length of the fingerprint.
    /// </summary>
    public uint FingerprintLength => (uint)_fingerprintBuffer.Length;

    /// <summary>
    /// Gets the max number of times we'll try to kick and item from a
    /// bucket when we insert before giving it.
    /// </summary>
    public uint MaxKicks { get; }

    /// <summary>
    /// Gets the total size, in memory, of the filter.
    /// </summary>
    public uint Size => (uint)Contents.Length;

    /// <summary>
    /// Contents of the cuckoo filter.
    /// </summary>
    internal byte[] Contents { get; }

    /// <summary>
    /// Returns a value indicating whether the filter probably contains
    /// the given item.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <returns>True if the filter contains the value, false otherwise.</returns>
    public bool Contains(byte[] value)
    {
      var fingerprint = GetFingerprint(value);
      _hashAlgorithm.Hash(_valuesBuffer, value, Uint32Bytes);

      var index1 = ToInt32(_valuesBuffer) & (Buckets - 1);
      if (_comparator(Contents, IndexToOffset(index1), fingerprint) != -1)
      {
        return true;
      }

      var index2 = DeriveIndex2(fingerprint, index1);
      if (_comparator(Contents, IndexToOffset(index2), fingerprint) != -1)
      {
        return true;
      }

      return false;
    }

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public bool Equals(CuckooFilter other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return Buckets == other.Buckets
             && BytesPerBucket == other.BytesPerBucket
             && CodegenHelpers.BytesEquals(Contents, other.Contents)
             && EntriesPerBucket == other.EntriesPerBucket
             && MaxKicks == other.MaxKicks;
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != GetType())
      {
        return false;
      }

      return Equals((CuckooFilter)obj);
    }

    /// <summary>Serves as the default hash function.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
      unchecked
      {
        var contentHash = new byte[4];
        _hashAlgorithm.Hash(contentHash, Contents, 4);

        var hashCode = (int)Buckets;
        hashCode = (hashCode * 397) ^ (int)BytesPerBucket;
        hashCode = (hashCode * 397) ^ (contentHash[0] | (contentHash[1] << 8)
                                                      | (contentHash[2] << 16)
                                                      | (contentHash[3] << 24));
        hashCode = (hashCode * 397) ^ (int)EntriesPerBucket;
        hashCode = (hashCode * 397) ^ (int)MaxKicks;
        return hashCode;
      }
    }

    /// <summary>
    /// Inserts the value into the filter. Whereas <see cref="TryInsert"/>
    /// returns false if the value cannot be inserted, this throws.
    /// </summary>
    /// <param name="value">Value to insert</param>
    /// <exception cref="FilterFullException">Thrown if the filter is
    /// too full to accept the value.</exception>
    public void Insert(byte[] value)
    {
      if (!TryInsert(value))
      {
        throw new FilterFullException();
      }
    }

    /// <summary>
    /// Removes a value from the filter.
    /// </summary>
    /// <param name="value">Value to remove</param>
    /// <returns>True if the filter contained the value, false otherwise.</returns>
    public bool Remove(byte[] value)
    {
      var fingerprint = GetFingerprint(value);
      _hashAlgorithm.Hash(_valuesBuffer, value, Uint32Bytes);

      var index1 = ToInt32(_valuesBuffer) & (Buckets - 1);

      var offset = IndexToOffset(index1);
      var removal = _comparator(Contents, offset, fingerprint);
      if (removal != -1)
      {
        Array.Clear(Contents, offset + _fingerprintBuffer.Length * removal, fingerprint.Length);
        return true;
      }


      var index2 = DeriveIndex2(fingerprint, index1);
      offset = IndexToOffset(index2);
      removal = _comparator(Contents, offset, fingerprint);
      if (removal != -1)
      {
        Array.Clear(Contents, offset + _fingerprintBuffer.Length * removal, fingerprint.Length);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Attempts to insert the value into the filter.
    /// </summary>
    /// <param name="value">Value to insert</param>
    /// <returns>True if it was inserted successfully, false if the
    /// filter was too full to do so.</returns>
    public bool TryInsert(byte[] value)
    {
      return TryInsertInner(value);
    }

    /// <summary>
    /// Attempts to insert the value into the filter.
    /// </summary>
    /// <param name="value">Value to insert</param>
    /// <returns>True if it was inserted successfully, false if the
    /// filter was too full to do so.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryInsertInner(byte[] value)
    {
      var fingerprint = GetFingerprint(value);
      _hashAlgorithm.Hash(_valuesBuffer, value, Uint32Bytes);
      var index1 = BoundToBucketCount(ToInt32(_valuesBuffer));

      if (_insertIntoBucket(Contents, IndexToOffset(index1), fingerprint))
      {
        return true;
      }

      var index2 = DeriveIndex2(fingerprint, index1);
      if (_insertIntoBucket(Contents, IndexToOffset(index2), fingerprint))
      {
        return true;
      }

      var targetIndex = Random.Next(1) == 0
          ? index1
          : index2;

      for (var i = 0; i < MaxKicks; i++)
      {
        fingerprint = SwapIntoBucket(fingerprint, targetIndex);
        targetIndex = DeriveIndex2(fingerprint, targetIndex);

        if (_insertIntoBucket(Contents, IndexToOffset(targetIndex), fingerprint))
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Returns a nicely formatted version of the filter. Used for
    /// examining internal state while testing.
    /// </summary>
    /// <returns></returns>
    internal IList<IList<string>> DumpDebug()
    {
      var list = new List<IList<string>>();
      for (var offset = 0L; offset < Contents.Length; offset += BytesPerBucket)
      {
        var items = new List<string>();
        for (var i = 0; i < EntriesPerBucket; i++)
        {
          var target = (int)offset + i * _fingerprintBuffer.Length;
          if (!_zeroCheck(Contents, target))
          {
            items.Add(Encoding.ASCII.GetString(Contents, target, _fingerprintBuffer.Length));
          }
        }

        list.Add(items);
      }

      return list;
    }

    private static byte[] CreateEmptyBucketData(long buckets, uint itemsPerBucket, uint bytesPerItem)
    {
      return new byte[buckets * itemsPerBucket * bytesPerItem];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ToInt32(byte[] data)
    {
      int x = data[0] << 24;
      x |= data[1] << 16;
      x |= data[2] << 8;
      x |= data[3];
      return x;
    }

    private static uint UpperPower2(uint x)
    {
      x--;
      x |= x >> 1;
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      x |= x >> 32;
      x++;
      return x;
    }

    /// <summary>
    /// Ensures the value is less than or equal to the number of Buckets.
    /// </summary>
    /// <param name="value">Value to bound</param>
    /// <returns>Truncated value</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private long BoundToBucketCount(long value)
    {
      // this.Buckets is always a power of 2, so to ensure index1 is <=1
      // the number of Buckets, we mask it against Buckets - 1. So if
      // Buckets is 16 (0b10000), we mask it against (0b01111).
      return value & (Buckets - 1);
    }

    /// <summary>
    /// Get the alternative index for an item, given its primary
    /// index and fingerprint.
    /// </summary>
    /// <param name="fingerprint">Fingerprint value</param>
    /// <param name="index1">Primary index</param>
    /// <returns>The secondary index</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private long DeriveIndex2(byte[] fingerprint, long index1)
    {
      _hashAlgorithm.Hash(_valuesBuffer, fingerprint, Uint32Bytes);
      return index1 ^ BoundToBucketCount(ToInt32(_valuesBuffer));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte[] GetFingerprint(byte[] input)
    {
      _hashAlgorithm.Hash(_fingerprintBuffer, input, _fingerprintBuffer.Length);
      if (_zeroCheck(_fingerprintBuffer, 0))
      {
        for (var i = 0; i < _fingerprintBuffer.Length; i++)
        {
          _fingerprintBuffer[i] = 0xff;
        }
      }

      return _fingerprintBuffer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int IndexToOffset(long index)
    {
      return (int)(index * BytesPerBucket);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte[] SwapIntoBucket(byte[] fingerprint, long bucket)
    {
      var subIndex = Random.Next((int)EntriesPerBucket);
      var offset = IndexToOffset(bucket) + subIndex * fingerprint.Length;
      var newFingerprint = _fingerprintSwapBuffer;

      Array.Copy(Contents, offset, _fingerprintSwapBuffer, 0, fingerprint.Length);
      Array.Copy(fingerprint, 0, Contents, offset, fingerprint.Length);
      _fingerprintSwapBuffer = fingerprint;
      _fingerprintBuffer = newFingerprint;

      return newFingerprint;
    }
  }
}
