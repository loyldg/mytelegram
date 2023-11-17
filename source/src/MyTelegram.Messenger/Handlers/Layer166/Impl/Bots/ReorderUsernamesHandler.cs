// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Reorder usernames associated to a bot we own.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/bots.reorderUsernames" />
///</summary>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestReorderUsernames, IBool>,
    Bots.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
