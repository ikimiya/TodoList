using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class CategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            return (await _context.Categories.ToListAsync());
        }

        public async Task<Categories?> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categories> Create(Categories categories)
        {
            await _context.Categories.AddAsync(categories);
            await _context.SaveChangesAsync();
            return categories;
        }

        public async Task<bool> Delete(int id)
        {
            var categories = await GetById(id);
            if (categories == null)
            {
                return false;
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Categories categories)
        {
            var index = await _context.Categories.FindAsync(categories.Id);

            if (index == null)
            {
                return false;
            }

            _context.Categories.Update(categories);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
