using Microsoft.EntityFrameworkCore;
using Tezo.Todo.Models;

namespace Tezo.Todo.Data
{
    public class TodoAPIDbContext : DbContext // context class responsible for interacting with the database.
    {
        //provides access to the table.
        public DbSet<User> User { get; set; }
        public DbSet<Assignment> Assignment { get; set; }

        // This method is called by Entity Framework Core when the context is being configured.
        // Here, it configures the database connection using Npgsql provider for PostgreSQL.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost; Database = TezoTodo; TrustServerCertificate=True; Port = 5432; Username = postgres; Password=895185;");
        }

        // This constructor accepts an instance of DbContextOptions and passes it to the base DbContext constructor.
        public TodoAPIDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
