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
            try
            {
                return await _context.Products
                    .OrderBy(p => p.FinalizadoEm.HasValue)
                    .ThenBy(p => p.Validade)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao carregar produtos");
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao buscar produto {id}");
            }
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.UtcNow;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao adicionar produto");
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao atualizar produto");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao excluir produto {id}");
            }
        }

        public async Task FinishAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null && !product.FinalizadoEm.HasValue)
                {
                    product.FinalizadoEm = DateOnly.FromDateTime(DateTime.Now);
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao finalizar produto {id}");
            }
        }

        public async Task<List<Product>> GetByLojaIdAsync(int idLoja)
        {
            try
            {
                return await _context.Products
                    .Where(p => p.IdLoja == idLoja)
                    .OrderBy(p => p.FinalizadoEm.HasValue)
                    .ThenBy(p => p.Validade)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao carregar produtos da loja");
            }
        }
    }
}