using SocksWebsite.Data.Base;
using SocksWebsite.Data.ViewModels;
using SocksWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace SocksWebsite.Data.Services
{
    public class ProductService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Quantity = data.Quantity,
                ImageURL = data.ImageURL,
                ProductCategory = data.ProductCategory,
                ProductSubCategory = data.ProductSubCategory,
                Price = data.Price,
                ProductSize = data.ProductSize
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return productDetails;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public async Task UpdateProductAsync(NewProductM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Quantity = data.Quantity;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.ProductCategory = data.ProductCategory;
                dbProduct.ProductSubCategory = data.ProductSubCategory;
                dbProduct.Price = data.Price;
                dbProduct.ProductSize = data.ProductSize;

                await _context.SaveChangesAsync();
            }
        }
    }
}
