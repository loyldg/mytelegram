using System.Collections.Concurrent;

namespace MyTelegram.Services.Services.IdGenerator;

public class HiLoValueGeneratorCache(IHiLoStateBlockSizeHelper stateBlockSizeHelper) : IHiLoValueGeneratorCache, ISingletonDependency
{
    private readonly ConcurrentDictionary<IdType, ConcurrentDictionary<long, HiLoValueGeneratorState>> _states = new();
    //private readonly int DefaultBlockSize = 10000;

    public bool Exists(IdType idType, long key)
    {
        if (!_states.TryGetValue(idType, out var stateList))
        {
            return false;
        }

        return stateList.ContainsKey(key);
    }

    public HiLoValueGeneratorState GetOrAdd(IdType idType,
        long key)
    {
        if (!_states.TryGetValue(idType, out var stateList))
        {
            var state = new HiLoValueGeneratorState(stateBlockSizeHelper.GetBlockSize(idType));
            stateList = new ConcurrentDictionary<long, HiLoValueGeneratorState>();
            stateList.TryAdd(key, state);
            _states.TryAdd(idType, stateList);
            return state;
        }
        else
        {
            if (stateList.TryGetValue(key, out var state))
            {
                return state;
            }

            state = new HiLoValueGeneratorState(stateBlockSizeHelper.GetBlockSize(idType));
            stateList.TryAdd(key, state);
            return state;
        }
    }

    public async Task<HiLoValueGeneratorState> GetOrAddAsync(IdType idType, long key, Func<Task<HiLoValueGeneratorState>> createStateFactory)
    {
        if (!_states.TryGetValue(idType, out var stateList))
        {
            var state = await createStateFactory();
            stateList = new();
            stateList.TryAdd(key, state);

            _states.TryAdd(idType, stateList);

            return state;
        }
        else
        {
            if (stateList.TryGetValue(key, out var state))
            {
                return state;
            }

            state = await createStateFactory();
            stateList.TryAdd(key, state);

            return state;
        }
    }
}