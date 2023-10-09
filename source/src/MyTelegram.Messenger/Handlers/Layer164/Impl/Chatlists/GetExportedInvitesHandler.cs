// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// List all <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep links »</a> associated to a folder
/// See <a href="https://corefork.telegram.org/method/chatlists.getExportedInvites" />
///</summary>
internal sealed class GetExportedInvitesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestGetExportedInvites, MyTelegram.Schema.Chatlists.IExportedInvites>,
    Chatlists.IGetExportedInvitesHandler
{
    protected override Task<MyTelegram.Schema.Chatlists.IExportedInvites> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestGetExportedInvites obj)
    {
        throw new NotImplementedException();
    }
}
