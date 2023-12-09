// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Export a <a href="https://corefork.telegram.org/api/folders">folder »</a>, creating a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// 400 INVITES_TOO_MUCH The maximum number of per-folder invites specified by the <code>chatlist_invites_limit_default</code>/<code>chatlist_invites_limit_premium</code> <a href="https://corefork.telegram.org/api/config#chatlist-invites-limit-default">client configuration parameters »</a> was reached.
/// 400 PEERS_LIST_EMPTY The specified list of peers is empty.
/// See <a href="https://corefork.telegram.org/method/chatlists.exportChatlistInvite" />
///</summary>
internal sealed class ExportChatlistInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestExportChatlistInvite, MyTelegram.Schema.Chatlists.IExportedChatlistInvite>,
    Chatlists.IExportChatlistInviteHandler
{
    protected override Task<MyTelegram.Schema.Chatlists.IExportedChatlistInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestExportChatlistInvite obj)
    {
        throw new NotImplementedException();
    }
}
