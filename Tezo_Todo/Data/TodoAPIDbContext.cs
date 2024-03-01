//using Microsoft.EntityFrameworkCore;
//using Tezo_Todo.Models;

//namespace Tezo_Todo.Data
//{
//    public class TodoAPIDbContext : DbContext
//    {
//        public DbSet<User> User { get; set; }
//        public DbSet<Assignment> Assignment { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseNpgsql(@"Host=localhost; Database = TezoTodo; TrustServerCertificate=True; Port = 5432; Username = postgres; Password=895185;");
//        }

//        public TodoAPIDbContext(DbContextOptions options) : base(options)
//        {
//        }
//    }
//}
