using System.IO.Hashing;
using System.Runtime.CompilerServices;

namespace MyTelegram.Messenger.Services.Filters;

public sealed class SimpleCuckooFilter
{
    private readonly int _bucketSize; // slots per bucket (b)

    // fingerprint bits & packing
    private readonly int _fpBits; // f
    private readonly ulong _fpMask; // (1<<f)-1
    private readonly int _mask; // bucketCount - 1
    private readonly int _maxKicks;

    private readonly Random _random = new();
    private readonly byte[] _storage; // packed bit array

    /// <summary>
    /// Construct from expected capacity and target false-positive rate.
    /// </summary>
    /// <param name="expectedCapacity">expected number of items (n)</param>
    /// <param name="falsePositiveRate">target false positive rate p (e.g. 0.01)</param>
    /// <param name="bucketSize">slots per bucket (b), default 4</param>
    /// <param name="maxKicks">max cuckoo kicks on insert</param>
    public SimpleCuckooFilter(int expectedCapacity = 1 << 20, double falsePositiveRate = 0.01, int bucketSize = 4, int maxKicks = 500)
    {
        if (expectedCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(expectedCapacity));
        }

        if (!(falsePositiveRate is > 0 and < 1))
        {
            throw new ArgumentOutOfRangeException(nameof(falsePositiveRate));
        }

