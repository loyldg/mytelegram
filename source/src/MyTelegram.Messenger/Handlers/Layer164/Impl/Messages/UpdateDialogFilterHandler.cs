// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Update <a href="https://corefork.telegram.org/api/folders">folder</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHATLIST_EXCLUDE_INVALID &nbsp;
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_INCLUDE_EMPTY The include_peers vector of the filter is empty.
/// 400 FILTER_TITLE_EMPTY The title field of the filter is empty.
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFilter" />
///</summary>
internal sealed class UpdateDialogFilterHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateDialogFilter, IBool>,
    Messages.IUpdateDialogFilterHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUpdateDialogFilter obj)
    {
        throw new NotImplementedException();
    }
}
