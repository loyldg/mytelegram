namespace MyTelegram.Domain;

public interface IDataEncryptionHelper
{
    int Encrypt(int keyId, ReadOnlySpan<byte> key, string plainText, Span<byte> cipherTextWithNonceAndTag);
    int Decrypt(ReadOnlySpan<byte> key, ReadOnlySpan<byte> cipherText, Span<byte> plainText);
    byte[] Encrypt(int keyId, ReadOnlySpan<byte> masterKey, long ownerPeerId, string plainText);
    int Encrypt(int keyId, ReadOnlySpan<byte> masterKey, long ownerPeerId, string plainText,Span<byte> cipherTextWithNonceAndTag);

    int Decrypt(ReadOnlySpan<byte> masterKey, long ownerPeerId, ReadOnlySpan<byte> cipherSpan,
        Span<byte> plainSpan);
}

public interface IDataEncryptionHelper2
{
    int Encrypt(long ownerPeerId, string plainText, Span<byte> encryptedData);
    string DecryptMessage(long ownerPeerId, ReadOnlyMemory<byte>? encryptedData);
    int DecryptMessage(long ownerPeerId, ReadOnlyMemory<byte>? encryptedData, Span<byte> decryptedData);
}