        if (bucketSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bucketSize));
        }

        _bucketSize = bucketSize;
        _maxKicks = Math.Max(32, maxKicks);

        // conservative load factor
        const double loadFactor = 0.85;

        var bucketsNeeded = Math.Max(4L, (long)Math.Ceiling(expectedCapacity / (loadFactor * bucketSize)));
        var pow = 1;
        while (pow < bucketsNeeded)
        {
            pow <<= 1;
        }

        var bucketCount = pow;
        _mask = bucketCount - 1;

        // fingerprint bits: f = ceil(log2(2*b / p))
        var fBitsD = Math.Ceiling(Math.Log(2.0 * bucketSize / falsePositiveRate, 2.0));
        var fBits = (int)Math.Max(1, Math.Min(32, fBitsD)); // limit to 1..32 for packed uint-friendly
        _fpBits = fBits;
        _fpMask = _fpBits >= 64 ? ulong.MaxValue : (1UL << _fpBits) - 1UL;

        var totalSlots = bucketCount * _bucketSize;
        var totalBits = totalSlots * _fpBits;
        var totalBytes = (totalBits + 7) >> 3;
        _storage = new byte[totalBytes];
    }

    /// <summary>
    /// Create with explicit bucketCount (power of two), bucketSize and fingerprint bits.
    /// Use this if you want full control.
    /// </summary>
    public SimpleCuckooFilter(int bucketCount, int bucketSize, int fpBits, int maxKicks = 500)
    {
        if (bucketCount <= 0 || (bucketCount & (bucketCount - 1)) != 0)
        {
            throw new ArgumentException("bucketCount must be power of two", nameof(bucketCount));
        }

        if (bucketSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bucketSize));
        }

        if (fpBits <= 0 || fpBits > 32)
        {
            throw new ArgumentOutOfRangeException(nameof(fpBits)); // support up to 32
        }

        _bucketSize = bucketSize;
        _maxKicks = Math.Max(32, maxKicks);
        _mask = bucketCount - 1;
        _fpBits = fpBits;
        _fpMask = (1UL << _fpBits) - 1UL;

        var totalSlots = bucketCount * _bucketSize;
        var totalBits = totalSlots * _fpBits;
        _storage = new byte[(totalBits + 7) >> 3];
    }

    public byte[] GetData()
    {
        return _storage;
    }

    public bool LoadData(ReadOnlySpan<byte> data)
    {
        if (data.Length == _storage.Length)
        {
            // throw new ArgumentException("data length mismatch", nameof(data));
            data.CopyTo(_storage);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Add key. Returns true if added (or already present), false if failed.
    /// </summary>
    public bool Add(ReadOnlySpan<byte> key)
    {
        var h = XxHash64.HashToUInt64(key);
        var fp = (uint)((h & _fpMask) | 1UL); // non-zero fingerprint (fits in 32 bits since fpBits <=32)
        var i1 = (int)(h & (ulong)_mask);
        var i2 = AltIndex(i1, fp);

        if (InsertToBucket(i1, fp) || InsertToBucket(i2, fp))
        {
            return true;
        }

        // kick loop
        var currentIndex = _random.Next(2) == 0 ? i1 : i2;
        var currentFingerprint = fp;
        for (var k = 0; k < _maxKicks; k++)
        {
            var slot = _random.Next(_bucketSize);
            var slotIndex = currentIndex * _bucketSize + slot;
            var evicted = ReadFingerprint(slotIndex);
            WriteFingerprint(slotIndex, currentFingerprint);
            if (evicted == 0)
            {
                return true;
            }

            currentFingerprint = evicted & (uint)_fpMask;
            currentIndex = AltIndex(currentIndex, currentFingerprint);
            if (InsertToBucket(currentIndex, currentFingerprint))
            {
                return true;
            }
        }

        return false; // insertion failed
    }

    /// <summary>
    /// Clear filter (reset).
    /// </summary>
    public void Clear()
    {
        Array.Clear(_storage, 0, _storage.Length);
    }

    /// <summary>
    /// Check possible presence. May return false positives.
    /// </summary>
    public bool Contains(ReadOnlySpan<byte> key)
    {
        var h = XxHash64.HashToUInt64(key);
        var fp = (uint)((h & _fpMask) | 1UL);
        var i1 = (int)(h & (ulong)_mask);
        var i2 = AltIndex(i1, fp);
        return BucketContains(i1, fp) || BucketContains(i2, fp);
    }

    /// <summary>
    /// Delete (remove) an element. Returns true if removed.
    /// </summary>
    public bool Delete(ReadOnlySpan<byte> key)
    {
        var h = XxHash64.HashToUInt64(key);
        var fp = (uint)((h & _fpMask) | 1UL);
        var i1 = (int)(h & (ulong)_mask);
        var i2 = AltIndex(i1, fp);
        return RemoveFromBucket(i1, fp) || RemoveFromBucket(i2, fp);
    }

    /// <summary>
    /// Return memory used by packed storage in bytes.
    /// </summary>
    public long GetMemoryBytes()
    {
        return _storage.Length;
    }

    // Alternate index function using simple mixing on fingerprint to avoid extra hashing
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int AltIndex(int i1, uint fingerprint)
    {
        const uint mul = 0x9E3779B9U;
        var mixed = (uint)((fingerprint ^ ((ulong)fingerprint >> 16)) * mul);
        return (int)(((uint)i1 ^ mixed) & (uint)_mask);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool BucketContains(int bucketIndex, uint fp)
    {
        var baseSlot = bucketIndex * _bucketSize;
        for (var s = 0; s < _bucketSize; s++)
        {
            if (ReadFingerprint(baseSlot + s) == fp)
            {
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool InsertToBucket(int bucketIndex, uint fp)
    {
        var baseSlot = bucketIndex * _bucketSize;
        for (var s = 0; s < _bucketSize; s++)
        {
            var slotIndex = baseSlot + s;
            var cur = ReadFingerprint(slotIndex);
            if (cur == fp)
            {
                return true; // already present
            }

            if (cur == 0)
            {
                WriteFingerprint(slotIndex, fp);
                return true;
            }
        }

        return false;
    }

    // Read fingerprint at slotIndex (0..totalSlots-1)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint ReadFingerprint(int slotIndex)
    {
        var bitPos = (long)slotIndex * _fpBits;
        var bytePos = (int)(bitPos >> 3);
        var bitOffset = (int)(bitPos & 7);
        // we need up to 32 bits; read up to 5 bytes safely
        ulong chunk = 0;
        var available = _storage.Length - bytePos;
        var toRead = Math.Min(5, Math.Max(0, available));
        for (var i = 0; i < toRead; i++)
        {
            chunk |= (ulong)_storage[bytePos + i] << (8 * i);
        }

        // shift right by bitOffset and mask
        var val = (chunk >> bitOffset) & _fpMask;
        return (uint)val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool RemoveFromBucket(int bucketIndex, uint fp)
    {
        var baseSlot = bucketIndex * _bucketSize;
        for (var s = 0; s < _bucketSize; s++)
        {
            var si = baseSlot + s;
            if (ReadFingerprint(si) == fp)
            {
                WriteFingerprint(si, 0u);
                return true;
            }
        }

        return false;
    }

    // Write fingerprint (fits in fpBits) into slotIndex
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteFingerprint(int slotIndex, uint fp)
    {
        var bitPos = (long)slotIndex * _fpBits;
        var bytePos = (int)(bitPos >> 3);
        var bitOffset = (int)(bitPos & 7);

        // current available bytes we may touch
        var available = _storage.Length - bytePos;
        var toReadWrite = Math.Min(5, Math.Max(0, available));

        // load existing chunk
        ulong chunk = 0;
        for (var i = 0; i < toReadWrite; i++)
        {
            chunk |= (ulong)_storage[bytePos + i] << (8 * i);
        }

        // clear the fpBits region then set new value
        var clearMask = ~(_fpMask << bitOffset);
        chunk = (chunk & clearMask) | ((fp & _fpMask) << bitOffset);

        // write back affected bytes
        for (var i = 0; i < toReadWrite; i++)
        {
            _storage[bytePos + i] = (byte)((chunk >> (8 * i)) & 0xFF);
        }
    }
}