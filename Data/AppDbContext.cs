using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<TaskTags> TaskTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskTags>()
                .HasKey(tt => new { tt.TaskId, tt.TagId });
        }
    }
}

/*** 
 From package maanger console
#createdatabase from apdb
Add-Migration InitialCreate
Update-Database

# clear database
Drop-Database
Update-Database

 ***/