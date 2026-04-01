namespace MyTelegram.Messenger.Services.Impl;

public class UserAppService(IQueryProcessor queryProcessor,
    IReadModelCacheHelper<IUserReadModel> userReadModelCacheHelper,
    IReadModelCacheHelper<IUserFullReadModel> userFullReadModelCacheHelper) : ReadModelWithCacheAppService<IUserReadModel>(userReadModelCacheHelper), IUserAppService, ITransientDependency
{
    public async Task CheckAccountPremiumStatusAsync(long userId)
    {
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(userId));
        if (userReadModel == null)
        {
            RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        }

        if (!userReadModel!.Premium)
        {
            RpcErrors.RpcErrors400.PremiumAccountRequired.ThrowRpcError();
        }
    }


    public Task<IUserFullReadModel?> GetUserFullAsync(long userId)
    {
        return userFullReadModelCacheHelper.GetOrCreateAsync(userId,
            () => queryProcessor.ProcessAsync(new GetUserFullQuery(userId)), p => p.Id);
    }

    public override async Task<IUserReadModel> GetAsync(long id, bool throwIfNotExists = true)
    {
        var userReadModel = await base.GetAsync(id, false);
        if (userReadModel?.IsDeleted ?? false)
        {
            userReadModelCacheHelper.Remove(userReadModel.Id);
        }

        if (throwIfNotExists)
        {
            if (userReadModel == null)
            {
                RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            }
        }

        return userReadModel!;
    }

    protected override Task<IUserReadModel?> GetReadModelAsync(long id)
    {
        return queryProcessor.ProcessAsync(new GetUserByIdQuery(id));
    }

    protected override string GetReadModelId(IUserReadModel readModel) => readModel.Id;

    protected override long GetReadModelKey(IUserReadModel readModel) => readModel.UserId;
    protected override Task<IUserReadModel?> CreateNonExistsReadModelAsync(long id)
    {
        return Task.FromResult<IUserReadModel?>(new UserReadModel { UserId = id, IsDeleted = true, Id = UserId.Create(id).Value });
    }

    protected override Task<IReadOnlyCollection<IUserReadModel>> GetReadModelListAsync(List<long> ids)
    {
        return queryProcessor.ProcessAsync(new GetUsersByUserIdListQuery(ids));
    }
}