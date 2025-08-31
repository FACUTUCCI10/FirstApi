using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Data
{
    public class BooksDb:DbContext
    {
        // Constructor que recibe las opciones de configuración para la base de datos
        public BooksDb(DbContextOptions<BooksDb>options): base(options)
        {

        }

        // Representa la tabla de libros en la base de datos
        public DbSet<Book> Books => Set<Book>();
    }
}
