namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/paid-messages">paid messages »</a> in this <a href="https://corefork.telegram.org/api/channel">supergroup</a> or <a href="https://corefork.telegram.org/api/monoforum">monoforum</a>.Also used to <a href="https://corefork.telegram.org/api/monoforum">enable or disable monoforums aka direct messages in a channel</a>.Note that passing the ID of the monoforum itself to <code>channel</code> will return a <code>CHANNEL_MONOFORUM_UNSUPPORTED</code> error: pass the ID of the associated channel to edit the settings of the associated monoforum, instead.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_MONOFORUM_UNSUPPORTED <a href="https://corefork.telegram.org/api/channel#monoforums">Monoforums</a> do not support this feature.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 STARS_AMOUNT_INVALID The specified amount in stars is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.updatePaidMessagesPrice"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdatePaidMessagesPriceHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdatePaidMessagesPrice, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestUpdatePaidMessagesPrice obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Chats = [], Updates = [], Users = [] });
    }
}