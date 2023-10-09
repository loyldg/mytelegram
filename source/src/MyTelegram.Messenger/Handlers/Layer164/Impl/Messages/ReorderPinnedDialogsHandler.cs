// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Reorder pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reorderPinnedDialogs" />
///</summary>
internal sealed class ReorderPinnedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderPinnedDialogs, IBool>,
    Messages.IReorderPinnedDialogsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReorderPinnedDialogs obj)
    {
        throw new NotImplementedException();
    }
}
