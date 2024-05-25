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
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICategoryService> _categoryService;
        
        public ServiceManager(IRepositoryManager repositoryManager
            , ILogerService loger
            , IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            IBookLinks bookLinks
            
)
        {
            _bookService = new Lazy<IBookService>(() => 
            new BookManager(repositoryManager, loger, mapper, bookLinks));

            _categoryService = new Lazy<ICategoryService>(() =>
            new CategoryManager(repositoryManager));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(loger,mapper,userManager,configuration)
            );
        }
        public IBookService BookService => _bookService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ICategoryService CategoryService => _categoryService.Value;
    }
}
