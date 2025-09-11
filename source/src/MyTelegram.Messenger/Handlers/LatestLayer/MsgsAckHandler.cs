namespace MyTelegram.Messenger.Handlers;

internal sealed class MsgsAckHandler : BaseObjectHandler<TMsgsAck, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        TMsgsAck obj)
    {
        return Task.FromResult<IObject>(null!);
    }
}