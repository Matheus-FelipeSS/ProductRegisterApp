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
                Console.WriteLine($"[ERRO INDEX] {ex.Message}");
                return Content("Erro ao carregar a lista de produtos.");
            }
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Criar Produto";

            var produto = new Product
            {
                DataEntrega = DateOnly.FromDateTime(DateTime.Now),
                Validade = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
            };

            return View("Form", produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
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

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewData["Title"] = "Editar Produto";
            return View("Form", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.IdProduct)
                return NotFound();

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

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewData["Title"] = "Excluir Produto";
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);

            TempData["Mensagem"] = "🗑️ Produto excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finish(int id)
        {
            await _productService.FinishAsync(id);

            TempData["Mensagem"] = "✅ Produto marcado como finalizado.";
            return RedirectToAction(nameof(Index));
        }
    }
}
