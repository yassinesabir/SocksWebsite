using SocksWebsite.Data.Base;
using SocksWebsite.Data.ViewModels;
using SocksWebsite.Models;

namespace SocksWebsite.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task AddNewProductAsync(NewProductM data);
        Task UpdateProductAsync(NewProductM data);
    }
}