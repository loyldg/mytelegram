namespace MyTelegram.Messenger.Services.Impl;

public class ChannelAppService(IQueryProcessor queryProcessor,
    IReadModelCacheHelper<IChannelReadModel> channelReadModelCacheHelper,
    IRpcErrorHelper rpcErrorHelper,
    IReadModelCacheHelper<IChannelFullReadModel> channelFullReadModelCacheHelper) :
    ReadModelWithCacheAppService<IChannelReadModel>(channelReadModelCacheHelper),
    IChannelAppService, ITransientDependency
{
    public Task<IChannelFullReadModel?> GetChannelFullAsync(long channelId)
    {
        return channelFullReadModelCacheHelper.GetOrCreateAsync(channelId,
            () => queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(channelId)), p => p.Id);
    }

    protected override Task<IChannelReadModel?> GetReadModelAsync(long id)
    {
        return queryProcessor.ProcessAsync(new GetChannelByIdQuery(id));
    }

    protected override string GetReadModelId(IChannelReadModel readModel) => readModel.Id;

    protected override long GetReadModelKey(IChannelReadModel readModel) => readModel.ChannelId;
    protected override Task<IChannelReadModel?> CreateNonExistsReadModelAsync(long id)
    {
        return Task.FromResult<IChannelReadModel?>(null);
    }

    protected override Task<IReadOnlyCollection<IChannelReadModel>> GetReadModelListAsync(List<long> ids)
    {
        return queryProcessor.ProcessAsync(new GetChannelByChannelIdListQuery(ids));
    }

    public async Task<bool> IsChannelMemberAsync(long userId, long channelId)
    {
        var channelMemberReadModel = await queryProcessor
            .ProcessAsync(new GetChannelMemberByUserIdQuery(channelId, userId));

        return channelMemberReadModel != null;
    }



    public async Task<bool> SendRpcErrorIfNotChannelMemberAsync(IRequestInput input, long channelId)
    {
        var channelReadModel = await GetAsync(channelId);
        return await SendRpcErrorIfNotChannelMemberAsync(input, channelReadModel);
    }

    public async Task<bool> SendRpcErrorIfNotChannelMemberAsync(IRequestInput input, IChannelReadModel channelReadModel)
    {
        if (string.IsNullOrEmpty(channelReadModel.UserName) &&
            channelReadModel is { Broadcast: false, LinkedChatId: null })
        {
            if (!await IsChannelMemberAsync(input.UserId, channelReadModel.ChannelId))
            {
                await rpcErrorHelper.ThrowRpcErrorAsync(input, RpcErrors.RpcErrors400.ChannelPrivate);

                return true;
            }
        }

        return false;
    }

    protected override bool IsValidReadModel(IChannelReadModel readModel)
    {
        return !readModel.IsDeleted;
    }
}