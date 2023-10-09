// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Send phone call debug data to server
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.saveCallDebug" />
///</summary>
internal sealed class SaveCallDebugHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveCallDebug, IBool>,
    Phone.ISaveCallDebugHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSaveCallDebug obj)
    {
        throw new NotImplementedException();
    }
}
