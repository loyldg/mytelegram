namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Set the default peer that will be used to join a group call in a specific dialog.
/// Possible errors
/// Code Type Description
/// 400 JOIN_AS_PEER_INVALID The specified peer cannot be used to join a group call.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.saveDefaultGroupCallJoinAs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveDefaultGroupCallJoinAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveDefaultGroupCallJoinAs, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSaveDefaultGroupCallJoinAs obj)
    {
        throw new NotImplementedException();
    }
}