namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Update the <a href="https://corefork.telegram.org/api/colors">accent color and background custom emoji »</a> of the current account.
/// Possible errors
/// Code Type Description
/// 400 COLOR_INVALID The specified color palette ID was invalid.
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateColor"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateColorHandler(ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateColor, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateColor obj)
    {
        PeerColor? color = null;
        switch (obj.Color)
        {
            case TPeerColor peerColor:
                color = new PeerColor(peerColor.Color, peerColor.BackgroundEmojiId);
                break;
        }
        var command = new UpdateUserColorCommand(UserId.Create(input.UserId), input.ToRequestInfo(), color, obj.ForProfile);
        await commandBus.PublishAsync(command, CancellationToken.None);
        return new TBoolTrue();
    }
}