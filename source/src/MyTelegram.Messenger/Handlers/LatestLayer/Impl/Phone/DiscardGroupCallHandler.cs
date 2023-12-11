// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Terminate a group call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GROUPCALL_ALREADY_DISCARDED The group call was already discarded.
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.discardGroupCall" />
///</summary>
internal sealed class DiscardGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDiscardGroupCall, MyTelegram.Schema.IUpdates>,
    Phone.IDiscardGroupCallHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestDiscardGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
