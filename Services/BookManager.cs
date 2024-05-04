using Entities.Models;
using Repositories.Contrats;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogerService _logger;

        public BookManager(IRepositoryManager manager, ILogerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            
            
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            //chechk entity
            var entity=_manager.Book.GetOneBookById(id,trackChanges);
            if (entity is null)
            {
                string mesage=($"The book with id:{id} colud not found");
                _logger.LogInfo(mesage);
                throw new Exception(mesage);
            }

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _manager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            //chechk entity
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Book with id:{id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }

            //check params
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            entity.Title = book.Title;
            entity.Price = book.Price;
            
            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
