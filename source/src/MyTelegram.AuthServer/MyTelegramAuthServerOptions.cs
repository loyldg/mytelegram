namespace MyTelegram.AuthServer;

public class MyTelegramAuthServerOptions
{
    public string PrivateKeyFilePath { get; set; } = string.Empty;
    public int AuthStep1DelayMs { get; set; } = 50;

    /// <summary>
    /// Refuse to start when the private key shipped with the open-source project is used.
    /// The default key is public, so anyone can decrypt req_DH_params and impersonate the server.
    /// </summary>
    public bool RejectDefaultPrivateKey { get; set; }

    /// <summary>
    /// Generate a random pq (product of two random 31-bit primes) for every req_pq / req_pq_multi.
    /// A fixed, publicly known pq gives no proof of work and lets clients flood req_DH_params for free.
    /// </summary>
    public bool UseRandomPq { get; set; } = true;

    public HandshakeRateLimitOptions HandshakeRateLimit { get; set; } = new();
}

/// <summary>
/// Per client IP token bucket for the handshake requests (req_pq, req_pq_multi, req_DH_params, set_client_DH_params).
/// Every request consumes one token. A full handshake uses 3 tokens.
/// </summary>
public class HandshakeRateLimitOptions
{
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Maximum burst size.
    /// </summary>
    public int Capacity { get; set; } = 120;

    /// <summary>
    /// Tokens added per minute. Keep it generous: many users may share one IP (NAT/CGNAT).
    /// </summary>
    public int TokensPerMinute { get; set; } = 120;
}