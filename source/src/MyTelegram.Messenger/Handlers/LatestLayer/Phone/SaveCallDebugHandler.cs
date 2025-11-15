namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Send phone call debug data to server
/// Possible errors
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.saveCallDebug"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveCallDebugHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveCallDebug, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSaveCallDebug obj)
    {
        throw new NotImplementedException();
    }
}