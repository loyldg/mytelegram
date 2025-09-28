using MyTelegram.Schema;
using MyTelegram.Schema.Serializer;
using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace MyTelegram.Core;

public class MyRsaHelper : IMyRsaHelper, ISingletonDependency
{
    // https://stackoverflow.com/questions/15702718/public-key-encryption-with-rsacryptoserviceprovider
    private MyRsaParameter? _myRsaParameter;
    private readonly BytesSerializer _bytesSerializer = new();

    public byte[] Decrypt(ReadOnlySpan<byte> encryptedSpan,
        string privateKey)
    {
        InitIfNeeded(privateKey);
        return RsaOperation(encryptedSpan, _myRsaParameter!.PrivateExponent, _myRsaParameter.Modulus);
    }

    public long GetFingerprintFromPrivateKey(string privateKeyWithFormat)
    {
        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKeyWithFormat.RemoveRsaKeyFormat()), out _);
        var p = rsa.ExportParameters(false);
        return GetFingerprint(p);
    }

    private void InitIfNeeded(string privateKey)
    {
        if (_myRsaParameter == null)
        {
            _myRsaParameter = new MyRsaParameter();
            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey.RemoveRsaKeyFormat()), out _);
            var p = rsa.ExportParameters(true);
            _myRsaParameter.Modulus = new BigInteger(p.Modulus, true, true);
            _myRsaParameter.PrivateExponent = new BigInteger(p.D, true, true);
            _myRsaParameter.PublicExponent = new BigInteger(p.Exponent, true, true);
        }
    }
    private long GetFingerprint(RSAParameters rsaParameters)
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        _bytesSerializer.Serialize(rsaParameters.Modulus!, writer);
        _bytesSerializer.Serialize(rsaParameters.Exponent!, writer);

        Span<byte> hash = stackalloc byte[20];
        SHA1.TryHashData(writer.WrittenSpan, hash, out _);

        return BinaryPrimitives.ReadInt64LittleEndian(hash.Slice(12, 8));
    }
    private byte[] RsaOperation(ReadOnlySpan<byte> data,
        BigInteger exponent,
        BigInteger modulus)
    {
        var bData = new BigInteger(data, true, true);

        return BigInteger
            .ModPow(bData, exponent, modulus)
            .ToByteArray(true, true);
    }
}