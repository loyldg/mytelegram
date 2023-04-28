using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTelegram.MessengerServer.Services.IdGenerator;

namespace MyTelegram.MessengerServer.Services.Impl
{
    public class IdGenerator : IIdGenerator
    {
        private readonly IHiLoValueGeneratorCache _cache;
        private readonly IHiLoValueGeneratorFactory _factory;

        public IdGenerator(IHiLoValueGeneratorCache cache,
            IHiLoValueGeneratorFactory factory)
        {
            _cache = cache;
            _factory = factory;
        }

        public async Task<int> NextIdAsync(IdType idType,
            long id,
            int step = 1,
            CancellationToken cancellationToken = default)
        {
            return (int)await NextLongIdAsync(idType, id, step, cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> NextLongIdAsync(IdType idType,
            long id = 0,
            int step = 1,
            CancellationToken cancellationToken = default)
        {
            var state = _cache.GetOrAdd(idType, id);
            var generator = _factory.Create(state);
            var nextId = await generator.NextAsync(idType, id, cancellationToken).ConfigureAwait(false);

            return nextId + GetInitId(idType);
        }

        private static long GetInitId(IdType idType)
        {
            return idType switch
            {
                IdType.ChannelId => MyTelegramServerDomainConsts.ChannelInitId,
                IdType.UserId => MyTelegramServerDomainConsts.UserIdInitId + 10000, //前10000个用户为测试用户
                IdType.BotUserId => MyTelegramServerDomainConsts.BotUserInitId,
                IdType.ChatId => MyTelegramServerDomainConsts.ChatIdInitId,
                _ => 0
            };
        }
    }
}
