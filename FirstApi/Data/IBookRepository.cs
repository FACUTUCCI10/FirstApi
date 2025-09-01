using FirstApi.Models;

namespace FirstApi.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetDetailsById(int id);
        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(int id);
    }
}
