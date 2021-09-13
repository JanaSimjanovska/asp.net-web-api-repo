using BooksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDB.Books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            try
            {
               
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id cannot be a negative number.");
                }
                if (id >= StaticDB.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no book with id {id} in the database.");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDB.Books[id]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            
        }

        [HttpGet("filterByAuthorAndTitle")]

        public ActionResult<Book> Get(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                    return StatusCode(StatusCodes.Status404NotFound, "Enter book author and/or title to get results.");

                if (string.IsNullOrEmpty(author))
                {
                    Book bookByTitleOnly = StaticDB.Books.FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));
                    if (bookByTitleOnly == null)
                        return StatusCode(StatusCodes.Status404NotFound, "There is no such book in our bookstore.");

                    return StatusCode(StatusCodes.Status200OK, bookByTitleOnly);
                }

                if (string.IsNullOrEmpty(title))
                {
                    Book bookByAuthorOnly = StaticDB.Books.FirstOrDefault(x => x.Author.ToLower().Contains(author.ToLower()));
                    if (bookByAuthorOnly == null)
                        return StatusCode(StatusCodes.Status404NotFound, "There is no such book in our bookstore.");

                    return StatusCode(StatusCodes.Status200OK, bookByAuthorOnly);
                }

                Book book = StaticDB.Books.FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()) && x.Author.ToLower().Contains(author.ToLower()));

                if (book == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Book not found");

                return StatusCode(StatusCodes.Status200OK, book);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }



        }

        // POST api/<BooksController>
        [HttpPost("postBook")]
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                StaticDB.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book added.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Please try again.");

            }
            
        }

        [HttpPost("postBooks")]
        public IActionResult PostBooks([FromBody] List<Book> books)
        {
            try
            {
              
                return StatusCode(StatusCodes.Status202Accepted, books.Select(x => x.Title));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Please try again.");

            }

        }
    }
}
