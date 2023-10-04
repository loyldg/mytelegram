using MyTelegram.Domain.Commands.QrCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class AcceptLoginTokenHandler : RpcResultObjectHandler<RequestAcceptLoginToken, IAuthorization>,
    IAcceptLoginTokenHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public AcceptLoginTokenHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestAcceptLoginToken obj)
    {
        var command = new AcceptLoginTokenCommand(QrCodeId.Create(BitConverter.ToString(obj.Token)),
            input.ReqMsgId,
            input.UserId,
            obj.Token);
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}