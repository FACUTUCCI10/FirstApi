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
        private readonly IBookRepository _repository;
        public BooksController(IBookRepository repository)
        {
            //inyectamos el contexto de la base de datos a través del constructor
            _repository = repository;
        }

        [HttpGet] //devuelve una lista de libros
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _repository.GetAll();
            return Ok(books);
        }

        //GET: api/Books/5
        [HttpGet("{id}")] // PARAMETRO ID para buscar un libro
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _repository.GetDetailsById(id);

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
            await _repository.Insert(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        //PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var bookInDb = await _repository.GetDetailsById(id);

            if (bookInDb == null)
            {
                return NotFound();
            }

            await _repository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _repository.GetDetailsById(id);

            if (book == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return book;
        }
    }
}
