// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Create a group call or livestream
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CREATE_CALL_FAILED An error occurred while creating the call.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SCHEDULE_DATE_INVALID Invalid schedule date provided.
/// See <a href="https://corefork.telegram.org/method/phone.createGroupCall" />
///</summary>
internal sealed class CreateGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestCreateGroupCall, MyTelegram.Schema.IUpdates>,
    Phone.ICreateGroupCallHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestCreateGroupCall obj)
    {
        throw new NotImplementedException();
    }
}
