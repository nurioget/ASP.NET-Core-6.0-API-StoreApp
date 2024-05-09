using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contrats
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        Task SaveAsync();
    }
}
 