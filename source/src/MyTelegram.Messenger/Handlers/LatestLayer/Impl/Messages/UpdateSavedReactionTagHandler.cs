// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.updateSavedReactionTag" />
///</summary>
internal sealed class UpdateSavedReactionTagHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateSavedReactionTag, IBool>,
    Messages.IUpdateSavedReactionTagHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUpdateSavedReactionTag obj)
    {
        throw new NotImplementedException();
    }
}
