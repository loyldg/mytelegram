using System.Linq.Expressions;

namespace MyTelegram;

/// <summary>
/// Reads one slice of a list sorted by id.
/// </summary>
/// <param name="filter">Filter of the slice</param>
/// <param name="skip">Number of items to skip</param>
/// <param name="limit">Max number of items to return, always greater than 0</param>
/// <param name="ascending">True to sort by ascending id, otherwise by descending id</param>
public delegate Task<IReadOnlyCollection<TResult>> FindOffsetSliceAsync<TSource, TResult>(
    Expression<Func<TSource, bool>> filter,
    int skip,
    int limit,
    bool ascending);

/// <summary>
/// Executes offset_id/add_offset based pagination described by <see cref="OffsetInfo"/>.
/// </summary>
public static class OffsetPagination
{
    /// <summary>
    /// Applies the min_id/max_id/min_date/max_date bounds.
    /// The bounds are applied before slicing, which is equivalent to filtering after slicing
    /// for the requests sent by official clients and allows clients (e.g. iOS) to page with offset_id=0 and max_id.
    /// </summary>
    public static Expression<Func<T, bool>> ApplyFilters<T>(Expression<Func<T, bool>> predicate,
        OffsetInfo offset,
        Expression<Func<T, int>> idSelector,
        Expression<Func<T, int>>? dateSelector)
    {
        if (offset.MinId > 0)
        {
            predicate = And(predicate, Compare(idSelector, ExpressionType.GreaterThan, offset.MinId));
        }

        if (offset.MaxId > 0)
        {
            predicate = And(predicate, Compare(idSelector, ExpressionType.LessThan, offset.MaxId));
        }

        if (dateSelector != null)
        {
            if (offset.MinDate > 0)
            {
                predicate = And(predicate, Compare(dateSelector, ExpressionType.GreaterThan, offset.MinDate));
            }

            if (offset.MaxDate > 0)
            {
                predicate = And(predicate, Compare(dateSelector, ExpressionType.LessThan, offset.MaxDate));
            }
        }

        return predicate;
    }

    /// <summary>
    /// Returns the page sorted by descending id, the slices may be projected to <typeparamref name="TResult"/>.
    /// </summary>
    public static async Task<List<TResult>> FindAsync<T, TResult>(OffsetInfo offset,
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, int>> idSelector,
        Expression<Func<T, int>>? dateSelector,
        FindOffsetSliceAsync<T, TResult> findSlice)
    {
        var filter = ApplyFilters(predicate, offset, idSelector, dateSelector);
        Expression<Func<T, bool>>? newerFilter = null;
        Expression<Func<T, bool>> olderFilter;

        if (offset.HasIdAnchor)
        {
            newerFilter = And(filter, Compare(idSelector, ExpressionType.GreaterThanOrEqual, offset.OffsetId));
            olderFilter = And(filter, Compare(idSelector, ExpressionType.LessThan, offset.OffsetId));
        }
        else if (offset.HasDateAnchor && dateSelector != null)
        {
            newerFilter = And(filter, Compare(dateSelector, ExpressionType.GreaterThan, offset.OffsetDate));
            olderFilter = And(filter, Compare(dateSelector, ExpressionType.LessThanOrEqual, offset.OffsetDate));
        }
        else
        {
            // No anchor: the anchor is above the newest item, so the newer slice is always empty.
            olderFilter = filter;
        }

        IReadOnlyCollection<TResult> newer = [];
        IReadOnlyCollection<TResult> older = [];
        if (newerFilter != null && offset.NewerTake > 0)
        {
            newer = await findSlice(newerFilter, offset.NewerSkip, offset.NewerTake, true);
        }

        if (offset.OlderTake > 0)
        {
            older = await findSlice(olderFilter, offset.OlderSkip, offset.OlderTake, false);
        }

        var result = new List<TResult>(newer.Count + older.Count);
        result.AddRange(newer.Reverse());
        result.AddRange(older);

        return result;
    }

    public static Expression<Func<T, bool>> Compare<T>(Expression<Func<T, int>> selector,
        ExpressionType comparison,
        int value)
    {
        var body = Expression.MakeBinary(comparison, selector.Body, Expression.Constant(value));
        return Expression.Lambda<Func<T, bool>>(body, selector.Parameters[0]);
    }

    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = left.Parameters[0];
        var rightBody = new ReplaceParameterVisitor(right.Parameters[0], parameter).Visit(right.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightBody), parameter);
    }

    private sealed class ReplaceParameterVisitor(ParameterExpression source, ParameterExpression target)
        : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == source ? target : base.VisitParameter(node);
        }
    }
}
