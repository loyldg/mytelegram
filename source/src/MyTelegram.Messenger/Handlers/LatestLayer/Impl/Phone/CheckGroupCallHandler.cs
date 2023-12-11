// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Check whether the group call Server Forwarding Unit is currently receiving the streams with the specified WebRTC source IDs.<br>
/// Returns an intersection of the source IDs specified in <code>sources</code>, and the source IDs currently being forwarded by the SFU.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GROUPCALL_JOIN_MISSING You haven't joined this group call.
/// See <a href="https://corefork.telegram.org/method/phone.checkGroupCall" />
///</summary>
internal sealed class CheckGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestCheckGroupCall, TVector<int>>,
    Phone.ICheckGroupCallHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestCheckGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
