// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

internal sealed class ToggleParticipantsHiddenHandler :
    RpcResultObjectHandler<RequestToggleParticipantsHidden, Schema.IUpdates>,
    Channels.IToggleParticipantsHiddenHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleParticipantsHidden obj)
    {
        throw new NotImplementedException();
    }
}