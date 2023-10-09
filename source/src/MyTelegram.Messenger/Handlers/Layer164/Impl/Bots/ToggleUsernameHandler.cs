// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Activate or deactivate a purchased <a href="https://fragment.com/">fragment.com</a> username associated to a bot we own.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/bots.toggleUsername" />
///</summary>
internal sealed class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestToggleUsername, IBool>,
    Bots.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}
