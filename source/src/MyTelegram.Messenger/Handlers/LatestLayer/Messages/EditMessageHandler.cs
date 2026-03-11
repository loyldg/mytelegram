using System.Text;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Edit message
/// Possible errors
/// Code Type Description
/// 400 BOT_DOMAIN_INVALID Bot domain invalid.
/// 400 BOT_INVALID This is not a valid bot.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 BUSINESS_PEER_INVALID Messages can't be set to the specified peer through the current <a href="https://corefork.telegram.org/api/business#connected-bots">business connection</a>.
/// 400 BUTTON_COPY_TEXT_INVALID The specified <a href="https://corefork.telegram.org/constructor/keyboardButtonCopy">keyboardButtonCopy</a>.<code>copy_text</code> is invalid.
/// 400 BUTTON_DATA_INVALID The data of one or more of the buttons you provided is invalid.
/// 400 BUTTON_TYPE_INVALID The type of one or more of the buttons you provided is invalid.
/// 400 BUTTON_URL_INVALID Button URL invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_FORWARDS_RESTRICTED You can't forward messages from a protected chat.
/// 403 CHAT_SEND_GIFS_FORBIDDEN You can't send gifs in this chat.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// 400 ENTITIES_TOO_LONG You provided too many styled message entities.
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 403 INLINE_BOT_REQUIRED Only the inline bot can edit message.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MEDIA_CAPTION_TOO_LONG The caption is too long.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MEDIA_GROUPED_INVALID You tried to send media of different types in an album.
/// 400 MEDIA_INVALID Media invalid.
/// 400 MEDIA_NEW_INVALID The new media is invalid.
/// 400 MEDIA_PREV_INVALID Previous media invalid.
/// 400 MEDIA_TTL_INVALID The specified media TTL is invalid.
/// 403 MESSAGE_AUTHOR_REQUIRED Message author required.
/// 400 MESSAGE_EDIT_TIME_EXPIRED You can't edit this message anymore, too much time has passed since its creation.
/// 400 MESSAGE_EMPTY The provided message is empty.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_NOT_MODIFIED The provided message data is identical to the previous message data, the message wasn't modified.
/// 400 MESSAGE_TOO_LONG The provided message is too long.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 500 MSG_WAIT_FAILED A waiting call returned an error.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PEER_TYPES_INVALID The passed <a href="https://corefork.telegram.org/constructor/keyboardButtonSwitchInline">keyboardButtonSwitchInline</a>.<code>peer_types</code> field is invalid.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_INVALID_DIMENSIONS The photo dimensions are invalid.
/// 400 PHOTO_SAVE_FILE_INVALID Internal issues, try again later.
/// 400 REPLY_MARKUP_INVALID The provided reply markup is invalid.
/// 400 REPLY_MARKUP_TOO_LONG The specified reply_markup is too long.
/// 400 SCHEDULE_DATE_INVALID Invalid schedule date provided.
/// 400 TODO_ITEMS_EMPTY A checklist was specified, but no <a href="https://corefork.telegram.org/api/todo">checklist items</a> were passed.
/// 400 TODO_ITEM_DUPLICATE Duplicate <a href="https://corefork.telegram.org/api/todo">checklist items</a> detected.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 400 WEBPAGE_NOT_FOUND A preview for the specified webpage <code>url</code> could not be generated.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.editMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class EditMessageHandler(IMediaHelper mediaHelper, ICommandBus commandBus, IPeerHelper peerHelper, IAccessHashHelper accessHashHelper, IMessageAppService messageAppService, IDataEncryptionHelper dataEncryptionHelper, IOptionsMonitor<MyTelegramMessengerServerOptions> options, IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditMessage, MyTelegram.Schema.IUpdates>
{
    private static byte[]? _encryptionKey;
    private static KeyConfig? _keyConfig;
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestEditMessage obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var ownerPeerId = input.UserId;
        if (peer.PeerType == PeerType.Channel)
        {
            ownerPeerId = peer.PeerId;
        }

        IMessageMedia? media = null;
        if (obj.Media != null)
        {
            if (obj.Media is TInputMediaEmpty)
            {
                media = new TMessageMediaEmpty();
            }
            else
            {
                media = await mediaHelper.SaveMediaAsync(obj.Media);
            }
        }

        var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId.Create(ownerPeerId, obj.Id).Value));
        if (messageReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var message = messageReadModel.Message;
        var messageTextEdited = false;
        if (!string.IsNullOrEmpty(obj.Message))
        {
            message = obj.Message;
            messageTextEdited = true;
        }

        byte[]? encryptedData = null;
        byte[]? inboxMessageEncryptedData = null;
        if (messageTextEdited && options.CurrentValue.EncryptionConfig is { Enabled: true, MessageKeys.Count: > 0 })
        {
            _keyConfig ??= options.CurrentValue.EncryptionConfig.MessageKeys[0];
            _encryptionKey ??= Encoding.UTF8.GetBytes(_keyConfig.Key);
            encryptedData = dataEncryptionHelper.Encrypt(_keyConfig.Id, _encryptionKey, ownerPeerId, message);
            if (messageReadModel.ToPeerType == PeerType.User && messageReadModel.ToPeerId != input.UserId)
            {
                inboxMessageEncryptedData = dataEncryptionHelper.Encrypt(_keyConfig.Id, _encryptionKey, messageReadModel.ToPeerId, message);
            }

            message = string.Empty;
        }

        //InboxItem? inboxItem = null;
        //if (messageReadModel!.ToPeerType == PeerType.User)
        //{
        //    var inboxMessageReadModel =
        //        await queryProcessor.ProcessAsync(new GetMessageByBatchIdQuery(messageReadModel.BatchId,
        //            messageReadModel.OwnerPeerId));
        //    if (inboxMessageReadModel != null)
        //    {
        //        inboxItem = new InboxItem(inboxMessageReadModel.OwnerPeerId, inboxMessageReadModel.MessageId);
        //    }
        //}
        var entities = obj.Entities ?? [];
        await messageAppService.ProcessMessageEntitiesAsync(obj.Message, entities, peer);
        if (entities.Count == 0)
        {
            entities = null;
        }

        var hashtags = messageAppService.GetHashtags(obj.Message);
        var command = new EditOutboxMessageCommand(MessageId.Create(ownerPeerId, obj.Id, obj.QuickReplyShortcutId.HasValue), input.ToRequestInfo(), obj.Id, message, CurrentDate, entities, media, obj.ReplyMarkup, obj.InvertMedia, hashtags, encryptedData, inboxMessageEncryptedData);
        await commandBus.PublishAsync(command);
        return null !;
    }
}