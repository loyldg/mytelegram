// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Join a group call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 GROUPCALL_SSRC_DUPLICATE_MUCH The app needs to retry joining the group call with a new SSRC value.
/// 400 JOIN_AS_PEER_INVALID The specified peer cannot be used to join a group call.
/// See <a href="https://corefork.telegram.org/method/phone.joinGroupCall" />
///</summary>
internal sealed class JoinGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestJoinGroupCall, MyTelegram.Schema.IUpdates>,
    Phone.IJoinGroupCallHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestJoinGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
