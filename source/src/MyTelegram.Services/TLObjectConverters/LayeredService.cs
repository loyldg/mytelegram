using MyTelegram.Core;

namespace MyTelegram.Services.TLObjectConverters;

public class LayeredService<TLayeredConverter> : ILayeredService<TLayeredConverter> where TLayeredConverter : ILayeredConverter
{
    private readonly IEnumerable<TLayeredConverter> _converters;
    public LayeredService(TLayeredConverter converter,
        IEnumerable<TLayeredConverter> converters)
    {
        Converter = converter;
        //_converters = converters;
        _converters = converters.OrderByDescending(p => p.Layer);
    }

    public TLayeredConverter Converter { get; }
    public int MinLayer => 158;

    public TLayeredConverter GetConverter(int layer)
    {
        var converter = Converter;

        if (layer == 0)
        {
            //Converter.RequestLayer = layer;
            converter.RequestLayer = layer;
            return converter;
        }

        // latest layer converter
        foreach (var layeredConverter in _converters)
        {
            // if we can not find converter.Layer==layer,return the latest layer where converter.Layer<layer
            if (layeredConverter.Layer == layer)
            {
                converter = layeredConverter;
                break;
            }

            if (layeredConverter.Layer < layer)
            {
                converter = layeredConverter;
                break;
            }
            //else if (layeredConverter.Layer > layer)
            //{
            //    break;
            //}
        }
        converter.RequestLayer = layer;
        return converter;
    }

    //public LayeredData GetLayeredData(Func<TLayeredConverter, IObject> func)
    //{
    //    return new LayeredData(GetLayeredData<IObject>(func).DataWithLayer);
    //}

    public LayeredData<TData> GetLayeredData<TData>(Func<TLayeredConverter, TData> func)
    {
        Dictionary<int, TData>? data = null;
        foreach (var layeredConverter in _converters)
        {
            if (layeredConverter.Layer < MinLayer)
            {
                continue;
            }

            //if (layeredConverter.Layer != Converter.Layer)
            {
                data ??= new();
                data.TryAdd(layeredConverter.Layer, func(layeredConverter));
            }
        }

        return new LayeredData<TData>(data);
    }
}
