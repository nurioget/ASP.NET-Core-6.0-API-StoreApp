using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contrats;
using Services.Contrats;
using System;
using System.Collections.Generic;
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

        public BookManager(IRepositoryManager manager, ILogerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task <BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity=_mapper.Map<Book>(bookDto);
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

        public async Task <(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            if (!bookParameters.ValidPriceRange)
                throw new PriceOutoOfRangeBadRequestException();

            var bookWithMetaData= await _manager
                .Book
                .GetAllBooksAsync(bookParameters,trackChanges);

           var booksDto = _mapper.Map<IEnumerable<BookDto>>(bookWithMetaData);
            return (booksDto,bookWithMetaData.MetaData);
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
