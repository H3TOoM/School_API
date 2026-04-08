using school_api.Data.Models;

namespace school_api.Services.Specifications
{
    public class StudentSpecifications
    {
        public class BySearchTerm : ISpecification<Student>
        {
            private readonly string _searchTerm;

            public BySearchTerm(string searchTerm)
            {
                _searchTerm = searchTerm;
            }

            public System.Linq.Expressions.Expression<Func<Student, bool>>? ToExpression()
            {
                return s => s.Name.Contains(_searchTerm) || (s.Email != null && s.Email.Contains(_searchTerm));
            }
        }

        public class ByDepartment : ISpecification<Student>
        {
            private readonly int _departmentId;

            public ByDepartment(int departmentId)
            {
                _departmentId = departmentId;
            }

            public System.Linq.Expressions.Expression<Func<Student, bool>>? ToExpression()
            {
                return s => s.StudentClass.DepartmentId == _departmentId;
            }
        }

        public class ByStudentClass : ISpecification<Student>
        {
            private readonly int _studentClassId;

            public ByStudentClass(int studentClassId)
            {
                _studentClassId = studentClassId;
            }

            public System.Linq.Expressions.Expression<Func<Student, bool>>? ToExpression()
            {
                return s => s.StudentClassId == _studentClassId;
            }
        }

        public class ByParent : ISpecification<Student>
        {
            private readonly int _parentId;

            public ByParent(int parentId)
            {
                _parentId = parentId;
            }

            public System.Linq.Expressions.Expression<Func<Student, bool>>? ToExpression()
            {
                return s => s.ParentId == _parentId;
            }
        }

        public class ByDateOfBirthRange : ISpecification<Student>
        {
            private readonly DateTime? _from;
            private readonly DateTime? _to;

            public ByDateOfBirthRange(DateTime? from, DateTime? to)
            {
                _from = from;
                _to = to;
            }

            public System.Linq.Expressions.Expression<Func<Student, bool>>? ToExpression()
            {
                if (_from.HasValue && _to.HasValue)
                    return s => s.DateOfBirth >= _from.Value && s.DateOfBirth <= _to.Value;
                if (_from.HasValue)
                    return s => s.DateOfBirth >= _from.Value;
                if (_to.HasValue)
                    return s => s.DateOfBirth <= _to.Value;
                return null;
            }
        }
    }
}
