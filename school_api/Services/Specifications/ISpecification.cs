using System.Linq.Expressions;

namespace school_api.Services.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? ToExpression();
    }
}
