namespace MyTelegram.MTProto;

public interface IMtpMessageParser
{
    void ProcessFirstUnencryptedPacket(ref ReadOnlySequence<byte> data,
        IClientData d);

    bool TryParse(ref ReadOnlySequence<byte> data,
        IClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? message);
}
