using SocksWebsite.Data.Enums;
using SocksWebsite.Data.Services;
using SocksWebsite.Data.Static;
using SocksWebsite.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocksWebsite.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync();
            var viewModelProducts = allProducts.Select(p => new NewProductM
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageURL = p.ImageURL,
                ProductCategory = p.ProductCategory,
                ProductSubCategory = p.ProductSubCategory,
                ProductSize = p.ProductSize,
                Quantity = p.Quantity
            }).ToList();

            return View(viewModelProducts);
        }




        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts
                    .Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower()))
                    .ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }

        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);

            var viewModel = new NewProductM
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                Description = productDetail.Description,
                Price = productDetail.Price,
                ImageURL = productDetail.ImageURL,
                ProductCategory = productDetail.ProductCategory,
                ProductSubCategory = productDetail.ProductSubCategory,
                ProductSize = productDetail.ProductSize,
                Quantity = productDetail.Quantity
            };

            return View(viewModel);
        }

        //GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductM product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }

            var response = new NewProductM
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                ProductCategory = (ProductCategory)productDetails.ProductCategory,
                ProductSubCategory = (ProductSubCategory)productDetails.ProductSubCategory,
                ProductSize = productDetails.ProductSize,
                Quantity = productDetails.Quantity
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductM product)
        {
            if (id != product.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
