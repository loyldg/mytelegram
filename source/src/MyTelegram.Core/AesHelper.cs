using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MyTelegram.Core;

public class AesHelper : IAesHelper, ISingletonDependency
{
    private const int TagSize = 16;

    public void EncryptGcm(ReadOnlySpan<byte> plainText, Span<byte> cipherText,
        ReadOnlySpan<byte> key,
        Span<byte> nonce, Span<byte> tag, ReadOnlySpan<byte> associatedData = default)
    {
        RandomNumberGenerator.Fill(nonce);
        using var aes = new AesGcm(key, TagSize);
        aes.Encrypt(nonce, plainText, cipherText, tag, associatedData);
    }

    public void DecryptGcm(Span<byte> plainText, ReadOnlySpan<byte> cipherText,
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> tag, ReadOnlySpan<byte> associatedData = default)
    {
        using var aes = new AesGcm(key, TagSize);
        aes.Decrypt(nonce, cipherText, tag, plainText, associatedData);
    }

    public void EncryptIge(ReadOnlySpan<byte> source, byte[] key, ReadOnlySpan<byte> iv, Span<byte> destination)
    {
        var outputBytes = ArrayPool<byte>.Shared.Rent(source.Length);
        AesIgeEncryptDecrypt(source, key, iv, outputBytes, true);
        outputBytes.AsSpan(0, source.Length).CopyTo(destination);
        ArrayPool<byte>.Shared.Return(outputBytes);
    }

    public void DecryptIge(ReadOnlySpan<byte> source, byte[] key, ReadOnlySpan<byte> iv, Span<byte> destination)
    {
        var outputBytes = ArrayPool<byte>.Shared.Rent(source.Length);
        AesIgeEncryptDecrypt(source, key, iv, outputBytes, false);
        outputBytes.AsSpan(0, source.Length).CopyTo(destination);
        ArrayPool<byte>.Shared.Return(outputBytes);
    }

    public void Ctr256Encrypt(Memory<byte> source, byte[] key, byte[] iv, long offset)
    {
        Ctr256Encrypt(source, source, key, iv, offset);
    }

    private void AesIgeEncryptDecrypt(ReadOnlySpan<byte> source, byte[] key, ReadOnlySpan<byte> iv, byte[] destination,
        bool encrypt)
    {
        if (source.Length % 16 != 0)
        {
            throw new ArgumentException("Aes ige input size not divisible by 16");
        }

        var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.Zeros;

        using var cryptor = encrypt ? aes.CreateEncryptor(key, null) : aes.CreateDecryptor(key, null);
        //var ivBytes = iv.AsSpan();
        Span<byte> ivBytes = stackalloc byte[32];
        iv.CopyTo(ivBytes);

        var prevBytes = ivBytes;
        var inputSpan = MemoryMarshal.Cast<byte, long>(source);
        var outputSpan = MemoryMarshal.Cast<byte, long>(destination.AsSpan(0, source.Length));
        var prev = MemoryMarshal.Cast<byte, long>(prevBytes);
        if (!encrypt)
        {
            (prev[2], prev[0]) = (prev[0], prev[2]);
            (prev[3], prev[1]) = (prev[1], prev[3]);
        }

        for (int i = 0, count = source.Length / 8; i < count;)
        {
            outputSpan[i] = inputSpan[i] ^ prev[0];
            outputSpan[i + 1] = inputSpan[i + 1] ^ prev[1];
            cryptor.TransformBlock(destination,
                i * 8,
                16,
                destination,
                i * 8);
            prev[0] = outputSpan[i] ^= prev[2];
            prev[1] = outputSpan[i + 1] ^= prev[3];
            prev[2] = inputSpan[i++];
            prev[3] = inputSpan[i++];
        }
    }

