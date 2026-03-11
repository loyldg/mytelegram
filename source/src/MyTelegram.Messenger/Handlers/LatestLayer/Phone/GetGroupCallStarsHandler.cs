namespace MyTelegram.Messenger.Handlers.Phone;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupCallStars"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupCallStarsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStars, MyTelegram.Schema.Phone.IGroupCallStars>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStars> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCallStars obj)
    {
        throw new NotImplementedException();
    }
}