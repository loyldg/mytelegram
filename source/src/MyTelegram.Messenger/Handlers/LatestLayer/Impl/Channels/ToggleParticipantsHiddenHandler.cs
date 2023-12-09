// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Hide or display the participants list in a <a href="https://corefork.telegram.org/api/channel">supergroup</a>.The supergroup must have at least <code>hidden_members_group_size_min</code> participants in order to use this method, as specified by the <a href="https://corefork.telegram.org/api/config#client-configuration">client configuration parameters »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PARTICIPANTS_TOO_FEW Not enough participants.
/// See <a href="https://corefork.telegram.org/method/channels.toggleParticipantsHidden" />
///</summary>
internal sealed class ToggleParticipantsHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleParticipantsHidden, MyTelegram.Schema.IUpdates>,
    Channels.IToggleParticipantsHiddenHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleParticipantsHidden obj)
    {
        throw new NotImplementedException();
    }
}
