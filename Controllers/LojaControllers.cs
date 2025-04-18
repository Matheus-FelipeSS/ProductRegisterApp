using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductControl.Contexts;
using ProductControl.Models;

public class LojaController : Controller
{
    private readonly ProductControlContext _context;

    public LojaController(ProductControlContext context)
    {
        _context = context;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError("Email", "Informe um email válido.");
            return View();
        }

        email = email.Trim().ToLower();

        var loja = await _context.Lojas.FirstOrDefaultAsync(l => l.Email.ToLower() == email);
        if (loja == null)
        {
            return RedirectToAction("Create");
        }

        HttpContext.Session.SetInt32("LojaId", loja.IdLoja);
        HttpContext.Session.SetString("Email", loja.Email);
        HttpContext.Session.SetString("LojaNome", loja.Nome);

        return RedirectToAction("Index", "Product");
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Loja loja)
    {
        if (!ModelState.IsValid)
            return View(loja);

        loja.Email = loja.Email.Trim().ToLower();

        var lojaExistente = await _context.Lojas
            .FirstOrDefaultAsync(l => l.Email.ToLower() == loja.Email);

        if (lojaExistente != null)
        {
            ModelState.AddModelError("Email", "Este email já está cadastrado.");
            return View(loja);
        }

        _context.Lojas.Add(loja);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("LojaId", loja.IdLoja);
        HttpContext.Session.SetString("Email", loja.Email);

        return RedirectToAction("Index", "Product");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Loja");
    }
}
