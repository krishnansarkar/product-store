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
        IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = GetCategoryList()
            };

            if (id != null && id != 0)
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
            }

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, int? id, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string productPath = Path.Combine(wwwrootPath, "images", "products");

                    if (productVM.Product.ImageUrl != null)
                    {
                        DeleteImage(productVM.Product.ImageUrl);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(productPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        productVM.Product.ImageUrl = Path.Combine("\\images", "products", fileName);
                    }
                }

                if (id != null && id != 0)
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = $"Product \"{productVM.Product.Title}\" updated successfully.";
                }
                else
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = $"Product \"{productVM.Product.Title}\" created successfully.";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            productVM.CategoryList = GetCategoryList();

            return View(productVM);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            if (product.ImageUrl != null)
                DeleteImage(product.ImageUrl);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = $"Product \"{product.Title}\" deleted successfully."
            }
            );
        }

        private IEnumerable<SelectListItem> GetCategoryList()
        {
            return _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        private void DeleteImage(string imageUrl)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwrootPath, imageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
    }
}
