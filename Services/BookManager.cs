using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contrats;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Exceptions.BadRequestException;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogerService _logger;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;

        public BookManager(IRepositoryManager manager, ILogerService logger, IMapper mapper, IBookLinks bookLinks = null)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _bookLinks = bookLinks;
        }

        public async Task <BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var category = _manager
                .Category
                .GetOneCategoryByIdAsync(bookDto.CategoryId, false);

            if(category is null)
                throw new CategoryNotFoundException(bookDto.CategoryId);

            var entity=_mapper.Map<Book>(bookDto);
            entity.CategoryId= bookDto.CategoryId;

            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity=await GetOneBookByIdAndChechExits(id,trackChanges);
            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse LinkResponse, MetaData metaData)>
             GetAllBooksAsync(LinkParameters linkParameters,
             bool trackChanges)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
                throw new PriceOutoOfRangeBadRequestException();

            var booksWithMetaData = await _manager
                .Book
                .GetAllBooksAsync(linkParameters.BookParameters, trackChanges);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            var links=_bookLinks.TryGenerateLinks(booksDto,
                linkParameters.BookParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse:links, metaData: booksWithMetaData.MetaData);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _manager
                .Book
                .GetAllBooksWithDetailsAsync(trackChanges);
        }

        public async Task <BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book= await GetOneBookByIdAndChechExits(id, trackChanges);

            return _mapper.Map<BookDto>(book);
        }

        public async Task <(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book =await GetOneBookByIdAndChechExits(id, trackChanges);

            var bookDtoForUpdate= _mapper.Map<BookDtoForUpdate>(book);
            return(bookDtoForUpdate,book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //chechk entity
            var entity =await GetOneBookByIdAndChechExits(id, trackChanges);

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndChechExits(int id, bool trackChanges)//error checking 
        {
            //chechk entity
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (entity is null)
                throw new BookNotFoundException(id);

            return entity;
        }
    }
}
