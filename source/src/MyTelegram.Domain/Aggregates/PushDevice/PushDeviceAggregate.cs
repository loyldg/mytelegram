namespace MyTelegram.Domain.Aggregates.PushDevice;

public class PushDeviceState : AggregateState<PushDeviceAggregate, PushDeviceId, PushDeviceState>,
    IApply<PushDeviceRegisteredEvent>,
    IApply<PushDeviceUnRegisteredEvent>
{
    public void Apply(PushDeviceRegisteredEvent aggregateEvent)
    {
    }

    public void Apply(PushDeviceUnRegisteredEvent aggregateEvent)
    {
    }
}

[EnableAutoGeneration]
public class PushDeviceAggregate : AggregateRoot<PushDeviceAggregate, PushDeviceId>
{
    private readonly PushDeviceState _state = new();
    public PushDeviceAggregate(PushDeviceId id) : base(id)
    {
        Register(_state);
    }

    [DoNotInheritRequestCommand]
    public void RegisterDevice(RequestInfo requestInfo,
        long userId,
        long permAuthKeyId,
        int tokenType,
        string token,
        bool noMuted,
        bool appSandbox,
        byte[]? secret,
        IReadOnlyList<long>? otherUids)
    {
        Emit(new PushDeviceRegisteredEvent(requestInfo,
            userId,
            permAuthKeyId,
            tokenType,
            token,
            noMuted,
            appSandbox,
            secret,
            otherUids));
    }

    public void UnRegisterDevice(RequestInfo requestInfo,
        int tokenType,
        string token,
        IReadOnlyList<long> otherUids)
    {
        Specs.AggregateIsCreated.ThrowFirstDomainErrorIfNotSatisfied(this);
        Emit(new PushDeviceUnRegisteredEvent(requestInfo, tokenType, token, otherUids));
    }
}
