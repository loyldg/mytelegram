namespace MyTelegram.GatewayServer.Services;

public class ProxyProtocolParser : IProxyProtocolParser
{
    private const int MinHeaderLength = 16; // signature(12) + ver/cmd(1) + fam/proto(1) + len(2)

    private static readonly byte[] Signature =
    [
        0x0D, 0x0A, 0x0D, 0x0A,
        0x00,
        0x0D, 0x0A, 0x51, 0x55, 0x49, 0x54, 0x0A
    ];

    public bool IsProxyProtocolV2(in ReadOnlySequence<byte> buffer)
    {
        if (buffer.Length < Signature.Length)
        {
            return false;
        }

        Span<byte> signature = stackalloc byte[Signature.Length];
        buffer.Slice(0, Signature.Length).CopyTo(signature);

        return signature.SequenceEqual(Signature);
    }

    public bool HasEnoughProxyProtocolV2Data(in ReadOnlySequence<byte> data, out int proxyProtocolHeaderLength)
    {
        proxyProtocolHeaderLength = 0;

        if (data.Length < MinHeaderLength)
        {
            return false;
        }

        Span<byte> header = stackalloc byte[MinHeaderLength];
        data.Slice(0, MinHeaderLength).CopyTo(header);

        for (var i = 0; i < Signature.Length; i++)
        {
            if (header[i] != Signature[i])
            {
                return false;
            }
        }

        var verCmd = header[12];
        var version = (verCmd & 0xF0) >> 4;
        if (version != 2)
        {
            return false;
        }

        var len = (ushort)((header[14] << 8) | header[15]);
        proxyProtocolHeaderLength = MinHeaderLength + len;

        return data.Length >= proxyProtocolHeaderLength;
    }

    public ProxyProtocolFeature? Parse(ref ReadOnlySequence<byte> buffer)
    {
        if (!HasEnoughProxyProtocolV2Data(buffer, out var totalLength))
        {
            return null;
        }

        Span<byte> header = stackalloc byte[MinHeaderLength];
        buffer.Slice(0, MinHeaderLength).CopyTo(header);

        var verCmd = header[12];
        var familyProto = header[13];
        var length = (ushort)((header[14] << 8) | header[15]);

        var version = (verCmd & 0xF0) >> 4;
        var command = verCmd & 0x0F;

        if (version != 2 || command != 0x01) // Must v2 + PROXY
        {
            buffer = buffer.Slice(totalLength);
            return null;
        }

        var family = (familyProto & 0xF0) >> 4;
        //int proto = famProto & 0x0F;

        var addressBlock = buffer.Slice(MinHeaderLength, length);

        // IPv4: 4+4+2+2 = 12 bytes
        if (family == 0x1 && addressBlock.Length >= 12)
        {
            Span<byte> address = stackalloc byte[12];
            addressBlock.Slice(0, 12).CopyTo(address);

            var srcIp = new IPAddress(address.Slice(0, 4));
            var dstIp = new IPAddress(address.Slice(4, 4));
            var srcPort = (address[8] << 8) | address[9];
            var dstPort = (address[10] << 8) | address[11];

            buffer = buffer.Slice(totalLength);
            return new ProxyProtocolFeature(srcIp, dstIp, srcPort, dstPort);
        }
        // IPv6: 16+16+2+2 = 36 bytes

        if (family == 0x2 && addressBlock.Length >= 36)
        {
            Span<byte> address = stackalloc byte[36];
            addressBlock.Slice(0, 36).CopyTo(address);

            var srcIp = new IPAddress(address.Slice(0, 16));
            var dstIp = new IPAddress(address.Slice(16, 16));
            var srcPort = (address[32] << 8) | address[33];
            var dstPort = (address[34] << 8) | address[35];

            buffer = buffer.Slice(totalLength);
            return new ProxyProtocolFeature(srcIp, dstIp, srcPort, dstPort);
        }

        // Skip other address families
        buffer = buffer.Slice(totalLength);
        return null;
    }
}