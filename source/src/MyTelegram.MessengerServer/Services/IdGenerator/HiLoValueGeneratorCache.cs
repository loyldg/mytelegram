namespace MyTelegram.MessengerServer.Services.IdGenerator;

public class HiLoValueGeneratorCache : IHiLoValueGeneratorCache
{
    //private readonly int DefaultBlockSize = 10000;
    private readonly IHiLoStateBlockSizeHelper _stateBlockSizeHelper;
    private readonly ConcurrentDictionary<IdType, ConcurrentDictionary<long, HiLoValueGeneratorState>> _states = new();

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
}
