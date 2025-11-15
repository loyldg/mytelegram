namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Terminate a group call
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_ALREADY_DISCARDED The group call was already discarded.
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.discardGroupCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DiscardGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDiscardGroupCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDiscardGroupCall obj)
    {
        throw new NotImplementedException();
    }
}