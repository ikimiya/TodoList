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

        public async Task<List<TaskTags>> GetAllTaskTags(int userId)
        {
            return await _context.TaskTags
                .Where(tt => tt.Task.UserId == userId)
                .ToListAsync();
        }


        public async Task<List<TaskTags>> GetTagsByTaskId(int taskId, int userId)
        {
            // user verify
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
            if (task == null)
            {
                return new List<TaskTags>();
            }

            return (await _context.TaskTags
                .Where(t => t.TaskId == taskId)
                .ToListAsync());
        }

        public async Task<TaskTags?> GetById (int id)
        {
            return await _context.TaskTags.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        public async Task<bool> Create(int taskId, int tagId, int userId)
        {
            var task = await _context.Tasks
                        .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
            if (task == null) return false;

            var tag = await _context.Tags
                        .FirstOrDefaultAsync(t => t.Id == tagId && t.UserId == userId);
            if (tag == null) return false;

            var existing = await _context.TaskTags
                        .FirstOrDefaultAsync(tt => tt.TaskId == taskId && tt.TagId == tagId);
            if (existing != null) return false;

            await _context.TaskTags.AddAsync(new TaskTags 
            {   TaskId = taskId, 
                TagId = tagId 
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int taskId, int tagId, int userId)
        {
            // verify task belongs to user
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
            if (task == null) return false;

            var taskTag = await _context.TaskTags
                .FirstOrDefaultAsync(tt => tt.TaskId == taskId && tt.TagId == tagId);
            if (taskTag == null) return false;

            _context.TaskTags.Remove(taskTag);
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
