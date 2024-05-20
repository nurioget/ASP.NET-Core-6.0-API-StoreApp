﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contrats;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            
        }

        public void CreateOneBook(Book book)=>Create(book);
       

        public void DeleteOneBook(Book book) => Delete(book);


        public void UpdateOneBook(Book book)=>Update(book);


        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice)
                .Search(bookParameters.SearchTerm)
                .Sort(bookParameters.OrderBy)  
                .ToListAsync();

            return PagedList<Book>
                .ToPagedList(books, 
                bookParameters.PageNumber,
                bookParameters.pageSize);

        }

        public async Task <Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }
    }
}
