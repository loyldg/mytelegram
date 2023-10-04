namespace MyTelegram.MessengerServer.Handlers.Impl;

public class PingHandler : BaseObjectHandler<RequestPing, IPong>,
    IPingHandler, IProcessedHandler
{
    protected override Task<IPong> HandleCoreAsync(IRequestInput input,
        RequestPing obj)
    {
        var r = new TPong { MsgId = input.ReqMsgId, PingId = obj.PingId };
        return Task.FromResult<IPong>(r);
    }
}