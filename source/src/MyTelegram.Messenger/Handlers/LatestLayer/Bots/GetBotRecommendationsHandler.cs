using MyTelegram.Schema.Users;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Obtain a list of similarly themed bots, selected based on similarities in their subscriber bases, see <a href="https://corefork.telegram.org/api/recommend">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.getBotRecommendations"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetBotRecommendationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotRecommendations, MyTelegram.Schema.Users.IUsers>
{
    protected override Task<MyTelegram.Schema.Users.IUsers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetBotRecommendations obj)
    {
        return Task.FromResult<MyTelegram.Schema.Users.IUsers>(new TUsers { Users = [] });
    }
}