// Контекст базы данных
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp3
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }  // Таблица студентов

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Укажите строку подключения к базе данных PostgreSQL
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=students;Username=postgres;Password=postgres");
        }
    }
}