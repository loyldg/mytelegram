namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 SAVED_ID_EMPTY The passed inputSavedStarGiftChat.saved_id is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.craftStarGift"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CraftStarGiftHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCraftStarGift, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestCraftStarGift obj)
    {
        throw new NotImplementedException();
    }
}