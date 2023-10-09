// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Delete a previously created <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// See <a href="https://corefork.telegram.org/method/chatlists.deleteExportedInvite" />
///</summary>
internal sealed class DeleteExportedInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestDeleteExportedInvite, IBool>,
    Chatlists.IDeleteExportedInviteHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestDeleteExportedInvite obj)
    {
        throw new NotImplementedException();
    }
}
