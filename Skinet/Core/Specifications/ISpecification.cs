using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    // Criteria of the thing we are going to get
    Expression<Func<T, bool>> Criteria { get; }
    // For our includes
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPaginationEnabled { get; }
}