using MyTelegram.Converters.Responses.Interfaces.Payments;
using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Fetch <a href="https://corefork.telegram.org/api/stars#balance-and-transaction-history">Telegram Stars transactions</a>.The <code>inbound</code> and <code>outbound</code> flags are mutually exclusive: if none of the two are set, both incoming and outgoing transactions are fetched.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getStarsTransactions" />
///</summary>
internal sealed class GetStarsTransactionsHandler(ILayeredService<IStarsStatusResponseConverter> starsStatusLayeredService) : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsTransactions, MyTelegram.Schema.Payments.IStarsStatus>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsStatus> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsTransactions obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IStarsStatus>(starsStatusLayeredService.GetConverter(input.Layer).ToLayeredData(new TStarsStatus
        {
            Balance = new TStarsAmount(),
            Chats = [],
            Users = []
        }));
    }
}
