namespace MyTelegram.Messenger.Handlers.Phone;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.saveDefaultSendAs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveDefaultSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveDefaultSendAs, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSaveDefaultSendAs obj)
    {
        throw new NotImplementedException();
    }
}