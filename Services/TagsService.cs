using TodoList.Data;
using TodoList.Models;
using Microsoft.EntityFrameworkCore;


namespace TodoList.Services
{
    public class TagsService
    {
        private readonly AppDbContext _context;

        public TagsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tags>> GetAllTags(int userId)
        {
            return await _context.Tags
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Tags?> GetById(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tags> Create(Tags tags)
        {
            await _context.Tags.AddAsync(tags);
            await _context.SaveChangesAsync();
            return tags;
        }

        public async Task<bool> Delete(int id)
        {
            var tagId = await GetById(id);
            if(tagId == null)
            {
                return false;
            }

            _context.Tags.Remove(tagId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Tags tags)
        {
            var index = await _context.Tags.FindAsync(tags.Id);
            if(index == null)
            {
                return false;
            }
            _context.Tags.Update(tags);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
