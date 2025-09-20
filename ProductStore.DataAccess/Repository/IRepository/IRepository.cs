using System.Linq.Expressions;

namespace ProductStore.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
    }
}
