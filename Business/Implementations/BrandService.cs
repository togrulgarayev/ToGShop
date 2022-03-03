using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels;
using Core;
using Core.Entities;
using Core.Interfaces;

namespace Business.Implementations
{
    public class BrandService : IBrandService
    {


        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<Brand>> GetAllAsync()
        {
            return await _unitOfWork.brandRepository.GetAllAsync(b=> !b.IsDeleted);
        }

        public async Task Create(BrandCreateViewModel brandViewModel)
        {
            var newBrand = new Brand()
            {
                Name = brandViewModel.Name
            };

            await _unitOfWork.brandRepository.CreateAsync(newBrand);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, BrandUpdateViewModel brandViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
