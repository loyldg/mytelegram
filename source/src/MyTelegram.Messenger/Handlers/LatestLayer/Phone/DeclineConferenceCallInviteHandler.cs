namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Declines a conference call invite.
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.declineConferenceCallInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeclineConferenceCallInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeclineConferenceCallInvite, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDeclineConferenceCallInvite obj)
    {
        throw new NotImplementedException();
    }
}