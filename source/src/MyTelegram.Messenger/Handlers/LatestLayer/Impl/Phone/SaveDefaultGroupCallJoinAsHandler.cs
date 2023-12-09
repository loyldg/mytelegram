// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Set the default peer that will be used to join a group call in a specific dialog.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 JOIN_AS_PEER_INVALID The specified peer cannot be used to join a group call.
/// See <a href="https://corefork.telegram.org/method/phone.saveDefaultGroupCallJoinAs" />
///</summary>
internal sealed class SaveDefaultGroupCallJoinAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveDefaultGroupCallJoinAs, IBool>,
    Phone.ISaveDefaultGroupCallJoinAsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSaveDefaultGroupCallJoinAs obj)
    {
        throw new NotImplementedException();
    }
}
