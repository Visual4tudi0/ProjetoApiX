using System.Linq.Expressions;

namespace ClassLibraryDomain.IRepository
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}