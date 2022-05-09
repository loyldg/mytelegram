namespace MyTelegram.QueryHandlers.MongoDB;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a,
        Expression<Func<T, bool>> b)
    {
        var p = a.Parameters[0];

        var visitor = new SubstExpressionVisitor
        {
            Subst =
            {
                [b.Parameters[0]] = p
            }
        };

        Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a,
        Expression<Func<T, bool>> b)
    {
        var p = a.Parameters[0];

        var visitor = new SubstExpressionVisitor
        {
            Subst =
            {
                [b.Parameters[0]] = p
            }
        };

        Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static Expression<Func<T, bool>> WhereIf<T>(this Expression<Func<T, bool>> a,
        bool condition,
        Expression<Func<T, bool>> b)
    {
        if (!condition)
        {
            return a;
        }

        return a.And(b);
    }
}
