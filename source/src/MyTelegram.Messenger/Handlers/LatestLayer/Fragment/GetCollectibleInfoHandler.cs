using MyTelegram.Schema.Fragment;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Fragment;

///<summary>
/// Fetch information about a <a href="https://corefork.telegram.org/api/fragment#fetching-info-about-fragment-collectibles">fragment collectible, see here »</a> for more info on the full flow.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 COLLECTIBLE_INVALID The specified collectible is invalid.
/// 400 COLLECTIBLE_NOT_FOUND The specified collectible could not be found.
/// See <a href="https://corefork.telegram.org/method/fragment.getCollectibleInfo" />
///</summary>
internal sealed class GetCollectibleInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Fragment.RequestGetCollectibleInfo, MyTelegram.Schema.Fragment.ICollectibleInfo>
{
    protected override Task<MyTelegram.Schema.Fragment.ICollectibleInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Fragment.RequestGetCollectibleInfo obj)
    {
        return Task.FromResult<MyTelegram.Schema.Fragment.ICollectibleInfo>(new TCollectibleInfo
        {
            PurchaseDate = CurrentDate,
            Currency = "USD",
            Amount = 10000000,
            CryptoCurrency = "BTC",
            CryptoAmount = 1,
            Url = "https://fragment.com/"
        });
    }
}
