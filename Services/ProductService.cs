using Microsoft.EntityFrameworkCore;
using ProductControl.Models;
using ProductControl.Contexts;

namespace ProductControl.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductControlContext _context;

        public ProductService(ProductControlContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .OrderBy(p => p.FinalizadoEm.HasValue)
                .ThenBy(p => p.Validade)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.Products.FindAsync(id);
            if (todo != null)
            {
                _context.Products.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task FinishAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null && !product.FinalizadoEm.HasValue)
            {
                product.FinalizadoEm = DateOnly.FromDateTime(DateTime.Now);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetByLojaIdAsync(int idLoja)
        {
            return await _context.Products
                .Where(p => p.IdLoja == idLoja)
                .OrderBy(p => p.FinalizadoEm.HasValue)
                .ThenBy(p => p.Validade)
                .ToListAsync();
        }
    }
}
