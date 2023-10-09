// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Edit the title of a group call or livestream
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// See <a href="https://corefork.telegram.org/method/phone.editGroupCallTitle" />
///</summary>
internal sealed class EditGroupCallTitleHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestEditGroupCallTitle, MyTelegram.Schema.IUpdates>,
    Phone.IEditGroupCallTitleHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestEditGroupCallTitle obj)
    {
        throw new NotImplementedException();
    }
}
