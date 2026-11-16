namespace MyTelegram.MTProto;

/// <summary>
/// Thrown when the peer violates the MTProto transport framing (unknown protocol, invalid length,
/// bad CRC, unexpected sequence number and so on). The connection must be closed, because the byte
/// stream (and the obfuscation counter) can no longer be resynchronized.
/// </summary>
public sealed class MtpTransportException(string message) : Exception(message);
