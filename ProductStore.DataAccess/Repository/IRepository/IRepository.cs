using System.Linq.Expressions;

namespace ProductStore.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
    }
}
