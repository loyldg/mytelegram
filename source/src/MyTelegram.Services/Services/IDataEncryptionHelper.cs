//namespace MyTelegram.Services.Services;

//public interface IDataEncryptionHelper
//{
//    int Encrypt(int keyId, ReadOnlySpan<byte> key, string plainText, Span<byte> cipherTextWithNonceAndTag);
//    int Decrypt(ReadOnlySpan<byte> key, ReadOnlySpan<byte> cipherText, Span<byte> plainText);
//    byte[] Encrypt(int keyId, ReadOnlySpan<byte> masterKey, long ownerPeerId, string plainText);

//    int Decrypt(ReadOnlySpan<byte> masterKey, long ownerPeerId, ReadOnlySpan<byte> cipherSpan,
//        Span<byte> plainSpan);
//}