using EventFlow.ReadStores;
using MyTelegram.Services.Services;

namespace MyTelegram.Services.Extensions;

public static class ReadModelAppServiceExtensions
{
    extension<TReadModel>(IReadModelWithCacheAppService<TReadModel> service) where TReadModel : IReadModel
    {
        public async Task<TReadModel?> GetAsync(long? id)
        {
            if (id is null or 0)
            {
                return default;
            }

            return await service.GetAsync(id.Value);
        }

        public async Task<TReadModel?> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return default;
            }

            return await service.GetAsync(id);
        }
    }
}
