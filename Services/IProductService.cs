namespace ProductControl.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProductControl.Models;

    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task FinishAsync(int id);
        Task<List<Product>> GetByLojaIdAsync(int idLoja);
    }
}