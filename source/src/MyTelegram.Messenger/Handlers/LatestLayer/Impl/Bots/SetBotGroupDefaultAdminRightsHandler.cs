// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Set the default <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested admin rights</a> for bots being added as admins to groups, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 RIGHTS_NOT_MODIFIED The new admin rights are equal to the old rights, no change was made.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/bots.setBotGroupDefaultAdminRights" />
///</summary>
internal sealed class SetBotGroupDefaultAdminRightsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotGroupDefaultAdminRights, IBool>,
    Bots.ISetBotGroupDefaultAdminRightsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotGroupDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}
