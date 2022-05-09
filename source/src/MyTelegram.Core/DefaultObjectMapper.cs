namespace MyTelegram.Core;

public class DefaultObjectMapper<TContext> : DefaultObjectMapper, IObjectMapper<TContext>
{
    public DefaultObjectMapper(
        IServiceProvider serviceProvider
    ) : base(
        serviceProvider)
    {
    }
}

public class DefaultObjectMapper : IObjectMapper //, ITransientDependency
{
    public DefaultObjectMapper(
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }

    [return: NotNullIfNotNull("source")]
    public virtual TDestination? Map<TSource, TDestination>(TSource source)
    {
        if (source == null)
        {
            return default;
        }

        var specificMapper = ServiceProvider.GetService<IObjectMapper<TSource, TDestination>>();
        if (specificMapper != null)
        {
            return specificMapper.Map(source);
        }

        throw new InvalidOperationException(
            $"Can not map source type {typeof(TSource).FullName} to destination type {typeof(TDestination).FullName}");
    }

    [return: NotNullIfNotNull("source")]
    public virtual TDestination? Map<TSource, TDestination>(TSource source,
        TDestination destination)
    {
        if (source == null)
        {
            return default;
        }

        var specificMapper = ServiceProvider.GetService<IObjectMapper<TSource, TDestination>>();
        if (specificMapper != null)
        {
            return specificMapper.Map(source, destination);
        }

        throw new InvalidOperationException(
            $"Can not map source type {typeof(TSource).FullName} to destination type {typeof(TDestination).FullName}");
    }
}

