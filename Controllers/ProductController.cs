using Microsoft.AspNetCore.Mvc;
using ProductControl.Models;
using ProductControl.Services;

namespace ProductControl.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Controle de Produtos";
                var idLoja = HttpContext.Session.GetInt32("LojaId");
                if (idLoja == null)
                    return RedirectToAction("Login", "Loja");

                var produtos = await _productService.GetByLojaIdAsync(idLoja.Value);
                return View(produtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Ocorreu um erro ao carregar os produtos";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Criar Produto";

            var produto = new Product
            {
                DataFabricacao = DateOnly.FromDateTime(DateTime.Now),
                Validade = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
            };

            return View("Form", produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["Title"] = "Criar Produto";
                    return View("Form", product);
                }

                var idLoja = HttpContext.Session.GetInt32("LojaId");
                if (idLoja == null)
                    return RedirectToAction("Login", "Loja");

                product.IdLoja = idLoja.Value;
                product.CreatedAt = DateTime.Now;

                await _productService.AddAsync(product);

                TempData["Mensagem"] = "✅ Produto cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao cadastrar o produto";
                return View("Form", product);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";
                    return RedirectToAction(nameof(Index));
                }

                ViewData["Title"] = "Editar Produto";
                return View("Form", product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao carregar o produto";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                if (id != product.IdProduct)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";
                    return RedirectToAction(nameof(Index));
                }

                if (!ModelState.IsValid)
                {
                    ViewData["Title"] = "Editar Produto";
                    return View("Form", product);
                }

                var idLoja = HttpContext.Session.GetInt32("LojaId");
                if (idLoja == null)
                    return RedirectToAction("Login", "Loja");

                product.IdLoja = idLoja.Value;

                await _productService.UpdateAsync(product);

                TempData["Mensagem"] = "✏️ Produto atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao atualizar o produto";
                return View("Form", product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";
                    return RedirectToAction(nameof(Index));
                }

                ViewData["Title"] = "Excluir Produto";
                return View(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao carregar o produto";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                TempData["Mensagem"] = "🗑️ Produto excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao excluir o produto";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Finish(int id)
        {
            try
            {
                await _productService.FinishAsync(id);
                TempData["Mensagem"] = "✅ Produto marcado como finalizado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["ErrorMessage"] = "Erro ao finalizar o produto";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}