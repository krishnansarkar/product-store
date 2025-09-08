using Microsoft.EntityFrameworkCore;
using ProductStore.DataAccess.Data;
using System.Linq.Expressions;

namespace ProductStore.DataAccess.Repository
{
    public class Repository<T> : IRespository<T> where T : class
    {
        private ApplicationDbContext _db;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(expression);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
