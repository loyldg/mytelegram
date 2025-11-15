namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get pinned dialogs
/// Possible errors
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getPinnedDialogs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPinnedDialogsHandler(IDialogAppService dialogAppService, IPtsHelper ptsHelper, IDialogConverterService dialogConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPinnedDialogs, MyTelegram.Schema.Messages.IPeerDialogs>
{
    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input, RequestGetPinnedDialogs obj)
    {
        var userId = input.UserId;
        var getDialogOutput = await dialogAppService.GetDialogsAsync(new GetDialogInput { Pinned = true, OwnerId = userId, Limit = DefaultPageSize, FolderId = obj.FolderId });
        var cachedPts = ptsHelper.GetCachedPts(input.UserId);
        getDialogOutput.CachedPts = cachedPts;
        return dialogConverterService.ToPeerDialogs(input, getDialogOutput, input.Layer);
    }
}