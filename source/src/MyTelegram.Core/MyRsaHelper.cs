using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace MyTelegram.Core;

public class MyRsaHelper(IHashHelper hashHelper) : IMyRsaHelper, ISingletonDependency
{
    // https://stackoverflow.com/questions/15702718/public-key-encryption-with-rsacryptoserviceprovider
    private MyRsaParameter? _myRsaParameter;

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

    //public long GetFingerprint(string publicKeyWithFormat)
    //{
    //    var rsa = RSA.Create();
    //    rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKeyWithFormat.RemoveRsaKeyFormat()), out _);
    //    var p = rsa.ExportParameters(false);
    //    return GetFingerprint(p);
    //}


    private long GetFingerprint(RSAParameters rsaParameters)
    {
        if (rsaParameters.Modulus == null)
        {
            throw new InvalidOperationException("Modulus is null");
        }

        if (rsaParameters.Exponent == null)
        {
            throw new InvalidOperationException("Exponent is null");
        }

        var memory = new MemoryStream();
        var bw = new BinaryWriter(memory);
        Serialize(rsaParameters.Modulus, bw);
        Serialize(rsaParameters.Exponent, bw);
        var data = memory.ToArray();

        var hash = hashHelper.Sha1(data);

        return BinaryPrimitives.ReadInt64LittleEndian(hash.AsSpan(hash.Length - 8));
    }

    private static void Serialize(byte[] value,
        BinaryWriter writer)
    {
        int padding;
        if (value.Length < 254)
        {
            padding = (value.Length + 1) % 4;
            writer.Write((byte)value.Length);
            writer.Write(value);
        }
        else
        {
            padding = value.Length % 4;
            writer.Write((byte)254);
            writer.Write((byte)value.Length);
            writer.Write((byte)(value.Length >> 8));
            writer.Write((byte)(value.Length >> 16));
            writer.Write(value);
        }

        if (padding != 0)
        {
            padding = 4 - padding;
        }

        for (var i = 0; i < padding; i++)
        {
            writer.Write((byte)0);
        }
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