//using MyTelegram.Domain.Aggregates.AuthKey;
//using MyTelegram.Domain.Commands.AuthKey;

namespace MyTelegram.MessengerServer.Handlers.Impl;

// ReSharper disable once UnusedMember.Global
public class DestroyAuthKeyHandler : RpcResultObjectHandler<RequestDestroyAuthKey, IDestroyAuthKeyRes>,
    IDestroyAuthKeyHandler, IProcessedHandler
{
    protected override Task<IDestroyAuthKeyRes> HandleCoreAsync(IRequestInput input,
        RequestDestroyAuthKey obj)
    {
        throw new NotImplementedException();
        //var command = new DestroyAuthKeyCommand(AuthKeyId.Create(input.PermAuthKeyId), input.ReqMsgId);
        //await _commandBus.PublishAsync(command, CancellationToken.None);

        //return new TDestroyAuthKeyOk();
    }
}