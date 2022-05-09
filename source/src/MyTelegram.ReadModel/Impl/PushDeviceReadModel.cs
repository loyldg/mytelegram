// ReSharper Disable All

namespace MyTelegram.ReadModel.Impl;

public class PushDeviceReadModel : IPushDeviceReadModel,
    IAmReadModelFor<PushDeviceAggregate, PushDeviceId, PushDeviceRegisteredEvent>,
    IAmReadModelFor<PushDeviceAggregate, PushDeviceId, PushDeviceUnRegisteredEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PushDeviceAggregate, PushDeviceId, PushDeviceRegisteredEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        UserId = domainEvent.AggregateEvent.UserId;
        AuthKeyId = domainEvent.AggregateEvent.AuthKeyId;

        TokenType = domainEvent.AggregateEvent.TokenType;
        Token = domainEvent.AggregateEvent.Token;
        NoMuted = domainEvent.AggregateEvent.NoMuted;
        AppSandbox = domainEvent.AggregateEvent.AppSandbox;
        Secret = domainEvent.AggregateEvent.Secret;
        OtherUids = domainEvent.AggregateEvent.OtherUids;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PushDeviceAggregate, PushDeviceId, PushDeviceUnRegisteredEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;

    public virtual long UserId { get; private set; }
    public virtual long AuthKeyId { get; private set; }

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
    public virtual int TokenType { get; private set; }

    /// <summary>
    ///     Device token
    /// </summary>
    public virtual string Token { get; private set; } = null!;

    /// <summary>
    ///     Avoid receiving (silent and invisible background) notifications. Useful to save battery.
    /// </summary>
    public virtual bool NoMuted { get; private set; }

    /// <summary>
    ///     If (boolTrue) is transmitted, a sandbox-certificate will be used during transmission.
    /// </summary>
    public virtual bool AppSandbox { get; private set; }

    /// <summary>
    ///     For FCM and APNS VoIP, optional encryption key used to encrypt push notifications
    /// </summary>
    public virtual byte[]? Secret { get; private set; }

    /// <summary>
    ///     List of user identifiers of other users currently using the client
    /// </summary>
    public virtual IReadOnlyList<long>? OtherUids { get; protected set; }
}
