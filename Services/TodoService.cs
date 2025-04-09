using Microsoft.EntityFrameworkCore;
using TWTodos.Contexts;
using TWTodos.Models;

namespace TWTodos.Services
{
    public class TodoService : ITodoService
    {
        private readonly TWTodosContext _context;

        public TodoService(TWTodosContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos
                .OrderBy(t => t.FinishedAt.HasValue)
                .ThenBy(t => t.DeadLine)
                .ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task AddAsync(Todo todo)
        {
            todo.CreatedAt = DateTime.Now;
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task FinishAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null && !todo.FinishedAt.HasValue)
            {
                todo.FinishedAt = DateOnly.FromDateTime(DateTime.Now);
                _context.Todos.Update(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
