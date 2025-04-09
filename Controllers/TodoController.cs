using Microsoft.AspNetCore.Mvc;
using TWTodos.Models;
using TWTodos.Services;

namespace TWTodos.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Lista de Tarefas";
            var todos = await _todoService.GetAllAsync();
            return View(todos);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Criar Tarefa";
            return View("Form", new Todo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddAsync(todo);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Criar Tarefa";
            return View("Form", todo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
                return NotFound();

            ViewData["Title"] = "Editar Tarefa";
            return View("Form", todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Todo todo)
        {
            if (id != todo.IdProduct)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _todoService.UpdateAsync(todo);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "Editar Tarefa";
            return View("Form", todo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
                return NotFound();

            ViewData["Title"] = "Excluir Tarefa";
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _todoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finish(int id)
        {
            await _todoService.FinishAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
