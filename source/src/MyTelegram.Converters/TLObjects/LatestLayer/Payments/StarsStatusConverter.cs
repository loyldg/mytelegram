// ReSharper disable All

using MyTelegram.Schema.Payments;

namespace MyTelegram.Converters.TLObjects.Payments;

internal sealed class StarsStatusConverter : IStarsStatusConverter, ITransientDependency
{
    public int Layer => Layers.LayerLatest;

    public IStarsStatus ToStarsStatus(bool ton)
    {
        if (ton)
        {
            return new TStarsStatus
            {
                Balance = new TStarsTonAmount
                {
                    Amount = 10000000
                },
                Chats = [],
                History = [],
                Users = []
            };
        }

        return new TStarsStatus
        {
            Balance = new TStarsAmount
            {
                Amount = 10000000
            },
            Chats = [],
            History = [],
            Users = []
        };
    }
}
