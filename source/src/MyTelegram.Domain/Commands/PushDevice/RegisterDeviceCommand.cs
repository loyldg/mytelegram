namespace MyTelegram.Domain.Commands.PushDevice;

public class RegisterDeviceCommand : RequestCommand2<PushDeviceAggregate, PushDeviceId, IExecutionResult>
{
    public RegisterDeviceCommand(PushDeviceId aggregateId,
        RequestInfo requestInfo,
        long userId,
        long authKeyId,
        int tokenType,
        string token,
        bool noMuted,
        bool appSandbox,
        byte[]? secret,
        IReadOnlyList<long>? otherUids
    ) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        AuthKeyId = authKeyId;
        TokenType = tokenType;
        Token = token;
        NoMuted = noMuted;
        AppSandbox = appSandbox;
        Secret = secret;
        OtherUids = otherUids;
    }

    /// <summary>
    ///     If (boolTrue) is transmitted, a sandbox-certificate will be used during transmission.
    /// </summary>
    public bool AppSandbox { get; }

    public long AuthKeyId { get; }

    /// <summary>
    ///     Avoid receiving (silent and invisible background) notifications. Useful to save battery.
    /// </summary>
    public bool NoMuted { get; }

    /// <summary>
    ///     List of user identifiers of other users currently using the client
    /// </summary>
    public IReadOnlyList<long>? OtherUids { get; }

    /// <summary>
    ///     For FCM and APNS VoIP, optional encryption key used to encrypt push notifications
    /// </summary>
    public byte[]? Secret { get; }

    /// <summary>
    ///     Device token
    /// </summary>
    public string Token { get; }

    public int TokenType { get; }

    // Device token type.
    //      Possible values:
    //  1 - APNS (device token for apple push)
    //      2 - FCM (firebase token for google firebase)
    //      3 - MPNS (channel URI for microsoft push)
    //      4 - Simple push (endpoint for firefox's simple push API)
    //  5 - Ubuntu phone (token for ubuntu push)
    //      6 - Blackberry (token for blackberry push)
    //      7 - Unused
    //  8 - WNS (windows push)
    //      9 - APNS VoIP (token for apple push VoIP)
    //      10 - Web push (web push, see below)
    //      11 - MPNS VoIP (token for microsoft push VoIP)
    //      12 - Tizen (token for tizen push)
    // 
    //  For 10 web push, the token must be a JSON-encoded object containing the keys described in PUSH updates
    public long UserId { get; }
}
