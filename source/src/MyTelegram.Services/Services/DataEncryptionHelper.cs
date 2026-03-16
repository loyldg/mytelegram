using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace MyTelegram.Services.Services;

public class DataEncryptionHelper(IAesHelper aesHelper) : IDataEncryptionHelper, ITransientDependency
{
    private static readonly byte[] KeyTypeBytes = System.Text.Encoding.UTF8.GetBytes("MyTelegram_Message");
    public int Encrypt(int keyId, ReadOnlySpan<byte> key, string plainText, Span<byte> cipherTextWithNonceAndTag)
    {
        var keyBytes = cipherTextWithNonceAndTag.Slice(0, 4);
        BinaryPrimitives.WriteInt32LittleEndian(keyBytes, keyId);
        var nonce = cipherTextWithNonceAndTag.Slice(4, 12);
        var count = Encoding.UTF8.GetBytes(plainText, cipherTextWithNonceAndTag.Slice(16));
        var plainSpan = cipherTextWithNonceAndTag.Slice(16, count);
        var tag = cipherTextWithNonceAndTag.Slice(16 + count, 16);
        aesHelper.EncryptGcm(plainSpan, plainSpan, key, nonce, tag);

        return 4 + 12 + count + 16;
    }

    public int Decrypt(ReadOnlySpan<byte> key, ReadOnlySpan<byte> cipherText, Span<byte> plainText)
    {
        var nonce = cipherText.Slice(0, 12);
        var dataSpan = cipherText.Slice(12, cipherText.Length - 16 - 12);
        var tag = cipherText.Slice(cipherText.Length - 16, 16);
        plainText = plainText.Slice(0, dataSpan.Length);
        aesHelper.DecryptGcm(plainText, dataSpan, key, nonce, tag);

        return cipherText.Length - 12 - 16;
    }

    public byte[] Encrypt(int keyId, ReadOnlySpan<byte> masterKey, long ownerPeerId, string plainText)
    {
        // 4:keyId 12:nonce 16:tag messageKey:32 8:salt
        var length = 4 + plainText.Length * 4 + 12 + 16 + 32 + 8;
        var tempBytes = ArrayPool<byte>.Shared.Rent(length);

        try
        {
            //Encrypt: plainText.Length=1,count=1,cipherTextWithNonceAndTag.length=32,16+count=17
            var span = tempBytes.AsSpan();
            var cipherSpan = span.Slice(0, length - 40);
            var messageKeyAndSalt = span.Slice(length - 40, 40);
            var messageKey = messageKeyAndSalt.Slice(0, 32);
            var salt = messageKeyAndSalt.Slice(32, 8);
            BinaryPrimitives.WriteInt64LittleEndian(salt, ownerPeerId);

            HKDF.DeriveKey(HashAlgorithmName.SHA256, masterKey, messageKey, salt, KeyTypeBytes);
            var count = Encrypt(keyId, messageKey, plainText, cipherSpan);

            var encryptedData = cipherSpan.Slice(0, count).ToArray();

            return encryptedData;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }
    }

    public int Decrypt(ReadOnlySpan<byte> masterKey, long ownerPeerId, ReadOnlySpan<byte> cipherSpan,
        Span<byte> plainSpan)
    {
        var tempBytes = ArrayPool<byte>.Shared.Rent(40);
        try
        {
            var messageKeyAndSalt = tempBytes.AsSpan();
            var messageKey = messageKeyAndSalt.Slice(0, 32);
            var salt = messageKeyAndSalt.Slice(32, 8);
            BinaryPrimitives.WriteInt64LittleEndian(salt, ownerPeerId);

            HKDF.DeriveKey(HashAlgorithmName.SHA256, masterKey, messageKey, salt, KeyTypeBytes);

            return Decrypt(messageKey, cipherSpan, plainSpan);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }
    }
}