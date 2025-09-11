namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Returns localized text of a text message with an invitation.
/// See <a href="https://corefork.telegram.org/method/help.getInviteText" />
///</summary>
internal sealed class GetInviteTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetInviteText, MyTelegram.Schema.Help.IInviteText>
{
    protected override Task<IInviteText> HandleCoreAsync(IRequestInput input,
        RequestGetInviteText obj)
    {
        IInviteText r = new TInviteText { Message = @"{0} invite you to use mytelegram." };

        return Task.FromResult(r);
    }
}
