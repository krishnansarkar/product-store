using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.DataAccess.Repository.IRepository;
using ProductStore.Models;
using ProductStore.Models.ViewModels;

namespace ProductStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = GetCategoryList()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = $"Product \"{productVM.Product.Title}\" created successfully.";
                return RedirectToAction("Index");
            }

            productVM.CategoryList = GetCategoryList();

            return View(productVM);
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductVM productVM = new()
            {
                Product = product,
                CategoryList = GetCategoryList()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = $"Category \"{productVM.Product.Title}\" updated successfully.";
                return RedirectToAction("Index");
            }

            productVM.CategoryList = GetCategoryList();

            return View(productVM);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductVM productVM = new()
            {
                Product = product,
                CategoryList = GetCategoryList()
            };

            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = $"Product \"{product.Title}\" deleted successfully.";
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetCategoryList()
        {
            return _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }
    }
}
