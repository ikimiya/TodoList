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

        public async Task<List<Tasks>> GetAllTasks(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && !t.IsDeleted)
                .ToListAsync();

        }

        public async Task<List<Tasks>> GetDeletedTasks(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && t.IsDeleted)
                .ToListAsync();
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

        public async Task<bool> Delete(int id)
        {
            var task = await GetbyId(id);
            if (task == null) return false;

            task.IsDeleted = true;
            task.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Tasks task)
        {
            var existing = await _context.Tasks.FindAsync(task.Id);
            if (existing == null)
            {
                return false;
            }



            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.Priority = task.Priority;
            existing.DueDate = task.DueDate;
            existing.CategoryId = task.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
