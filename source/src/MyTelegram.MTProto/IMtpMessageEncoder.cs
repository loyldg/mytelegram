namespace MyTelegram.MTProto;

public interface IMtpMessageEncoder
{
    int Encode(IClientData d,
        EncryptedMessageResponse m,
        Span<byte> encodedBytes);

    int Encode(IClientData d,
        UnencryptedMessageResponse m,
        Span<byte> encodedBytes);

    /// <summary>
    /// Encodes a quick ACK token using the encoding of the connection's transport.
    /// Returns 0 when the transport does not support quick ACK (nothing must be sent).
    /// </summary>
    int EncodeQuickAck(IClientData d,
        uint quickAckToken,
        Span<byte> encodedBytes);
}
