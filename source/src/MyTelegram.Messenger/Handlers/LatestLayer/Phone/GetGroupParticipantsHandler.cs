namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get group call participants
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupParticipants"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupParticipants, MyTelegram.Schema.Phone.IGroupParticipants>
{
    protected override Task<MyTelegram.Schema.Phone.IGroupParticipants> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupParticipants obj)
    {
        throw new NotImplementedException();
    }
}