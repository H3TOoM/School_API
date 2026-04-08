using System.Linq;

namespace school_api.Services.Sorting
{
    public interface ISortStrategy<T>
    {
        bool CanApply(string sortBy);
        IOrderedQueryable<T> Apply(IQueryable<T> query, bool descending);
    }
}
