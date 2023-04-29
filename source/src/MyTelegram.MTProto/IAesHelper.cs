namespace MyTelegram.MTProto;

public interface IAesHelper
{
    void Ctr128Encrypt(Span<byte> data,
        byte[] key,
        CtrState ctrState);

    void DecryptIge(ReadOnlySpan<byte> input,
        byte[] outputBytes,
        byte[] key,
        byte[] iv);

    void EncryptIge(ReadOnlySpan<byte> input,
        byte[] outputBytes,
        byte[] key,
        byte[] iv);
    //void Ctr128Encrypt(ReadOnlySpan<byte> data,
    //    Span<byte> encryptedData,
    //    byte[] key,
    //    CtrState ctrState);

    //void EncryptIge(ReadOnlySpan<byte> plainSpan,
    //    Span<byte> encryptedData,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv);

    //void DecryptIge(ReadOnlySpan<byte> encryptedSpan,
    //    Span<byte> decryptedData,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv);

    //Memory<byte> Ctr128Encrypt(ReadOnlyMemory<byte> span,
    //    byte[] key,
    //    CtrState ctrState);
    //Memory<byte> Ctr128Encrypt(ReadOnlySpan<byte> span,
    //    byte[] key,
    //    CtrState ctrState);

    //Memory<byte> EncryptIge(ReadOnlyMemory<byte> plainSpan,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv);

    //Memory<byte> DecryptIge(ReadOnlyMemory<byte> encryptedSpan,
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> iv);
}
