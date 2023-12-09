// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Start a telegram phone call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_PROTOCOL_FLAGS_INVALID Call protocol flags invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PARTICIPANT_VERSION_OUTDATED The other participant does not use an up to date telegram client with support for calls.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// See <a href="https://corefork.telegram.org/method/phone.requestCall" />
///</summary>
internal sealed class RequestCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestRequestCall, MyTelegram.Schema.Phone.IPhoneCall>,
    Phone.IRequestCallHandler
{
    protected override Task<MyTelegram.Schema.Phone.IPhoneCall> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestRequestCall obj)
    {
        throw new NotImplementedException();
    }
}
