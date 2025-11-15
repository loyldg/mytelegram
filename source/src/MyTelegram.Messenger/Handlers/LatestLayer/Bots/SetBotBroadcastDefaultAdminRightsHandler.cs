namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Set the default <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested admin rights</a> for bots being added as admins to channels, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
/// Possible errors
/// Code Type Description
/// 400 RIGHTS_NOT_MODIFIED The new admin rights are equal to the old rights, no change was made.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.setBotBroadcastDefaultAdminRights"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetBotBroadcastDefaultAdminRightsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotBroadcastDefaultAdminRights, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestSetBotBroadcastDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}