    public void Ctr256Encrypt(Memory<byte> source, Memory<byte> encryptedData, byte[] key, byte[] iv, long offset)
    {
        const int blockSize = 16;

        if (key is not { Length: 32 }) // 256 bits = 32 bytes
        {
            throw new ArgumentException("Key must be 256 bits (32 bytes).", nameof(key));
        }

        if (iv is not { Length: blockSize }) // 16 bytes for AES block size
        {
            throw new ArgumentException($"IV must be {blockSize} bytes.", nameof(iv));
        }

        using var aes = Aes.Create();
        aes.Key = key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;

        using var encryptor = aes.CreateEncryptor();

        var blockOffset = (int)(offset % blockSize);
        var blockIndex = offset / blockSize;
        var totalLength = blockOffset + source.Length;
        var blocks = (totalLength + blockSize - 1) / blockSize;

        var encryptedDataSpan = encryptedData.Span;

        var counterBlock = ArrayPool<byte>.Shared.Rent(blockSize);
        var outputBuffer = ArrayPool<byte>.Shared.Rent(blockSize);

        try
        {
            var dataIndex = 0;
            // 12 bytes of IV + 4 bytes of counter
            var ivSpan = iv.AsSpan()[..12];
            for (var i = 0; i < blocks; i++)
            {
                var counter = (uint)(blockIndex + i);

                ivSpan.CopyTo(counterBlock);
                counterBlock[12] = (byte)(counter >> 24);
                counterBlock[13] = (byte)(counter >> 16);
                counterBlock[14] = (byte)(counter >> 8);
                counterBlock[15] = (byte)counter;

                encryptor.TransformBlock(counterBlock, 0, blockSize, outputBuffer, 0);

                var outputBufferStart = i == 0 ? blockOffset : 0;
                var chunkSize = Math.Min(blockSize - outputBufferStart, encryptedDataSpan.Length - dataIndex);

                for (var j = 0; j < chunkSize; j++)
                {
                    encryptedDataSpan[dataIndex] ^= outputBuffer[outputBufferStart + j];
                    dataIndex++;
                }
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(counterBlock);
            ArrayPool<byte>.Shared.Return(outputBuffer);
        }
    }

    public void CtrEncrypt(ReadOnlySpan<byte> input, Span<byte> destination, byte[] key, byte[] iv,
        ulong offset = 0)
    {
        if (input.Length != destination.Length)
        {
            throw new ArgumentException("Input and output must be the same length.");
        }

        using var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;
        aes.Key = key;
        using var encryptor = aes.CreateEncryptor();

        const int blockSize = 16;
        const int blocksPerBatch = 32;
        const int batchSize = blockSize * blocksPerBatch;

        Span<byte> counter = stackalloc byte[blockSize];
        iv.CopyTo(counter);
        AddToCounter(counter, offset / blockSize);

        var counterBatch = ArrayPool<byte>.Shared.Rent(batchSize);
        var encryptedBytes = ArrayPool<byte>.Shared.Rent(batchSize);
        var encryptedSpan = encryptedBytes.AsSpan();
        var counterBatchSpan = counterBatch.AsSpan();

        var inputOffset = 0;
        var blockOffset = (int)(offset % blockSize);
        var length = input.Length;

        Span<byte> tempCounter = stackalloc byte[blockSize];
        counter.CopyTo(tempCounter);
        try
        {
            while (inputOffset < length)
            {
                var remaining = length - inputOffset;
                var blocksToGen = Math.Min(blocksPerBatch, (remaining + blockOffset + blockSize - 1) / blockSize);
                var bytesToXor = Math.Min(remaining, blocksToGen * blockSize - blockOffset);

                for (var i = 0; i < blocksToGen; i++)
                {
                    tempCounter.CopyTo(counterBatchSpan.Slice(i * blockSize, blockSize));
                    IncrementCounter(tempCounter);
                }

                encryptor.TransformBlock(counterBatch, 0, blocksToGen * blockSize, encryptedBytes, 0);

                var keyStreamSpan = encryptedSpan.Slice(blockOffset, bytesToXor);
                var inputSpan = input.Slice(inputOffset, bytesToXor);
                var outputSpan = destination.Slice(inputOffset, bytesToXor);

                if (Vector.IsHardwareAccelerated && bytesToXor >= Vector<byte>.Count)
                {
                    var simdCount = bytesToXor - bytesToXor % Vector<byte>.Count;
                    for (var i = 0; i < simdCount; i += Vector<byte>.Count)
                    {
                        var vInput = new Vector<byte>(inputSpan[i..]);
                        var vKey = new Vector<byte>(keyStreamSpan[i..]);
                        (vInput ^ vKey).CopyTo(outputSpan[i..]);
                    }

                    for (var i = simdCount; i < bytesToXor; i++)
                    {
                        outputSpan[i] = (byte)(inputSpan[i] ^ keyStreamSpan[i]);
                    }
                }
                else
                {
                    for (var i = 0; i < bytesToXor; i++)
                    {
                        outputSpan[i] = (byte)(inputSpan[i] ^ keyStreamSpan[i]);
                    }
                }

                inputOffset += bytesToXor;
                blockOffset = 0;
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(counterBatch);
            ArrayPool<byte>.Shared.Return(encryptedBytes);
        }
    }

    public void CtrEncrypt(ReadOnlyMemory<byte> input, Memory<byte> destination, byte[] key, byte[] iv, ulong offset = 0)
    {
        CtrEncrypt(input.Span, destination.Span, key, iv, offset);
    }

    private static void IncrementCounter(Span<byte> counter)
    {
        for (var i = counter.Length - 1; i >= 0; i--)
        {
            if (++counter[i] != 0)
            {
                break;
            }
        }
    }

    private static void AddToCounter(Span<byte> counter, ulong value)
    {
        for (var i = 0; i < 8; i++)
        {
            var index = counter.Length - 1 - i;
            value += counter[index];
            counter[index] = (byte)value;
            value >>= 8;
        }
    }
}