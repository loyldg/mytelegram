using MyTelegram.Domain.Aggregates.PushDevice;
using MyTelegram.Domain.Commands.PushDevice;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;
using MyTelegram.Schema.LayerN;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class RegisterDeviceHandler : RpcResultObjectHandler<RequestRegisterDevice, IBool>,
    IRegisterDeviceHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;

    public RegisterDeviceHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestRegisterDevice obj)
    {
        var command = new RegisterDeviceCommand(PushDeviceId.Create(obj.Token),
            input.ReqMsgId,
            input.UserId,
            input.AuthKeyId,
            obj.TokenType,
            obj.Token,
            obj.NoMuted,
            obj.AppSandbox,
            obj.Secret,
            obj.OtherUids.ToList());
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return new TBoolTrue();
    }
}

public class RegisterDeviceHandlerLayerN : RpcResultObjectHandler<RequestRegisterDeviceLayerN, IBool>,
    IRegisterDeviceHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;

    public RegisterDeviceHandlerLayerN(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestRegisterDeviceLayerN obj)
    {
        var command = new RegisterDeviceCommand(PushDeviceId.Create(obj.Token),
            input.ReqMsgId,
            input.UserId,
            input.AuthKeyId,
            obj.TokenType,
            obj.Token,
            false,
            false,
            null,
            null);
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return new TBoolTrue();
    }
}