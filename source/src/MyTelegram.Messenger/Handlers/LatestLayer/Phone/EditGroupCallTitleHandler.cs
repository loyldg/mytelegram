namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Edit the title of a group call or livestream
/// Possible errors
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.editGroupCallTitle"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditGroupCallTitleHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestEditGroupCallTitle, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestEditGroupCallTitle obj)
    {
        throw new NotImplementedException();
    }
}