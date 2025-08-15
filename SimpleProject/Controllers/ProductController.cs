using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleProject.Models;
using SimpleProject.Services.Interfaces;


namespace SimpleProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, IFileService fileService, ICategoryService categoryService)
        {
            _productService = productService;
            _fileService = fileService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            //return ControllerContext.MyDisplayRouteInfo();
            var products = await _productService.GetProducts();
            ViewBag.Title = "Product List";
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var path = "";
                    if (model.File?.Length > 0)
                    {
                        path = await _fileService.Upload(model.File, "/Images/");
                        if (path == "Error uploading file")
                        {
                            return BadRequest();
                        }
                    }
                    model.Path = path;
                    var result = await _productService.AddProduct(model);
                    if (result != "Success")
                    {
                        ModelState.AddModelError(string.Empty, result);
                        return View(model);
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

                return View(model);

            }
            catch (Exception)
            {
                ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != model.Id)
                    {
                        return BadRequest("Product ID mismatch.");
                    }
                    var product = await _productService.GetProductById(id);
                    if (product == null) NotFound();
                    var path = model.Path;
                    if (model.File?.Length > 0)
                    {
                        _fileService.DeletePhysicalFile(path);
                        path = await _fileService.Upload(model.File, "/Images/");
                        if (path == "Error uploading file")
                        {
                            return BadRequest();
                        }
                    }
                    product.Path = path;
                    product.Name = model.Name;
                    product.Price = model.Price;

                    var result = await _productService.UpdateProduct(product);
                    if (result != "success")
                    {
                        ModelState.AddModelError(string.Empty, result);
                        return View(model);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productService.DeleteProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> IsProductNameExist(string Name)
        {
            var result = await _productService.IsProductNameExistAsync(Name);
            if (result) return Json(false);
            return Json(true);
        }
    }
}
