// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Refuse or end running call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_ALREADY_ACCEPTED The call was already accepted.
/// 500 CALL_OCCUPY_FAILED The call failed because the user is already making another call.
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.discardCall" />
///</summary>
internal sealed class DiscardCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDiscardCall, MyTelegram.Schema.IUpdates>,
    Phone.IDiscardCallHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestDiscardCall obj)
    {
        throw new NotImplementedException();
    }
}
