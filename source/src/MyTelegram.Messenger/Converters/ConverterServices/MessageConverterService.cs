using System.Buffers.Binary;
using System.Text;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public class MessageConverterService(
    IMessageMediaResponseService messageMediaResponseService,
    ILayeredService<IMessageConverter> messageLayeredService,
    ILayeredService<IMessageServiceConverter> messageServiceLayeredService,
    ILayeredService<IMessageFwdHeaderConverter> messageFwdHeaderLayeredService,
    ILayeredService<IPollConverter> pollLayeredService,
    IDataEncryptionHelper dataEncryptionHelper,
    IDataEncryptionHelper2 dataEncryptionHelper2,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options
) : IMessageConverterService, ITransientDependency
{
    private static Dictionary<int, byte[]> _keys = [];
    public IMessage ToMessage(
        long selfUserId,
        IMessageReadModel readModel,
        IPollReadModel? pollReadModel = null,
        List<string>? chosenOptions = null,
        List<IUserReactionReadModel>? userReactionReadModels = null,
        int layer = 0
    )
    {
        IMessageMedia? media = null;
        if (pollReadModel != null)
        {
            media = new TMessageMediaPoll
            {
                Poll = pollLayeredService.GetConverter(layer).ToPoll(pollReadModel),
                Results = pollLayeredService
                    .GetConverter(layer)
                    .ToPollResults(pollReadModel, chosenOptions),
            };
        }

        return ToMessage(selfUserId, readModel, media, userReactionReadModels, layer);
    }

    private IMessage ToMessage(
        long selfUserId,
        IMessageReadModel readModel,
        IMessageMedia? pollMedia = null,
        IReadOnlyCollection<IUserReactionReadModel>? userReactionReadModels = null,
        int layer = 0
    )
    {
        var fromId = new Peer(PeerType.User, readModel.SenderPeerId).ToPeer();
        switch (readModel.SendMessageType)
        {
            case SendMessageType.MessageService:
                {
                    var m = messageServiceLayeredService.GetConverter(layer).ToMessage(readModel);
                    if (readModel.ToPeerType == PeerType.Channel)
                    {
                        if (
                            readModel.Post
                            || readModel.MessageActionType == MessageActionType.ChannelCreate
                            || readModel.MessageActionType == MessageActionType.SetMessagesTtl
                        )
                        {
                            fromId = null;
                        }
                    }
                    if (readModel.SendAs != null)
                    {
                        fromId = readModel.SendAs.ToPeer();
                    }

                    m.FromId = fromId;
                    m.Out = readModel.SenderPeerId == selfUserId;

                    if (readModel.MentionedUserIds?.Contains(selfUserId) ?? false)
                    {
                        m.Mentioned = true;
                        m.MediaUnread = true;
                    }

                    return m;
                }

            default:
                {
                    var m = messageLayeredService.GetConverter(layer).ToMessage(readModel);
                    var media = readModel.Media2 ?? readModel.Media.ToTObject<IMessageMedia>();
                    m.Media = messageMediaResponseService.ToLayeredData(media, layer);
                    m.Out = readModel.SenderPeerId == selfUserId;
                    m.FromId = fromId;
                    if (readModel.FwdHeader != null)
                    {
                        m.FwdFrom = messageFwdHeaderLayeredService
                            .GetConverter(layer)
                            .ToMessageFwdHeader(readModel.FwdHeader);
                    }

                    if (readModel.ToPeerType == PeerType.Channel)
                    {
                        m.Replies = ToMessageReplies(readModel.Post, readModel.Reply);
                        if (m.Replies != null && readModel.FwdHeader != null) // forward from linked channel
                        {
                            m.FromId = readModel.FwdHeader.FromId.ToPeer();
                            m.Out = false;
                        }

                        if (readModel.Post)
                        {
                            m.FromId = null;
                        }

                        if (readModel.SendAs != null)
                        {
                            m.FromId = readModel.SendAs.ToPeer();
                        }
                    }

                    if (m.QuickReplyShortcutId.HasValue)
                    {
                        m.Date = 0;
                    }

                    if (m.Out)
                    {
                        m.FromScheduled = readModel.FromScheduled;
                    }

                    if (pollMedia != null)
                    {
                        m.Media = pollMedia;
                    }

                    if (readModel.MentionedUserIds?.Contains(selfUserId) ?? false)
                    {
                        m.Mentioned = true;
                        m.MediaUnread = true;
                    }

                    if (readModel.EncryptedData?.Length > 0)
                    {
                        m.Message = DecryptMessage(readModel.OwnerPeerId, readModel.MessageId, readModel.EncryptedData);
                    }

                    if (string.IsNullOrEmpty(m.Message))
                    {
                        m.Message = string.Empty;
                    }

                    return m;
                }
        }
    }

    public List<IMessage> ToMessageList(
        long selfUserId,
        IReadOnlyCollection<IMessageReadModel> messageReadModels,
        IReadOnlyCollection<IPollReadModel>? pollReadModels,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? pollAnswerVoterReadModels,
        IReadOnlyCollection<IUserReactionReadModel>? userReactionReadModels,
        int layer = 0
    )
    {
        var messages = new List<IMessage>();
        var pollReadModelsDict = pollReadModels?.ToDictionary(p => p.PollId) ?? [];
        var pollAnswerVoterReadModelsDict =
            pollAnswerVoterReadModels
                ?.GroupBy(p => p.PollId)
                .ToDictionary(k => k.Key, v => v.Select(x => x.Option).ToList()) ?? [];

        var pollConverter = pollLayeredService.GetConverter(layer);

        foreach (var readModel in messageReadModels)
        {
            IMessageMedia? media = null;
            if (readModel.PollId.HasValue)
            {
                pollReadModelsDict.TryGetValue(readModel.PollId.Value, out var poll);
                pollAnswerVoterReadModelsDict.TryGetValue(
                    readModel.PollId.Value,
                    out var chosenOptions
                );

                if (poll != null)
                {
                    media = new TMessageMediaPoll
                    {
                        Poll = pollConverter.ToPoll(poll),
                        Results = pollConverter.ToPollResults(poll, chosenOptions),
                    };
                }
            }

            messages.Add(ToMessage(selfUserId, readModel, media, null, layer));
        }

        return messages;
    }

    public IMessage ToMessage(
        long selfUserId,
        MessageItem item,
        List<long>? userReactionIds = null,
        bool mentioned = false,
        int layer = 0
    )
    {
        var isOut = item.IsOut;
        var fromId = item.SenderPeer.ToPeer();

        if (item.ToPeer.PeerType == PeerType.Channel /*&& selfUserId != 0*/)
        {
            isOut = item.SenderPeer.PeerId == selfUserId;
        }

        switch (item.SendMessageType)
        {
            case SendMessageType.MessageService:
                {
                    if (item.ToPeer.PeerType == PeerType.Channel)
                    {
                        if (
                            item.Post
                            || item.MessageSubType == MessageSubType.CreateChannel
                            || item.MessageSubType == MessageSubType.SetHistoryTtl
                        )
                        {
                            fromId = null;
                        }
                    }

                    var m = messageServiceLayeredService.GetConverter(layer).ToMessage(item);

                    m.Out = isOut;
                    m.Mentioned = mentioned;
                    m.MediaUnread = mentioned;
                    m.FromId = fromId;
                    if (item.SendAs != null)
                    {
                        m.FromId = item.SendAs.ToPeer();
                    }

                    return m;
                }

            default:
                {
                    var media = messageMediaResponseService.ToLayeredData(item.Media, layer);
                    var m = messageLayeredService.GetConverter(layer).ToMessage(item);

                    m.Media = media;
                    m.Out = isOut;
                    m.Mentioned = mentioned;
                    m.MediaUnread = mentioned;
                    m.FromId = fromId;

                    if (item.FwdHeader != null)
                    {
                        m.FwdFrom = messageFwdHeaderLayeredService
                            .GetConverter(layer)
                            .ToMessageFwdHeader(item.FwdHeader);
                    }

                    if (item.Post)
                    {
                        m.FromId = null;
                    }

                    if (item.ToPeer.PeerType == PeerType.Channel)
                    {
                        m.Replies = ToMessageReplies(item.Post, item.Reply);
                        if (m.Replies != null && item.FwdHeader?.SavedFromPeer != null) // forward from linked channel
                        {
                            //m.FromId = _peerHelper.ToPeer(PeerType.Channel, item.FwdHeader.SavedFromPeer.PeerId);
                            m.FromId = item.FwdHeader.SavedFromPeer.ToPeer();
                            m.Out = false;
                        }

                        if (item.Post)
                        {
                            m.FromId = null;
                        }

                        if (item.SendAs != null)
                        {
                            m.FromId = item.SendAs.ToPeer();
                        }
                    }


                    if (m.QuickReplyShortcutId.HasValue)
                    {
                        m.Date = 0;
                    }

                    if (m.Out)
                    {
                        m.FromScheduled = item.ScheduleDate.HasValue;
                    }

                    if (item.EncryptedData?.Length > 0)
                    {
                        m.Message = DecryptMessage(item.OwnerPeer.PeerId, item.MessageId, item.EncryptedData);
                    }

                    return m;
                }
        }
    }

    protected IMessageReplies? ToMessageReplies(bool post, MessageReply? reply)
    {
        if (reply == null)
        {
            return null;
        }

        if (post)
        {
            if (reply.ChannelId == null)
            {
                return null;
            }
        }

        var messageReplies = new TMessageReplies
        {
            Comments = post,
            MaxId = reply.MaxId,
            Replies = reply.Replies,
            RepliesPts = reply.RepliesPts,
        };

        if (post)
        {
            messageReplies.ChannelId = reply.ChannelId;
            messageReplies.RecentRepliers = [];
            if (reply.RecentRepliers?.Count > 0)
            {
                messageReplies.RecentRepliers = [.. reply.RecentRepliers.Select(p => p.ToPeer())];
            }
        }

        return messageReplies;
    }

    private void InitKeys()
    {
        if (_keys.Count == 0)
        {
            _keys = options.CurrentValue.EncryptionConfig.MessageKeys.ToDictionary(k => k.Id,
                v => Encoding.UTF8.GetBytes(v.Key));
        }
    }

    public string DecryptMessage(long ownerPeerId, int messageId, ReadOnlyMemory<byte>? encryptedData)
    {
        return dataEncryptionHelper2.DecryptMessage(ownerPeerId, encryptedData);
        //if (encryptedData == null || encryptedData.Value.IsEmpty)
        //{
        //    return string.Empty;
        //}
        ////var key=op

        //InitKeys();

        //var encryptedSpan = encryptedData.Value.Span;
        //var keyId = BinaryPrimitives.ReadInt32LittleEndian(encryptedSpan);

        //if (!_keys.TryGetValue(keyId, out var masterKey))
        //{
        //    return string.Empty;
        //}
        //var tempBytes = ArrayPool<byte>.Shared.Rent(encryptedSpan.Length);
        //try
        //{
        //    var span = tempBytes.AsSpan(0, encryptedSpan.Length);
        //    var count = dataEncryptionHelper.Decrypt(masterKey, ownerPeerId, encryptedSpan.Slice(4), span);
        //    return Encoding.UTF8.GetString(span.Slice(0, count));
        //}
        //finally
        //{
        //    ArrayPool<byte>.Shared.Return(tempBytes);
        //}
    }
}
