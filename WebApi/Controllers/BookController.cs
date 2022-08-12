
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

using WebApi.BookOperations.GetByIdBook;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController :ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly olması sadece constructor ile setlenmesine izin verir.
        private readonly IMapper _mapper;
        
        public BookController(BookStoreDbContext context,IMapper mapper )
        {
            _context = context;
            _mapper = mapper;


        }
        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

            

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdBookQuery query = new GetByIdBookQuery(_context,_mapper);
            try
            {
                GetByIdBookQueryValidator validator = new GetByIdBookQueryValidator();
                query.BookId = id;
                validator.ValidateAndThrow(query);
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
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                //ValidationResult result =
                validator.ValidateAndThrow(command);
                command.Handle();

                //if (!result.IsValid)
                //    foreach (var item in result.Errors)
                //        Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message:" + item.ErrorMessage);
                //else
                //    
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
           

            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
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