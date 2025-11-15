using MyTelegram.Converters.Responses.Interfaces.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain a list of active, expired or cancelled <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscriptions »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarsSubscriptions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarsSubscriptionsHandler(ILayeredService<IStarsStatusResponseConverter> starsStatusLayeredService) : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsSubscriptions, MyTelegram.Schema.Payments.IStarsStatus>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsStatus> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarsSubscriptions obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IStarsStatus>(starsStatusLayeredService.GetConverter(input.Layer).ToLayeredData(new TStarsStatus { Balance = new TStarsAmount(), Chats = [], Users = [] }));
    }
}