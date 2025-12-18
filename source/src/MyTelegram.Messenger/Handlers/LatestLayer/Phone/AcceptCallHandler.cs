namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Accept incoming call
/// Possible errors
/// Code Type Description
/// 400 CALL_ALREADY_ACCEPTED The call was already accepted.
/// 400 CALL_ALREADY_DECLINED The call was already declined.
/// 500 CALL_OCCUPY_FAILED The call failed because the user is already making another call.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// 406 CALL_PROTOCOL_COMPAT_LAYER_INVALID The other side of the call does not support any of the VoIP protocols supported by the local client, as specified by the <code>protocol.layer</code> and <code>protocol.library_versions</code> fields.
/// 400 CALL_PROTOCOL_FLAGS_INVALID Call protocol flags invalid.
/// 400 CALL_PROTOCOL_LAYER_INVALID The specified protocol layer version range is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.acceptCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AcceptCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestAcceptCall, MyTelegram.Schema.Phone.IPhoneCall>
{
    protected override Task<MyTelegram.Schema.Phone.IPhoneCall> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestAcceptCall obj)
    {
        throw new NotImplementedException();
    }
}