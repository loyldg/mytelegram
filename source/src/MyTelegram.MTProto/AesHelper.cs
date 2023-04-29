namespace MyTelegram.MTProto;

public class AesHelper : IAesHelper
{
    public void Ctr128Encrypt(Span<byte> data,
        byte[] key,
        CtrState ctrState)
    {
        var n = ctrState.Number;
        var length = data.Length;
        var iv = ctrState.Iv;
        using var aes = Aes.Create();
        aes.Key = key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;
        var encryptor = aes.CreateEncryptor();
        for (var i = 0; i < data.Length; i++)
        {
            if (n == 0)
            {
                encryptor.TransformBlock(ctrState.Iv,
                    0,
                    ctrState.Iv.Length,
                    ctrState.ECounter,
                    0);
                for (var j = 15; j >= 0; j--)
                {
                    if (++iv[j] != 0)
                    {
                        break;
                    }
                }
            }

            data[i] = (byte)(data[i] ^ ctrState.ECounter[n]);
            n = (n + 1) % 16;
        }

        ctrState.Number = n;
    }

    public void DecryptIge(ReadOnlySpan<byte> input,
        byte[] outputBytes,
        byte[] key,
        byte[] iv)
    {
        AesIgeEncryptDecrypt(input,
            outputBytes,
            key,
            iv,
            false);
    }

    public void EncryptIge(ReadOnlySpan<byte> input,
        byte[] outputBytes,
        byte[] key,
        byte[] iv)
    {
        AesIgeEncryptDecrypt(input,
            outputBytes,
            key,
            iv,
            true);
    }

    private void AesIgeEncryptDecrypt(ReadOnlySpan<byte> input,
        byte[] outputBytes,
        byte[] key,
        byte[] iv,
        bool encrypt)
    {
        if (input.Length % 16 != 0)
        {
            throw new ArgumentException("Aes ige input size not divisible by 16");
        }

        var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.Zeros;
        using var cryptor = encrypt ? aes.CreateEncryptor(key, null) : aes.CreateDecryptor(key, null);
        Span<byte> ivBytes = stackalloc byte[iv.Length];
        iv.CopyTo(ivBytes);
        var prevBytes = ivBytes;
        var inputSpan = MemoryMarshal.Cast<byte, long>(input);
        var outputSpan = MemoryMarshal.Cast<byte, long>(outputBytes);
        var prev = MemoryMarshal.Cast<byte, long>(prevBytes);
        for (int i = 0, count = input.Length / 8; i < count;)
        {
            outputSpan[i] = inputSpan[i] ^ prev[0];
            outputSpan[i + 1] = inputSpan[i + 1] ^ prev[1];
            cryptor.TransformBlock(outputBytes,
                i * 8,
                16,
                outputBytes,
                i * 8);
            prev[0] = outputSpan[i] ^= prev[2];
            prev[1] = outputSpan[i + 1] ^= prev[3];
            prev[2] = inputSpan[i++];
            prev[3] = inputSpan[i++];
        }
    }

    private static void Xor(Span<byte> dest,
        ReadOnlySpan<byte> src)
    {
        for (var i = 0; i < dest.Length; i++)
        {
            dest[i] = (byte)(dest[i] ^ src[i]);
        }
    }

    //public void Ctr128Encrypt(ReadOnlySpan<byte> data,
    //    Span<byte> encryptedData, byte[] key, CtrState ctrState)
    //{
    //    var n = ctrState.Number;
    //    var length = data.Length;
    //    var iv = ctrState.Iv;
    //    using var aes = Aes.Create();
    //    aes.Key = key;
    //    aes.Mode = CipherMode.ECB;
    //    aes.Padding = PaddingMode.None;
    //    var encryptor = aes.CreateEncryptor();
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //        if (n == 0)
    //        {
    //            encryptor.TransformBlock(ctrState.Iv,
    //                0,
    //                ctrState.Iv.Length,
    //                ctrState.ECounter,
    //                0);
    //            for (int j = 15; j >= 0; j--)
    //            {
    //                if (++iv[j] != 0)
    //                {
    //                    break;
    //                }
    //            }
    //        }

