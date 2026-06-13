namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Start a telegram phone call
/// Possible errors
/// Code Type Description
/// 400 CALL_PROTOCOL_FLAGS_INVALID Call protocol flags invalid.
/// 400 CALL_PROTOCOL_LAYER_INVALID The specified protocol layer version range is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 500 PARTICIPANT_CALL_FAILED  
/// 400 PARTICIPANT_VERSION_OUTDATED The other participant does not use an up to date telegram client with support for calls.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.requestCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestRequestCall, MyTelegram.Schema.Phone.IPhoneCall>
{
    protected override Task<MyTelegram.Schema.Phone.IPhoneCall> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestRequestCall obj)
    {
        throw new NotImplementedException();
    }
}