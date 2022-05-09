using MyTelegram.Domain.Aggregates.PushDevice;
using MyTelegram.Domain.Commands.PushDevice;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UnRegisterDeviceHandler : RpcResultObjectHandler<RequestUnregisterDevice, IBool>,
    IUnregisterDeviceHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;

    public UnRegisterDeviceHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUnregisterDevice obj)
    {
        var command = new UnRegisterDeviceCommand(PushDeviceId.Create(obj.Token),
            input.ReqMsgId,
            obj.TokenType,
            obj.Token,
            obj.OtherUids.ToList());
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return new TBoolTrue();
    }
}
