// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class DeleteParticipantHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteParticipantHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Channels.IDeleteParticipantHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteParticipantHistory obj)
    {
        throw new NotImplementedException();
    }
}
