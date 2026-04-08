using System.Linq.Expressions;

namespace school_api.Services.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }

    internal class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public Expression<Func<T, bool>>? ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            if (leftExpression == null) return rightExpression;
            if (rightExpression == null) return leftExpression;

            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(leftExpression.Parameters[0], parameter);
            var rightVisitor = new ReplaceExpressionVisitor(rightExpression.Parameters[0], parameter);

            var leftBody = leftVisitor.Visit(leftExpression.Body);
            var rightBody = rightVisitor.Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(leftBody, rightBody), parameter);
        }
    }

    internal class OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public Expression<Func<T, bool>>? ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            if (leftExpression == null) return rightExpression;
            if (rightExpression == null) return leftExpression;

            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(leftExpression.Parameters[0], parameter);
            var rightVisitor = new ReplaceExpressionVisitor(rightExpression.Parameters[0], parameter);

            var leftBody = leftVisitor.Visit(leftExpression.Body);
            var rightBody = rightVisitor.Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(leftBody, rightBody), parameter);
        }
    }

    internal class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public Expression<Func<T, bool>>? ToExpression()
        {
            var expression = _specification.ToExpression();
            if (expression == null) return null;

            var parameter = Expression.Parameter(typeof(T));
            var visitor = new ReplaceExpressionVisitor(expression.Parameters[0], parameter);
            var body = visitor.Visit(expression.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Not(body), parameter);
        }
    }

    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression? Visit(Expression? node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }
}
