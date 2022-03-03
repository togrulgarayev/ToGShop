using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Data.DAL;

namespace Data.Repositories.Implementations
{
    class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
