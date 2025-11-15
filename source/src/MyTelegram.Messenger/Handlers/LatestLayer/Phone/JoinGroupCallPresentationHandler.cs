namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Start screen sharing in a call
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 403 PARTICIPANT_JOIN_MISSING Trying to enable a presentation, when the user hasn't joined the Video Chat with <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.joinGroupCallPresentation"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class JoinGroupCallPresentationHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestJoinGroupCallPresentation, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestJoinGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}