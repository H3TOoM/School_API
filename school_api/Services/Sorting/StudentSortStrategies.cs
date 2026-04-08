using school_api.Data.Models;
using System.Linq;

namespace school_api.Services.Sorting
{
    public class StudentSortStrategies
    {
        public class ByName : ISortStrategy<Student>
        {
            public bool CanApply(string sortBy) => sortBy.Equals("name", StringComparison.OrdinalIgnoreCase);

            public IOrderedQueryable<Student> Apply(IQueryable<Student> query, bool descending)
            {
                return descending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
            }
        }

        public class ByDateOfBirth : ISortStrategy<Student>
        {
            public bool CanApply(string sortBy) => sortBy.Equals("dateofbirth", StringComparison.OrdinalIgnoreCase);

            public IOrderedQueryable<Student> Apply(IQueryable<Student> query, bool descending)
            {
                return descending ? query.OrderByDescending(s => s.DateOfBirth) : query.OrderBy(s => s.DateOfBirth);
            }
        }

        public class Default : ISortStrategy<Student>
        {
            public bool CanApply(string sortBy) => true;

            public IOrderedQueryable<Student> Apply(IQueryable<Student> query, bool descending)
            {
                return query.OrderBy(s => s.Name);
            }
        }
    }
}
