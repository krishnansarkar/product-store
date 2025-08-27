using Microsoft.AspNetCore.Mvc;
using ProductStore.Data;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
