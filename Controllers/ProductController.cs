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
            ViewData["Title"] = "Controle de Produtos";
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Criar Produto";
            return View("Form", new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Criar Produto";
            return View("Form", product);
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

    if (ModelState.IsValid)
    {
        await _productService.UpdateAsync(product);
        return RedirectToAction(nameof(Index));
    }

    ViewData["Title"] = "Editar Produto";
    return View("Form", product);
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finish(int id)
        {
            await _productService.FinishAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