    //        encryptedData[i] = (byte)(data[i] ^ ctrState.ECounter[n]);
    //        n = (n + 1) % 16;
    //    }

    //    ctrState.Number = n;
    //}

    //public void EncryptIge(ReadOnlySpan<byte> plainSpan, Span<byte> encryptedData,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv)
    //{
    //    var length = plainSpan.Length;
    //    var restLength = plainSpan.Length % 16;
    //    if (restLength % 16 != 0) length += 16 - restLength;
    //    var aes = Aes.Create();

    //    aes.Key = key.ToArray();
    //    aes.Mode = CipherMode.ECB;
    //    aes.Padding = PaddingMode.None;
    //    var blockSize = aes.BlockSize / 8;
    //    using var encryptor = aes.CreateEncryptor();
    //    var iv1 = iv[..blockSize];
    //    var iv2 = iv[blockSize..];
    //    var cipherTextBlock = ArrayPool<byte>.Shared.Rent(blockSize);
    //    for (var i = 0; i < plainSpan.Length; i += blockSize)
    //    {
    //        var plainTextBlock = ArrayPool<byte>.Shared.Rent(blockSize);
    //        plainSpan.Slice(i, blockSize).CopyTo(plainTextBlock);
    //        Xor(plainTextBlock, iv1);
    //        encryptor.TransformBlock(plainTextBlock,
    //            0,
    //            blockSize,
    //            cipherTextBlock,
    //            0);

    //        ArrayPool<byte>.Shared.Return(plainTextBlock);

    //        Xor(cipherTextBlock, iv2);

    //        iv1 = cipherTextBlock;
    //        iv2 = plainSpan.Slice(i, blockSize);
    //        cipherTextBlock.CopyTo(encryptedData.Slice(i, blockSize));
    //    }
    //    ArrayPool<byte>.Shared.Return(cipherTextBlock, true);
    //}

    //public void DecryptIge(ReadOnlySpan<byte> encryptedSpan,
    //    Span<byte> decryptedData,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv)
    //{
    //    using var aes = Aes.Create();
    //    aes.Mode = CipherMode.ECB;
    //    aes.Padding = PaddingMode.None;
    //    aes.Key = key.ToArray();

    //    var blockSize = aes.BlockSize / 8;

    //    using var decryptor = aes.CreateDecryptor();

    //    var iv1 = iv[..blockSize];
    //    var iv2 = iv[blockSize..];

    //    var plaintextBlock = ArrayPool<byte>.Shared.Rent(blockSize);

    //    for (var i = 0; i < encryptedSpan.Length; i += blockSize)
    //    {
    //        var cipherTextBlock = ArrayPool<byte>.Shared.Rent(blockSize);
    //        encryptedSpan.Slice(i, blockSize).CopyTo(cipherTextBlock);
    //        Xor(cipherTextBlock, iv2);

    //        decryptor.TransformBlock(cipherTextBlock,
    //            0,
    //            blockSize,
    //            plaintextBlock,
    //            0);
    //        ArrayPool<byte>.Shared.Return(cipherTextBlock);

    //        Xor(plaintextBlock, iv1);
    //        iv1 = encryptedSpan[i..(i + blockSize)];
    //        iv2 = plaintextBlock;

    //        plaintextBlock.CopyTo(decryptedData[i..]);
    //    }
    //    ArrayPool<byte>.Shared.Return(plaintextBlock);
    //}

    //private static void Ctr128Inc(Span<byte> counter)
    //{
    //    var n = 16;
    //    var c = 1;
    //    do
    //    {
    //        --n;
    //        c += counter[n];
    //        counter[n] = (byte)c;
    //        c >>= 8;
    //    } while (n != 0);
    //}
}
