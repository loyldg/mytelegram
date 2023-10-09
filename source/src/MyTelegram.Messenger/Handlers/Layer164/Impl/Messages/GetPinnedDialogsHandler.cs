// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// See <a href="https://corefork.telegram.org/method/messages.getPinnedDialogs" />
///</summary>
internal sealed class GetPinnedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPinnedDialogs, MyTelegram.Schema.Messages.IPeerDialogs>,
    Messages.IGetPinnedDialogsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IPeerDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPinnedDialogs obj)
    {
        throw new NotImplementedException();
    }
}
