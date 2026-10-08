using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public class DialogConverterService(
    IUserConverterService userConverterService,
    IChatConverterService chatConverterService,
    IMessageConverterService messageConverterService,
    ILayeredService<IDraftMessageConverter> draftMessageLayeredService,
    ILayeredService<IPeerNotifySettingsConverter> peerNotifySettingsLayeredService,
    ILayeredService<IDialogConverter> dialogLayeredService) : IDialogConverterService, ITransientDependency
{
    public async Task<IDialogs> ToDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output, int layer = 0)
    {
        var dialogs = new List<IDialog>();
        var channels = output.ChannelList.ToDictionary(k => k.ChannelId);
        foreach (var dialogReadModel in output.DialogList)
        {
            IChannelReadModel? channelReadModel = null;
            if (dialogReadModel.ToPeerType == PeerType.Channel)
            {
                channels.TryGetValue(dialogReadModel.ToPeerId, out channelReadModel);
            }

            var dialog = ToDialog(dialogReadModel, channelReadModel, layer);
            dialogs.Add(dialog);
        }

        var users = await userConverterService.ToUserListAsync(request, output.UserList, output.PhotoList, output.ContactList,
            output.PrivacyList, layer);
        var channelList = chatConverterService.ToChannelList(request,
            output.ChannelList,
            output.PhotoList,
            output.ChannelMemberList,
            layer: layer
        );
        var allMessages = output.MessageList.ToList();
        foreach (var channel in channelList)
        {
            if (channel is TChannelForbidden channelForbidden)
            {
                allMessages.RemoveAll(p => p.ToPeerId == channelForbidden.Id);
            }
        }

        var messages = messageConverterService
            .ToMessageList(output.SelfUserId, allMessages, output.PollList, output.ChosenPollOptions, output.UserReactionList, layer);

        if (dialogs.Count == output.Limit)
        {
            return new TDialogsSlice
            {
                Chats = [.. channelList],
                Dialogs = [.. dialogs],
                Messages = [.. messages],
                Users = [.. users],
                Count = dialogs.Count
            };
        }
        return new TDialogs
        {
            Chats = [.. channelList],
            Dialogs = [.. dialogs],
            Messages = [.. messages],
            Users = [.. users],
        };
    }

    public async Task<IPeerDialogs> ToPeerDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output, int layer = 0)
    {
        var dialogs = await ToDialogsAsync(request, output, layer);
        var pts = output.PtsReadModel?.Pts ?? 0;
        if (output.CachedPts > pts)
        {
            pts = output.CachedPts;
        }
        return dialogs switch
        {
            TDialogs dialogs1 => new TPeerDialogs
            {
                Chats = dialogs1.Chats,
                Dialogs = dialogs1.Dialogs,
                Messages = dialogs1.Messages,
                Users = dialogs1.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            TDialogsSlice dialogsSlice => new TPeerDialogs
            {
                Chats = dialogsSlice.Chats,
                Dialogs = dialogsSlice.Dialogs,
                Messages = dialogsSlice.Messages,
                Users = dialogsSlice.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            _ => throw new ArgumentOutOfRangeException(nameof(output))
        };
    }

    private IDialog ToDialog(IDialogReadModel dialogReadModel,
        IChannelReadModel? channelReadModel,
        int layer)
    {
        var peerNotifySettings = peerNotifySettingsLayeredService.GetConverter(layer)
            .ToPeerNotifySettings(dialogReadModel.NotifySettings ?? PeerNotifySettings.DefaultSettings);
        var dialog = dialogLayeredService.GetConverter(layer).ToDialog(dialogReadModel, channelReadModel, peerNotifySettings);

        if (dialogReadModel.Draft != null)
        {
            if (dialog is ILayeredDialog layeredDialog)
            {
                var draftMessage = draftMessageLayeredService.GetConverter(layer).ToDraftMessage(dialogReadModel.Draft);
                layeredDialog.Draft = draftMessage;
            }
        }

        return dialog;
    }
}