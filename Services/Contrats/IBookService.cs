using Entities.DataTranferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contrats
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int id,bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id,BookDtoForUpdate bookDto, bool trackChanges);
        void DeleteOneBook(int id,bool trackChanges);

    }
}
