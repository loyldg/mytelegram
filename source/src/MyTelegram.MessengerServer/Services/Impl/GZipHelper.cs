namespace MyTelegram.MessengerServer.Services.Impl;

public class GZipHelper : IGZipHelper //, ISingletonDependency
{
    public byte[] Decompress(byte[] data)
    {
        //return UnGzip(data);
        if (IsGzipPacked(data))
        {
            return UnGzip(data);
        }

        return Unzip(data);
    }

    public byte[] Compress(byte[] data)
    {
        //data.Dump("compress data");
        using var compressedStream = new MemoryStream();
        using var compressor = new GZipStream(compressedStream, CompressionMode.Compress);
        compressor.Write(data, 0, data.Length);
        return compressedStream.ToArray();
        //using var compressedStream = new MemoryStream();
        //using var stream = new MemoryStream(data);
        //using var zipStream = new GZipStream(stream, CompressionMode.Compress);
        //zipStream.CopyTo(compressedStream);

        //return compressedStream.ToArray();
    }

    private static bool IsGzipPacked(byte[] data)
    {
        //gzip header
        if (data[0] == 0x1f && data[1] == 0x8b && data[2] == 0x8)
        {
            return true;
        }

        return false;
    }

    private static byte[] UnGzip(byte[] data)
    {
        //data.Dump("UnGzip data");

        using var decompressedStream = new MemoryStream();
        using var stream = new MemoryStream(data);
        using var zipStream = new GZipStream(stream, CompressionMode.Decompress);
        zipStream.CopyTo(decompressedStream);
        return decompressedStream.ToArray();
        //var decompressedStream = new MemoryStream();
        //using var stream = new MemoryStream(data);
        //using var gzipStream=new GZipOutputStream(stream);
        //gzipStream.CopyTo(decompressedStream);

        //return decompressedStream.ToArray();
        //using var decompressedStream = new MemoryStream();
        //using var stream = new MemoryStream(data);
        //using var zipStream = new GZipStream(stream, CompressionMode.Decompress);
        //zipStream.CopyTo(decompressedStream);

        //return decompressedStream.ToArray();
    }

    private static byte[] Unzip(byte[] packedData)
    {
        //packedData.Dump("Unzip data");
        var outputStream = new MemoryStream();
        using var compressedStream = new MemoryStream(packedData);
        using var inputStream = new InflaterInputStream(compressedStream);
        inputStream.CopyTo(outputStream);
        outputStream.Position = 0;
        return outputStream.ToArray();
    }
}