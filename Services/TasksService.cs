using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class TasksService
    {
        private readonly AppDbContext _context;

        public TasksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return (await _context.Tasks.ToListAsync());
        }

        public async Task<Tasks?> GetbyId (int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tasks> Create (Tasks task)
        {
            task.CreatedAt = DateTime.UtcNow;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }
        
        public async Task<bool> Delete (int id)
        {
            var task = await GetbyId(id);
            if(task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update (Tasks task)
        {
            var index = await _context.Tasks.FindAsync(task.Id);
            if(index == null)
            {
                return false;
            }

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return true;

        }


    }
}
