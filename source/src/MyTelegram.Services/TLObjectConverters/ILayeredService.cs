using MyTelegram.Core;

namespace MyTelegram.Services.TLObjectConverters;

public interface ILayeredService<out TLayeredConverter>
    where TLayeredConverter : ILayeredConverter
{
    /// <summary>
    /// base converter
    /// </summary>
    TLayeredConverter Converter { get; }
    int MinLayer { get; }
    TLayeredConverter GetConverter(int layer);
    //LayeredData GetLayeredData(Func<TLayeredConverter, IObject> func);
    LayeredData<TData> GetLayeredData<TData>(Func<TLayeredConverter, TData> func);
}