namespace MyTelegram.Messenger.Services.IdGenerator;

public class HiLoValueGeneratorCache : IHiLoValueGeneratorCache
{
    private readonly ConcurrentDictionary<IdType, ConcurrentDictionary<long, HiLoValueGeneratorState>> _states = new();
    //private readonly int DefaultBlockSize = 10000;
    private readonly IHiLoStateBlockSizeHelper _stateBlockSizeHelper;

    public HiLoValueGeneratorCache(IHiLoStateBlockSizeHelper stateBlockSizeHelper)
    {
        _stateBlockSizeHelper = stateBlockSizeHelper;
    }

    public HiLoValueGeneratorState GetOrAdd(IdType idType,
        long key)
    {
        if (!_states.TryGetValue(idType, out var stateList))
        {
            var state = new HiLoValueGeneratorState(_stateBlockSizeHelper.GetBlockSize(idType));
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

            state = new HiLoValueGeneratorState(_stateBlockSizeHelper.GetBlockSize(idType));
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