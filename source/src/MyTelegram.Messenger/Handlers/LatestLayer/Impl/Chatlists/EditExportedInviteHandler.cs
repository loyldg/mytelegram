// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Edit a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// See <a href="https://corefork.telegram.org/method/chatlists.editExportedInvite" />
///</summary>
internal sealed class EditExportedInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestEditExportedInvite, MyTelegram.Schema.IExportedChatlistInvite>,
    Chatlists.IEditExportedInviteHandler
{
    protected override Task<MyTelegram.Schema.IExportedChatlistInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestEditExportedInvite obj)
    {
        throw new NotImplementedException();
    }
}
