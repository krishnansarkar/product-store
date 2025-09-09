using ProductStore.DataAccess.Data;
using ProductStore.DataAccess.Repository.IRepository;
using ProductStore.Models;

namespace ProductStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
