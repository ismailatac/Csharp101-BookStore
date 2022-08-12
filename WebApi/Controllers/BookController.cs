
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBookCommand;
using WebApi.BookOperations;

using WebApi.BookOperations.GetByIdBookQuery;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController :ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly olması sadece constructor ile setlenmesine izin verir.
        
        public BookController(BookStoreDbContext context)
        {
            _context = context;
            
        }
        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

            

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdBookQuery query = new GetByIdBookQuery(_context);
            try
            {
                var result = query.Handle(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            
            
            
        }
        // [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();

        }
        [HttpDelete]
        public IActionResult DeleteBook (int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);

            try
            {
                command.Handle(id);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


            return Ok();
            
        }
    }



}