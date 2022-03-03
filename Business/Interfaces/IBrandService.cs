using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();
        Task Create(BrandCreateViewModel brandViewModel);
        Task Update(int id, BrandUpdateViewModel brandViewModel);
        Task Remove(int id);
    }
}
