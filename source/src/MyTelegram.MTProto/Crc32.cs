namespace MyTelegram.MTProto;

/// <summary>
/// CRC-32 (IEEE 802.3, reflected polynomial 0xEDB88320) as used by the MTProto "full" TCP transport
/// (the same algorithm as zlib's crc32, which tdlib uses).
/// </summary>
public static class Crc32
{
    private static readonly uint[] Table = CreateTable();

    public static uint Compute(ReadOnlySpan<byte> data)
    {
        return Append(0, data);
    }

    /// <summary>
    /// Continues a CRC computation. Pass 0 as <paramref name="crc"/> to start a new one.
    /// </summary>
    public static uint Append(uint crc, ReadOnlySpan<byte> data)
    {
        var value = ~crc;
        var table = Table;
        foreach (var b in data)
        {
            value = table[(value ^ b) & 0xFF] ^ (value >> 8);
        }

        return ~value;
    }

    private static uint[] CreateTable()
    {
        const uint polynomial = 0xEDB88320;
        var table = new uint[256];
        for (uint i = 0; i < table.Length; i++)
        {
            var value = i;
            for (var bit = 0; bit < 8; bit++)
            {
                value = (value & 1) != 0 ? polynomial ^ (value >> 1) : value >> 1;
            }

            table[i] = value;
        }

        return table;
    }
}
