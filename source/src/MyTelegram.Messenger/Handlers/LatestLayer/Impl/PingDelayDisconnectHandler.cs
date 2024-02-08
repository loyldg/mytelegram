// ReSharper disable All

namespace MyTelegram.Handlers.Impl;

internal sealed class PingDelayDisconnectHandler : BaseObjectHandler<RequestPingDelayDisconnect, IPong>,
    IPingDelayDisconnectHandler
{
    protected override Task<IPong> HandleCoreAsync(IRequestInput input,
        RequestPingDelayDisconnect obj)
    {
        var r = new TPong { MsgId = input.ReqMsgId, PingId = obj.PingId };
        return Task.FromResult<IPong>(r);
    }
}
