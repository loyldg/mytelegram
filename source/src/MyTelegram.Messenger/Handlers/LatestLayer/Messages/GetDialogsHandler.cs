namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Returns the current user dialog list.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 OFFSET_PEER_ID_INVALID The provided offset peer is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDialogs" />
///</summary>
internal sealed class GetDialogsHandler(
    IDialogAppService dialogAppService,
    IPeerHelper peerHelper,
    IDialogConverterService dialogConverterService,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogs, MyTelegram.Schema.Messages.IDialogs>
{
    protected override async Task<IDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetDialogs obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.OffsetPeer);

        var userId = input.UserId;
        var offsetPeer = peerHelper.GetPeer(obj.OffsetPeer);
        bool? pinned = null;
        if (obj.ExcludePinned)
        {
            pinned = false;
        }

        var getDialogOutput = await dialogAppService.GetDialogsAsync(new GetDialogInput
        {
            FolderId = obj.FolderId,
            Limit = obj.Limit,
            Pinned = pinned,
            //Pinned = !obj.ExcludePinned,
            //ExcludePinned = obj.ExcludePinned,
            OwnerId = userId,
            OffsetPeer = offsetPeer
        });

        return dialogConverterService.ToDialogs(input, getDialogOutput, input.Layer);
    }
}
