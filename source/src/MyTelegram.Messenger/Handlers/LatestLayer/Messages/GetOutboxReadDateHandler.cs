namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get the exact read date of one of our messages, sent to a private chat with another user.Can be only done for private outgoing messages not older than <a href="https://corefork.telegram.org/api/config#pm-read-date-expire-period">appConfig.pm_read_date_expire_period »</a>.If the <code>peer</code>'s <a href="https://corefork.telegram.org/constructor/userFull">userFull</a>.<code>read_dates_private</code> flag is set, we will not be able to fetch the exact read date of messages we send to them, and a <code>USER_PRIVACY_RESTRICTED</code> RPC error will be emitted.<br>
/// The exact read date of messages might still be unavailable for other reasons, see <a href="https://corefork.telegram.org/constructor/globalPrivacySettings">here »</a> for more info.<br>
/// To set <a href="https://corefork.telegram.org/constructor/userFull">userFull</a>.<code>read_dates_private</code> for ourselves invoke <a href="https://corefork.telegram.org/method/account.setGlobalPrivacySettings">account.setGlobalPrivacySettings</a>, setting the <code>settings.hide_read_marks</code> flag.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_NOT_READ_YET The specified message wasn't read yet.
/// 400 MESSAGE_TOO_OLD The message is too old, the requested information is not available.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// 403 YOUR_PRIVACY_RESTRICTED You cannot fetch the read date of this message because you have disallowed other users to do so for <em>your</em> messages; to fix, allow other users to see <em>your</em> exact last online date OR purchase a <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> subscription.
/// See <a href="https://corefork.telegram.org/method/messages.getOutboxReadDate" />
///</summary>
internal sealed class GetOutboxReadDateHandler(
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    IPeerHelper peerHelper,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetOutboxReadDate, MyTelegram.Schema.IOutboxReadDate>
{
    protected override async Task<MyTelegram.Schema.IOutboxReadDate> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetOutboxReadDate obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        if (peer.PeerType == PeerType.User)
        {
            var userReadModel = await userAppService.GetAsync(input.UserId);
            if (!userReadModel?.Premium ?? false)
            {
                var selfPrivacy = await privacyAppService.GetGlobalPrivacySettingsAsync(input.UserId);
                if (selfPrivacy?.HideReadMarks ?? false)
                {
                    RpcErrors.RpcErrors403.YourPrivacyRestricted.ThrowRpcError();
                }

                var toPeerPrivacy = await privacyAppService.GetGlobalPrivacySettingsAsync(peer.PeerId);
                if (toPeerPrivacy?.HideReadMarks ?? false)
                {
                    RpcErrors.RpcErrors403.UserPrivacyRestricted.ThrowRpcError();
                }
            }
        }

        var date = await queryProcessor.ProcessAsync(new GetOutboxReadDateQuery(input.UserId, obj.MsgId, peer));
        var diff = CurrentDate - date;
        if (diff > MyTelegramConsts.ChatReadMarkExpirePeriod)
        {
            RpcErrors.RpcErrors400.MessageTooOld.ThrowRpcError();
        }

        return new TOutboxReadDate
        {
            Date = date
        };
    }
}
