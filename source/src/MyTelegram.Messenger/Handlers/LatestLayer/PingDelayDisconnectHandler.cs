namespace MyTelegram.Messenger.Handlers;

internal sealed class PingDelayDisconnectHandler : BaseObjectHandler<RequestPingDelayDisconnect, IPong>
{
    protected override Task<IPong> HandleCoreAsync(IRequestInput input,
        RequestPingDelayDisconnect obj)
    {
        var r = new TPong { MsgId = input.ReqMsgId, PingId = obj.PingId };
        return Task.FromResult<IPong>(r);
    }
}
