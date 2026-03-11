namespace MyTelegram.Messenger.Handlers.Phone;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.deleteGroupCallMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteGroupCallMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeleteGroupCallMessages, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDeleteGroupCallMessages obj)
    {
        throw new NotImplementedException();
    }
}