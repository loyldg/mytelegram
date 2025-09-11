namespace MyTelegram.Converters.Responses;

public class UpdatesResponseService(
    IMessageResponseService messageResponseService,
    IUserResponseService userResponseService,
    IChannelResponseService channelResponseService,
    IChannelParticipantResponseService channelParticipantResponseService,
    IEmojiStatusResponseService emojiStatusResponseService,
    IChatInviteExportedResponseService chatInviteExportedResponseService,
    IDialogFilterResponseService dialogFilterResponseService,
    IMessageReactionsResponseService messageReactionsResponseService,
    IPhoneCallResponseService phoneCallResponseService,
    //IBroadcastRevenueBalancesResponseService broadcastRevenueBalancesResponseService,
    IMessageMediaResponseService messageMediaResponseService,
    IDocumentAttributeVideoResponseService documentAttributeVideoResponseService,
    ILayeredService<IUpdateDeleteScheduledMessagesResponseConverter> updateDeleteScheduledMessagesLayeredService,
    ILayeredService<IUpdateStarsBalanceResponseConverter> updateStarsBalanceLayeredService,
    ILayeredService<IUpdateGroupCallResponseConverter> updateGroupCallLayeredService,
    ILayeredService<IBoostResponseConverter> boostLayeredService,
    IAccessHashHelper2 accessHashHelper2,
    ILayeredService<IUpdateMessageExtendedMediaResponseConverter> updateMessageExtendedMediaLayeredService
) : IUpdatesResponseService, ITransientDependency
{
    public IUpdates ToLayeredData(long userId, long accessHashKeyId, IUpdates latestLayerData, int layer, LayeredResponseExtraData? layeredResponseExtraData)
    {
        return ToLayeredDataCore(userId, accessHashKeyId, latestLayerData, layer, layeredResponseExtraData);
    }

    public IUpdate ToLayeredData(long userId, long accessHashKeyId, IUpdate latestLayerData, int layer, LayeredResponseExtraData? layeredResponseExtraData)
    {
        return ToLayeredUpdateData(userId, accessHashKeyId, latestLayerData, layer, layeredResponseExtraData);
    }

    private IUpdates ToLayeredDataCore(long userId, long accessHashKeyId, IUpdates latestLayerData, int layer, LayeredResponseExtraData? layeredResponseExtraData)
    {
        switch (latestLayerData)
        {
            case TUpdates updates:
                for (var i = 0; i < updates.Users.Count; i++)
                {
                    if (updates.Users[i] is TUser user)
                    {
                        updates.Users[i] = userResponseService.ToLayeredData(user, layer); ;
                    }

                    if (updates.Users[i] is ILayeredUser layeredUser)
                    {
                        layeredUser.AccessHash =
                            accessHashHelper2.GenerateAccessHash(userId, accessHashKeyId, layeredUser.Id, AccessHashType.User);
                        layeredUser.Self = userId == layeredUser.Id;
                    }
                }

                for (var i = 0; i < updates.Chats.Count; i++)
                {
                    if (updates.Chats[i] is TChannel channel)
                    {
                        updates.Chats[i] = channelResponseService.ToLayeredData(channel, layer);
                    }

                    if (updates.Chats[i] is ILayeredChannel layeredChannel)
                    {
                        layeredChannel.AccessHash = accessHashHelper2.GenerateAccessHash(userId, accessHashKeyId, layeredChannel.Id, AccessHashType.Channel);
                        if (layeredResponseExtraData != null && layeredResponseExtraData.BannedRights != 0)
                        {
                            layeredChannel.AdminRights = null;
                            layeredChannel.BannedRights = ChatBannedRights.FromValue(layeredResponseExtraData.BannedRights, layeredResponseExtraData.UntilDate)
                                .ToChatBannedRights();
                        }
                    }
                }

                for (var i = 0; i < updates.Updates.Count; i++)
                {
                    var update = updates.Updates[i];
                    updates.Updates[i] = ToLayeredUpdateData(userId, accessHashKeyId, update, layer, layeredResponseExtraData);
                }

                break;
            case TUpdateShort updateShort:
                updateShort.Update = ToLayeredUpdateData(userId, accessHashKeyId, updateShort.Update, layer, layeredResponseExtraData);
                break;
            case TUpdateShortSentMessage updateShortSentMessage:

                updateShortSentMessage.Media =
                    messageMediaResponseService.ToLayeredData(updateShortSentMessage.Media, layer);
                break;
        }

        return latestLayerData;
    }

    private IUpdate ToLayeredUpdateData(long userId, long accessHashKeyId, IUpdate update, int layer, LayeredResponseExtraData? layeredResponseExtraData)
    {
        switch (update)
        {
            case TUpdateMessageExtendedMedia updateMessageExtendedMedia:
                return updateMessageExtendedMediaLayeredService.GetConverter(layer)
                    .ToLayeredData(updateMessageExtendedMedia);

            case TUpdateDeleteScheduledMessages updateDeleteScheduledMessages:
                return updateDeleteScheduledMessagesLayeredService.GetConverter(layer)
                    .ToLayeredData(updateDeleteScheduledMessages);

            case TUpdateStarsBalance updateStarsBalance:
                return updateStarsBalanceLayeredService.GetConverter(layer)
                    .ToLayeredData(updateStarsBalance);

            case TUpdateGroupCall updateGroupCall:
                return updateGroupCallLayeredService.GetConverter(layer)
                    .ToLayeredData(updateGroupCall);

            case TUpdateBotChatBoost updateBotChatBoost:
                if (updateBotChatBoost.Boost is TBoost boost)
                {
                    updateBotChatBoost.Boost = boostLayeredService.GetConverter(layer)
                        .ToLayeredData(boost);
                }

                return updateBotChatBoost;
            case TUpdateBotChatInviteRequester updateBotChatInviteRequester:
                updateBotChatInviteRequester.Invite =
                    chatInviteExportedResponseService.ToLayeredData(updateBotChatInviteRequester.Invite, layer);
                return updateBotChatInviteRequester;
            case TUpdateBotEditBusinessMessage updateBotEditBusinessMessage:
                updateBotEditBusinessMessage.Message =
                    messageResponseService.ToLayeredData(updateBotEditBusinessMessage.Message,
                        layer);
                updateBotEditBusinessMessage.ReplyToMessage =
                    messageResponseService.ToLayeredData(
                        updateBotEditBusinessMessage.ReplyToMessage, layer);
                break;

            case TUpdateBotNewBusinessMessage updateBotNewBusinessMessage:
                updateBotNewBusinessMessage.Message =
                    messageResponseService.ToLayeredData(updateBotNewBusinessMessage.Message,
                        layer);
                updateBotNewBusinessMessage.ReplyToMessage =
                    messageResponseService.ToLayeredData(
                        updateBotNewBusinessMessage.ReplyToMessage, layer);
                break;

            //case TUpdateBroadcastRevenueTransactions updateBroadcastRevenueTransactions:
            //    updateBroadcastRevenueTransactions.Balances =
            //        broadcastRevenueBalancesResponseService.ToLayeredData(updateBroadcastRevenueTransactions.Balances,
            //            layer);

                break;
            case TUpdateBusinessBotCallbackQuery updateBusinessBotCallbackQuery:
                updateBusinessBotCallbackQuery.Message =
                    messageResponseService.ToLayeredData(updateBusinessBotCallbackQuery.Message, layer);
                updateBusinessBotCallbackQuery.ReplyToMessage =
                    messageResponseService.ToLayeredData(updateBusinessBotCallbackQuery.ReplyToMessage, layer);
                break;

            case TUpdateChannelParticipant updateChannelParticipant:
                if (updateChannelParticipant.NewParticipant != null)
                {
                    updateChannelParticipant.NewParticipant =
                        channelParticipantResponseService.ToLayeredData(updateChannelParticipant.NewParticipant,
                            layer);
                }

                if (updateChannelParticipant.PrevParticipant != null)
                {
                    updateChannelParticipant.PrevParticipant =
                        channelParticipantResponseService.ToLayeredData(
                            updateChannelParticipant.PrevParticipant, layer);
                }
                break;

            case TUpdateChannelWebPage updateChannelWebPage:
                if (updateChannelWebPage.Webpage is TWebPage { Document: TDocument channelWebPageDocument })
                {
                    channelWebPageDocument.Attributes =
                        documentAttributeVideoResponseService.ToLayeredData(channelWebPageDocument.Attributes, layer);
                }

                break;

            case TUpdateDialogFilter updateDialogFilter:
                updateDialogFilter.Filter =
                    dialogFilterResponseService.ToLayeredData(updateDialogFilter.Filter, layer);
                break;

            case TUpdateEditChannelMessage updateEditChannelMessage:
                updateEditChannelMessage.Message =
                    messageResponseService.ToLayeredData(updateEditChannelMessage.Message, layer);

                return updateEditChannelMessage;
            case TUpdateEditMessage updateEditMessage:
                updateEditMessage.Message =
                    messageResponseService.ToLayeredData(updateEditMessage.Message, layer);
                break;

            case TUpdateMessageReactions updateMessageReactions:
                updateMessageReactions.Reactions =
                    messageReactionsResponseService.ToLayeredData(updateMessageReactions.Reactions, layer);
                break;
            case TUpdateNewChannelMessage updateNewChannelMessage:
                updateNewChannelMessage.Message =
                    messageResponseService.ToLayeredData(updateNewChannelMessage.Message, layer);

                return updateNewChannelMessage;
            case TUpdateNewMessage updateNewMessage:
                updateNewMessage.Message =
                    messageResponseService.ToLayeredData(updateNewMessage.Message, layer);
                break;
            case TUpdateNewScheduledMessage updateNewScheduledMessage:
                updateNewScheduledMessage.Message =
                    messageResponseService.ToLayeredData(updateNewScheduledMessage.Message, layer);
                break;
            case TUpdatePeerWallpaper updatePeerWallpaper:
                if (updatePeerWallpaper.Wallpaper is TWallPaper { Document: TDocument wallPaperDocument })
                {
                    wallPaperDocument.Attributes =
                        documentAttributeVideoResponseService.ToLayeredData(wallPaperDocument.Attributes, layer);
                }

                break;

            case TUpdatePhoneCall updatePhoneCall:
                updatePhoneCall.PhoneCall =
                    phoneCallResponseService.ToLayeredData(updatePhoneCall.PhoneCall, layer);
                break;

            case TUpdateQuickReplyMessage updateQuickReplyMessage:
                updateQuickReplyMessage.Message =
                    messageResponseService.ToLayeredData(updateQuickReplyMessage.Message, layer);
                break;

            case TUpdateServiceNotification updateServiceNotification:
                updateServiceNotification.Media =
                    messageMediaResponseService.ToLayeredData(updateServiceNotification.Media, layer);
                break;

            case TUpdateStarsRevenueStatus:
                break;

            case TUpdateStory updateStory:
                if (updateStory.Story is TStoryItem storyItem)
                {
                    storyItem.Media = messageMediaResponseService.ToLayeredData(storyItem.Media, layer);
                }

                break;
            case TUpdateTheme updateTheme:
                if (updateTheme.Theme is TTheme { Document: TDocument document })
                {
                    document.Attributes =
                        documentAttributeVideoResponseService.ToLayeredData(document.Attributes, layer);
                }

                break;
            case TUpdateUserEmojiStatus updateUserEmojiStatus:
                updateUserEmojiStatus.EmojiStatus =
                    emojiStatusResponseService.ToLayeredData(updateUserEmojiStatus.EmojiStatus, layer);
                break;
            case TUpdateWebPage updateWebPage:
                if (updateWebPage.Webpage is TWebPage { Document: TDocument webPageDocument })
                {
                    webPageDocument.Attributes =
                        documentAttributeVideoResponseService.ToLayeredData(webPageDocument.Attributes, layer);
                }

                break;
        }

        return update;
    }
}