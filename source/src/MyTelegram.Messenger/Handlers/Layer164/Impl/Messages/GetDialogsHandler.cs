// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns the current user dialog list.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 OFFSET_PEER_ID_INVALID The provided offset peer is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDialogs" />
///</summary>
internal sealed class GetDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogs, MyTelegram.Schema.Messages.IDialogs>,
    Messages.IGetDialogsHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPeerHelper _peerHelper;
    //private readonly ITlDialogConverter _dialogConverter;
    private readonly ILayeredService<IDialogConverter> _layeredService;

    private readonly IAccessHashHelper _accessHashHelper;
    public GetDialogsHandler(IDialogAppService dialogAppService,
        IPeerHelper peerHelper,
        ILayeredService<IDialogConverter> layeredService,
        IAccessHashHelper accessHashHelper)
    {
        _dialogAppService = dialogAppService;
        _peerHelper = peerHelper;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetDialogs obj)
    {
        // Archived Folders
        if (obj.FolderId == 1)
        {
            return new TDialogs
            {
                Chats = new(),
                Dialogs = new(),
                Messages = new(),
                Users = new()
            };
            //return new TDialogsNotModified
            //{
            //    Count = 0,
            //};
        }

        await _accessHashHelper.CheckAccessHashAsync(obj.OffsetPeer);

        var userId = input.UserId;
        var offsetPeer = _peerHelper.GetPeer(obj.OffsetPeer);
        bool? pinned = null;
        if (obj.ExcludePinned)
        {
            pinned = false;
        }

        var r = await _dialogAppService.GetDialogsAsync(new GetDialogInput
        {
            FolderId = obj.FolderId,
            Limit = obj.Limit,
            Pinned = pinned,
            //Pinned = !obj.ExcludePinned,
            //ExcludePinned = obj.ExcludePinned,
            OwnerId = userId,
            OffsetPeer = offsetPeer
        });

        return _layeredService.GetConverter(input.Layer).ToDialogs(r);
    }
}
