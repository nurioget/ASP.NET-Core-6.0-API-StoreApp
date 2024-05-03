﻿using Repositories.Contrats;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IserviceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public ServiceManager(IRepositoryManager repositoryManager) 
        {
            _bookService=new Lazy<IBookService>(()=>new BookManager(repositoryManager));
        }
        public IBookService BookService => _bookService.Value;
    }
}
