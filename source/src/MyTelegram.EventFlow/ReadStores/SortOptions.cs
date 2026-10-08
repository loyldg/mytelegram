using System.Linq.Expressions;

namespace MyTelegram.EventFlow.ReadStores;

public record SortOptions<TReadModel>(
    Expression<Func<TReadModel, object>> Sort,
    SortType SortType = SortType.Descending)
{
    private List<SortOptions<TReadModel>>? _thenSorts;

    public IReadOnlyList<SortOptions<TReadModel>> GetAll()
    {
        if (_thenSorts == null || _thenSorts.Count == 0)
        {
            return [this];
        }

        var list = new List<SortOptions<TReadModel>>(_thenSorts.Count + 1)
        {
            this
        };
        list.AddRange(_thenSorts);
        return list;
    }

    public SortOptions<TReadModel> ThenBy(
        Expression<Func<TReadModel, object>> sort,
        SortType sortType)
    {
        _thenSorts ??= new List<SortOptions<TReadModel>>();
        _thenSorts.Add(new SortOptions<TReadModel>(sort, sortType));
        return this;
    }
}