using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public class DialogConverterService(
    IUserConverterService userConverterService,
    IChatConverterService chatConverterService,
    IMessageConverterService messageConverterService,
    IDraftMessageConverter draftMessageConverter,
    IPeerNotifySettingsConverter peerNotifySettingsConverter,
    IDialogConverter dialogConverter
    ) : IDialogConverterService, ITransientDependency
{
    public async Task<IDialogs> ToDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output)
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

            var dialog = ToDialog(dialogReadModel, channelReadModel);
            dialogs.Add(dialog);
        }

        var users = await userConverterService.ToUserListAsync(request, output.UserList, output.PhotoList, output.ContactList,
            output.PrivacyList);
        var channelList = chatConverterService.ToChannelList(request,
            output.ChannelList,
            output.PhotoList,
            output.ChannelMemberList
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
            .ToMessageList(output.SelfUserId, allMessages, output.PollList, output.ChosenPollOptions, output.UserReactionList);

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

    public async Task<IPeerDialogs> ToPeerDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output)
    {
        var dialogs = await ToDialogsAsync(request, output);
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
        IChannelReadModel? channelReadModel)
    {
        var peerNotifySettings = peerNotifySettingsConverter
            .ToPeerNotifySettings(dialogReadModel.NotifySettings ?? PeerNotifySettings.DefaultSettings);
        var dialog = dialogConverter.ToDialog(dialogReadModel, channelReadModel, peerNotifySettings);

        if (dialogReadModel.Draft != null)
        {
            if (dialog is ILayeredDialog layeredDialog)
            {
                var draftMessage = draftMessageConverter.ToDraftMessage(dialogReadModel.Draft);
                layeredDialog.Draft = draftMessage;
            }
        }

        return dialog;
    }
}