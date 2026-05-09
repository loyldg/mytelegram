using System.Linq.Expressions;

namespace MyTelegram.EventFlow;

internal class SubstExpressionVisitor : ExpressionVisitor
{
    public Dictionary<Expression, Expression> Subst = new();

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return Subst.GetValueOrDefault(node, node);
    }
}
