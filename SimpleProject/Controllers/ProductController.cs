using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleProject.Models;
using SimpleProject.Services.Interfaces;
using SimpleProject.ViewModels.Products;


namespace SimpleProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IFileService fileService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _fileService = fileService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? search)
        {

            //var products = await _productService.GetProducts();
            //var result = _mapper.Map<List<GetProductListViewModel>>(products);
            ViewBag.CurrentSearch = search;
            var products = _productService.GetProductsAsQueryable(search);
            var result =await _mapper.ProjectTo<GetProductListViewModel>(products).ToListAsync();

            ViewBag.Title = "Product List";
            return View(result);
        }

        public async Task<IActionResult> SearchProductList(string? searchtext)
        {

            ViewBag.CurrentSearchJQuery = searchtext;
            var products = _productService.GetProductsAsQueryable(searchtext);
            var result = await _mapper.ProjectTo<GetProductListViewModel>(products).ToListAsync();
            return PartialView("_ProductList",result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "NameEn");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(model);
                    var result = await _productService.AddProduct(product, model.Files);
                    if (result != "Success")
                    {
                        ModelState.AddModelError(string.Empty, result);
                        TempData["Failed"] = result;
                        return View(model);
                    }
                    TempData["Success"] = "Product created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

                return View(model);

            }
            catch (Exception ex)
            {
                TempData["Failed"] = ex.Message + "--" + ex.InnerException;
                ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<UpdateProductViewModel>(product);
            ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "NameEn");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != model.Id)
                    {
                        return BadRequest("Product ID mismatch.");
                    }
                    var product = await _productService.GetProductByIdWithouIncludeAsync(id);
                    if (product == null) NotFound();


                    var newProduct = _mapper.Map(model,product);

                    var result = await _productService.UpdateProduct(newProduct,model.Files);
                    if (result != "Success")
                    {
                        ModelState.AddModelError(string.Empty, result);
                        ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "NameEn");
                        return View(model);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception)
            {
                ViewData["Categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "NameEn");

                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdWithouIncludeAsync(id);
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
                var product = await _productService.GetProductByIdWithouIncludeAsync(id);
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
        public async Task<IActionResult> IsProductNameArExist(string NameAr)
        {
            var result = await _productService.IsProductNameArExistAsync(NameAr);
            if (result) return Json(false);
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> IsProductNameEnExist(string NameEn)
        {
            var result = await _productService.IsProductNameArExistAsync(NameEn);
            if (result) return Json(false);
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> IsProductNameArExistExcludeItself(string NameAr,int Id)
        {
            var result = await _productService.IsProductNameArExistExcludeItselfAsync(NameAr,Id);
            if (result) return Json(false);
            return Json(true);
        }
    }
}
