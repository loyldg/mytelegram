// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Accept incoming call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_ALREADY_ACCEPTED The call was already accepted.
/// 400 CALL_ALREADY_DECLINED The call was already declined.
/// 500 CALL_OCCUPY_FAILED The call failed because the user is already making another call.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// 400 CALL_PROTOCOL_FLAGS_INVALID Call protocol flags invalid.
/// See <a href="https://corefork.telegram.org/method/phone.acceptCall" />
///</summary>
internal sealed class AcceptCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestAcceptCall, MyTelegram.Schema.Phone.IPhoneCall>,
    Phone.IAcceptCallHandler
{
    protected override Task<MyTelegram.Schema.Phone.IPhoneCall> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestAcceptCall obj)
    {
        throw new NotImplementedException();
    }
}
