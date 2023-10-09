// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns localized text of a text message with an invitation.
/// See <a href="https://corefork.telegram.org/method/help.getInviteText" />
///</summary>
internal sealed class GetInviteTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetInviteText, MyTelegram.Schema.Help.IInviteText>,
    Help.IGetInviteTextHandler
{
    protected override Task<MyTelegram.Schema.Help.IInviteText> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetInviteText obj)
    {
        throw new NotImplementedException();
    }
}
