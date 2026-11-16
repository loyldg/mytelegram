namespace MyTelegram.Core;

/// <summary>
/// Published by the session server when an encrypted payload that requested a transport-level quick ACK
/// has been received, decrypted and accepted for processing. The gateway sends the token back on the
/// connection identified by <paramref name="ConnectionId"/>.
/// https://core.telegram.org/mtproto/mtproto-transports#quick-ack
/// </summary>
/// <param name="AuthKeyId">Auth key id of the payload.</param>
/// <param name="ConnectionId">Gateway connection that received the payload.</param>
/// <param name="QuickAckToken">First 32 bits of msg_key_large (little endian) with the most significant bit set.</param>
public record QuickAckEvent(
    long AuthKeyId,
    string ConnectionId,
    uint QuickAckToken);
