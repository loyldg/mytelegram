// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get info about a group call
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.getGroupCall" />
///</summary>
internal sealed class GetGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCall, MyTelegram.Schema.Phone.IGroupCall>,
    Phone.IGetGroupCallHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCall> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
