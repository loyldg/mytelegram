// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Start screen sharing in a call
/// <para>Possible errors</para>
/// Code Type Description
/// 403 PARTICIPANT_JOIN_MISSING Trying to enable a presentation, when the user hasn't joined the Video Chat with <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// See <a href="https://corefork.telegram.org/method/phone.joinGroupCallPresentation" />
///</summary>
internal sealed class JoinGroupCallPresentationHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestJoinGroupCallPresentation, MyTelegram.Schema.IUpdates>,
    Phone.IJoinGroupCallPresentationHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestJoinGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}
