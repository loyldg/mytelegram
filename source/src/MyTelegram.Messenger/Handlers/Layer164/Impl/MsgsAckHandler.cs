// ReSharper disable All
namespace MyTelegram.Handlers.Impl;

internal sealed class MsgsAckHandler : BaseObjectHandler<TMsgsAck, IObject>, IMsgsAckHandler, IProcessedHandler
{
    private readonly ILogger<MsgsAckHandler> _logger;

    public MsgsAckHandler(ILogger<MsgsAckHandler> logger)
    {
        _logger = logger;
    }

    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        TMsgsAck obj)
    {
        _logger.LogInformation("Receive acks from userId {UserId}:@{MsgIds}", input.UserId, obj.MsgIds);
        return Task.FromResult<IObject>(null!);
        //IObject r = new TMsgsAck
        //{
        //    MsgIds = new TVector<long>()
        //};

        //return Task.FromResult(r);
    }
}