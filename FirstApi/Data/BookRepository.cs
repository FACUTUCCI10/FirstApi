using Dapper;
using FirstApi.Models;
using System.Data;
using System.Data.Common;

namespace FirstApi.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnection _dbconnection;
        public BookRepository(IDbConnection connection)
        {
            _dbconnection = connection;
        }

        public async Task Insert(Book book)
        {
            var consulta = @"INSERT INTO Books (Id,Title, Author, IsAvailable)
                                VALUES (@Id,@Title, @Author, @IsAvailable);";

            // Fix: Use ExecuteAsync from Dapper, which does not require a generic type parameter
            await _dbconnection.ExecuteAsync(consulta, new
            {
                book.Id,
                book.Title,
                book.Author,
                book.IsAvailable
            });
        }

        public async Task Delete(int id)
        {
            var consulta = @"Delete from Books 
                                   WHERE Id = @Id;";



            await _dbconnection.ExecuteAsync(consulta, new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var consulta = @"SELECT Id,
                                    Title,
                                    Author,
                                    IsAvailable
                                    FROM Books";
            return await _dbconnection.QueryAsync<Book>(consulta, new { });
        }

        public async Task<Book?> GetDetailsById(int id)
        {
            var consulta = @"SELECT Id,
                                    Title,
                                    Author,
                                    IsAvailable
                                    FROM Books
                                    where Id = @Id";

            return await _dbconnection.QueryFirstOrDefaultAsync<Book>(consulta, new { Id = id });
        }

        public async Task Update(Book book)
        {
            var consulta = @"UPDATE Books SET  
                                   Title = @Title,
                                   Author = @Author, 
                                   IsAvailable = @IsAvailable
                                   WHERE Id = @Id;";   


            
            await _dbconnection.ExecuteAsync(consulta, new
            {
                book.Id,
                book.Title,
                book.Author,
                book.IsAvailable
            });
        }
    }
}
