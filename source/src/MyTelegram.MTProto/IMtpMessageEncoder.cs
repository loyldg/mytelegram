namespace MyTelegram.MTProto;

public interface IMtpMessageEncoder
{
    int Encode(IClientData d,
        EncryptedMessageResponse m,
        Span<byte> encodedBytes);

    int Encode(IClientData d,
        UnencryptedMessageResponse m,
        Span<byte> encodedBytes);
}
