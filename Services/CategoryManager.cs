﻿using Entities.Exceptions;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await _manager
                .Category
                .GetAllCategoriesAsync(trackChanges);
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {

            var category = await _manager
                .Category
                .GetOneCategoryByIdAsync(id, trackChanges);

            if (category is null)
                throw new CategoryNotFoundException(id);
            return category;
        }
    }
}
