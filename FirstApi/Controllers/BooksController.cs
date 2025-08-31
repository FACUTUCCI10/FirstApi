using FirstApi.Data;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksDb _context;
        public BooksController(BooksDb context) 
        {
            //inyectamos el contexto de la base de datos a través del constructor
            _context = context;
        }

        [HttpGet] //devuelve una lista de libros
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
           return await _context.Books.ToListAsync();
        }

        //GET: api/Books/5
        [HttpGet("{id}")] // PARAMETRO ID para buscar un libro
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //POST: api/Books
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book>>> PostBook(Book book)
        {
            _context.Books.Add(book); 

            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        //PUT: api/Books/5
        [HttpPut("{id}")] 
        public async Task<ActionResult<IEnumerable<Book>>> PutBook(int id,Book book)
        {
            if(id!=book.Id)
            {
                return BadRequest();
            }
            
            var bookInDb = await _context.Books.FindAsync(id);

            if (bookInDb == null)
            {
                return NotFound();
            }

            bookInDb.Title = book.Title;
            bookInDb.Author = book.Author;
            bookInDb.IsAvailable = book.IsAvailable;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        // Updated the return type of the DeleteBook method to match the expected ActionResult<Book> instead of ActionResult<IEnumerable<Book>>.
        // This resolves the CS0029 error.

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }
    }
}
