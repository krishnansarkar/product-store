using Microsoft.EntityFrameworkCore;
using ProductStore.DataAccess.Data;
using System.Linq.Expressions;

namespace ProductStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
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

        public T Get(Expression<Func<T, bool>> expression, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(expression);
            query = IncludePropertiesInQuery(query, includeProperties);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = IncludePropertiesInQuery(query, includeProperties);
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        private IQueryable<T> IncludePropertiesInQuery(IQueryable<T> query, string? includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query;
        }
    }
}
