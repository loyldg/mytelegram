// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

internal sealed class ToggleParticipantsHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleParticipantsHidden, MyTelegram.Schema.IUpdates>,
    Channels.IToggleParticipantsHiddenHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleParticipantsHidden obj)
    {
        throw new NotImplementedException();
    }
}
