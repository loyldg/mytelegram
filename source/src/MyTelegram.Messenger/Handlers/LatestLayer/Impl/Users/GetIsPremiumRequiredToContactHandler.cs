// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// See <a href="https://corefork.telegram.org/method/users.getIsPremiumRequiredToContact" />
///</summary>
internal sealed class GetIsPremiumRequiredToContactHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetIsPremiumRequiredToContact, TVector<bool>>,
    Users.IGetIsPremiumRequiredToContactHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    public GetIsPremiumRequiredToContactHandler(IQueryProcessor queryProcessor, IAccessHashHelper accessHashHelper)
    {
        _queryProcessor = queryProcessor;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<TVector<bool>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetIsPremiumRequiredToContact obj)
    {
        var userIds = new List<long>();
        foreach (var item in obj.Id)
        {
            if (item is TInputUser inputUser)
            {
                await _accessHashHelper.CheckAccessHashAsync(inputUser);
                userIds.Add(inputUser.UserId);
            }
        }

        var r = await _queryProcessor.ProcessAsync(new GetGlobalPrivacySettingsListQuery(userIds));

        var result = new TVector<bool>();
        foreach (var item in obj.Id)
        {
            if (item is TInputUser inputUser)
            {
                if (r.TryGetValue(inputUser.UserId, out var globalPrivacySettings))
                {
                    result.Add(globalPrivacySettings?.NewNoncontactPeersRequirePremium ?? false);
                }
                else
                {
                    result.Add(false);
                }
            }
            else
            {
                result.Add(false);
            }
        }

        return result;
    }
}
