namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set global privacy settings
/// Possible errors
/// Code Type Description
/// 400 AUTOARCHIVE_NOT_AVAILABLE The autoarchive setting is not available at this time: please check the value of the <a href="https://corefork.telegram.org/api/config#client-configuration">autoarchive_setting_available field in client config »</a> before calling this method.
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.setGlobalPrivacySettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetGlobalPrivacySettingsHandler(ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetGlobalPrivacySettings, MyTelegram.Schema.IGlobalPrivacySettings>
{
    protected override async Task<IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSetGlobalPrivacySettings obj)
    {
        var command = new UpdateGlobalPrivacySettingsCommand(UserId.Create(input.UserId), input.ToRequestInfo(), new GlobalPrivacySettings(obj.Settings.ArchiveAndMuteNewNoncontactPeers, obj.Settings.KeepArchivedUnmuted, obj.Settings.KeepArchivedFolders, obj.Settings.HideReadMarks, obj.Settings.NewNoncontactPeersRequirePremium, obj.Settings.NoncontactPeersPaidStars));
        await commandBus.PublishAsync(command);
        return new TGlobalPrivacySettings
        {
            ArchiveAndMuteNewNoncontactPeers = obj.Settings.ArchiveAndMuteNewNoncontactPeers,
            HideReadMarks = obj.Settings.HideReadMarks,
            KeepArchivedFolders = obj.Settings.KeepArchivedFolders,
            KeepArchivedUnmuted = obj.Settings.KeepArchivedUnmuted,
            NewNoncontactPeersRequirePremium = obj.Settings.NewNoncontactPeersRequirePremium,
            NoncontactPeersPaidStars = obj.Settings.NoncontactPeersPaidStars
        };
    }
}