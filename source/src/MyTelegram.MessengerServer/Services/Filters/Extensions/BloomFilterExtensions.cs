////using MyTelegram.MessengerServer.Services.BloomFilter.InMemory;
////using MyTelegram.MessengerServer.Services.BloomFilter.Redis;

//using MyTelegram.MessengerServer.Services.BloomFilter.InMemory;

//namespace MyTelegram.MessengerServer.Services.BloomFilter.Extensions;

//public static class BloomFilterExtensions
//{
//    public static void AddInMemoryBloomFilter(this IServiceCollection services)
//    {
//        services.AddSingleton<IBloomFilter, InMemoryBloomFilter>();
//        services.AddSingleton<ICuckooFilter, CuckooFilter>();
//    }

//    //public static void AddRedisBloomFilter(this IServiceCollection services)
//    //{
//    //    services.AddSingleton<IBloomFilter, MyRedisBloomFilter>();
//    //    services.AddSingleton<ICuckooFilter, RedisCuckooFilter>();
//    //}
//}


