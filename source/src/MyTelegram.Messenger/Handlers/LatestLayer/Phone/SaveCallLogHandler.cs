namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Save phone call debug information
/// Possible errors
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.saveCallLog"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveCallLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveCallLog, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSaveCallLog obj)
    {
        throw new NotImplementedException();
    }
}