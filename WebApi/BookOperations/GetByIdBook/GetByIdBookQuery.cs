using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetByIdBook
{
    public class GetByIdBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId;

        public GetByIdBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BooksViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(book => book.Id == id).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
                
            }
            BooksViewModel vm = _mapper.Map<BooksViewModel>(book); //new BooksViewModel();

            //vm.Title = book.Title;
            //vm.Genre = ((GenreEnum)book.GenreId).ToString();
            //vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            //vm.PageCount = book.PageCount;
            
            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }

    }
}