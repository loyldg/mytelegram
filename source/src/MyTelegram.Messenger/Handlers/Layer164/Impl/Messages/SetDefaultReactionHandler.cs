// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Change default emoji reaction to use in the quick reaction menu: the value is synced across devices and can be fetched using <a href="https://corefork.telegram.org/method/help.getConfig">help.getConfig, <code>reactions_default</code> field</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 REACTION_INVALID The specified reaction is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.setDefaultReaction" />
///</summary>
internal sealed class SetDefaultReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultReaction, IBool>,
    Messages.ISetDefaultReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetDefaultReaction obj)
    {
        throw new NotImplementedException();
    }
}
