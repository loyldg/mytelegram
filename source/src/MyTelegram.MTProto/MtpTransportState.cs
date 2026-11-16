namespace MyTelegram.MTProto;

/// <summary>
/// Per-connection MTProto transport state.
/// Receive-side members are only used by the connection's receive loop. <see cref="OutboundSeqNo"/> is
/// changed while a frame is encoded, so encoding must be serialized per connection (together with the
/// obfuscation counter <see cref="IClientData.ReceiveCount"/>).
/// </summary>
public sealed class MtpTransportState
{
    /// <summary>
    /// True when the header of the next frame has already been decoded but its body is not complete yet.
    /// The header is cached so that it is not decoded again on every partial read.
    /// The obfuscation counter is only advanced when the whole frame is consumed.
    /// </summary>
    public bool HasPendingHeader { get; private set; }

    public int PendingHeaderLength { get; private set; }

    /// <summary>
    /// Number of bytes after the header: payload, plus padding (padded intermediate) or CRC (full).
    /// </summary>
    public int PendingBodyLength { get; private set; }

    public bool PendingQuickAckRequested { get; private set; }

    /// <summary>
    /// Raw "len" field of a pending full transport frame (it is part of the CRC input).
    /// </summary>
    public int PendingFullLengthField { get; private set; }

    /// <summary>
    /// Raw "seq" field of a pending full transport frame.
    /// </summary>
    public int PendingFullSeqNo { get; private set; }

    /// <summary>
    /// Full transport only: the sequence number expected in the next frame received from the client.
    /// It is a per-TCP-connection counter starting at 0, unrelated to the MTProto seq_no.
    /// </summary>
    public int InboundSeqNo { get; set; }

    /// <summary>
    /// Full transport only: the sequence number of the next frame sent to the client (starts at 0).
    /// </summary>
    public int OutboundSeqNo { get; set; }

    public MtpQuickAckTracker QuickAck { get; } = new();

    internal void SetPendingHeader(int headerLength,
        int bodyLength,
        bool quickAckRequested,
        int fullLengthField,
        int fullSeqNo)
    {
        HasPendingHeader = true;
        PendingHeaderLength = headerLength;
        PendingBodyLength = bodyLength;
        PendingQuickAckRequested = quickAckRequested;
        PendingFullLengthField = fullLengthField;
        PendingFullSeqNo = fullSeqNo;
    }

    internal void ClearPendingHeader()
    {
        HasPendingHeader = false;
        PendingHeaderLength = 0;
        PendingBodyLength = 0;
        PendingQuickAckRequested = false;
        PendingFullLengthField = 0;
        PendingFullSeqNo = 0;
    }
}

public enum QuickAckAcceptResult
{
    Accepted,

    /// <summary>
    /// The token does not have the most significant bit set, so it cannot be a quick ACK token.
    /// </summary>
    Invalid,

    /// <summary>
    /// The same token was sent recently (for example, the EventBus delivered the event twice).
    /// </summary>
    Duplicate,

    /// <summary>
    /// The connection has no outstanding quick ACK request (unexpected token, or the requests were
    /// discarded because the connection is closing).
    /// </summary>
    NotRequested
}

/// <summary>
/// Tracks quick ACK requests of one connection.
/// The gateway cannot compute the tokens itself (the auth key is only known by the session server),
/// so it counts outstanding requests and filters invalid or duplicated tokens coming back.
/// </summary>
public sealed class MtpQuickAckTracker
{
    /// <summary>
    /// Upper bound of outstanding requests. Requests whose payload is rejected by the session server
    /// are never answered, so the counter must be bounded.
    /// </summary>
    public const int MaxPendingRequests = 4096;

    public const uint TokenFlag = 0x80000000;

    private const int RecentTokenCapacity = 64;
    private readonly Lock _lock = new();
    private readonly uint[] _recentTokens = new uint[RecentTokenCapacity];
    private int _recentTokenCount;
    private int _recentTokenIndex;
    private int _pendingCount;

    public int PendingCount
    {
        get
        {
            lock (_lock)
            {
                return _pendingCount;
            }
        }
    }

    /// <summary>
    /// Registers a quick ACK request. Returns false when too many requests are outstanding.
    /// </summary>
    public bool OnRequested()
    {
        lock (_lock)
        {
            if (_pendingCount >= MaxPendingRequests)
            {
                return false;
            }

            _pendingCount++;
            return true;
        }
    }

    public QuickAckAcceptResult TryAccept(uint token)
    {
        if ((token & TokenFlag) == 0)
        {
            return QuickAckAcceptResult.Invalid;
        }

        lock (_lock)
        {
            for (var i = 0; i < _recentTokenCount; i++)
            {
                if (_recentTokens[i] == token)
                {
                    return QuickAckAcceptResult.Duplicate;
                }
            }

            if (_pendingCount == 0)
            {
                return QuickAckAcceptResult.NotRequested;
            }

            _pendingCount--;
            _recentTokens[_recentTokenIndex] = token;
            _recentTokenIndex = (_recentTokenIndex + 1) % RecentTokenCapacity;
            if (_recentTokenCount < RecentTokenCapacity)
            {
                _recentTokenCount++;
            }

            return QuickAckAcceptResult.Accepted;
        }
    }

    /// <summary>
    /// Discards all outstanding requests (used when the connection is closed) and returns their number.
    /// </summary>
    public int Reset()
    {
        lock (_lock)
        {
            var count = _pendingCount;
            _pendingCount = 0;
            return count;
        }
    }
}
