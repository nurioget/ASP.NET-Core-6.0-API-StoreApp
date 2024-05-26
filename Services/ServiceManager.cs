using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contrats;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IBookService _bookService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICategoryService _categoryService;

        public ServiceManager(IBookService bookService, IAuthenticationService authenticationService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _authenticationService = authenticationService;
            _categoryService = categoryService;
        }

        public IBookService BookService => _bookService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public ICategoryService CategoryService => _categoryService;
    }
}
