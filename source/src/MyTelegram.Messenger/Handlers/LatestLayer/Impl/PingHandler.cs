// ReSharper disable All

namespace MyTelegram.Handlers.Impl;

internal sealed class PingHandler : BaseObjectHandler<RequestPing, IPong>,
    IPingHandler
{
    protected override Task<IPong> HandleCoreAsync(IRequestInput input,
        RequestPing obj)
    {
        var r = new TPong { MsgId = input.ReqMsgId, PingId = obj.PingId };
        return Task.FromResult<IPong>(r);
    }
}
