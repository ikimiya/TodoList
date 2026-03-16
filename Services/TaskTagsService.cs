using Microsoft.AspNetCore.Mvc;
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



    }
}
