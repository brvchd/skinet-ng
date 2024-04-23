using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    // Criteria of the thing we are going to get
    Expression<Func<T, bool>> Criteria { get; }
    
    // For our includes
    List<Expression<Func<T, object>>> Includes { get; }
}