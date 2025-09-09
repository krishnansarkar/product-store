using ProductStore.Models;

namespace ProductStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRespository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
