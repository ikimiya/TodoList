using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class TaskTagsService
    {

        private readonly AppDbContext _context;

        public TaskTagsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskTags>> GetAllTaskTags()
        {
            return (await _context.TaskTags.ToListAsync());
        }

        public async Task<TaskTags?> GetById (int id)
        {
            return await _context.TaskTags.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        public async Task<TaskTags> Create (TaskTags taskTags)
        {
            await _context.TaskTags.AddAsync(taskTags);
            await _context.SaveChangesAsync();
            return taskTags;
        }

        public async Task<bool> Delete (int id)
        {
            var taskTags = await GetById(id);
            if(taskTags == null)
            {
                return false;
            }
            _context.TaskTags.Remove(taskTags);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update (TaskTags taskTags)
        {
            var index = await _context.TaskTags.FindAsync(taskTags.TaskId);
            if(index == null)
            {
                return false;
            }
            _context.TaskTags.Update(taskTags);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
