using Microsoft.AspNetCore.Mvc;

namespace ProductStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
