using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Data.DAL;

namespace Data.Repositories.Implementations
{
    public class BrandRepository : Repository<Brand>, IBrandInterface
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
