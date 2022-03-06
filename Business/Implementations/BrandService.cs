using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.BrandViewModels;
using Core;
using Core.Entities;

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
            Brand dbBrand = await _unitOfWork.brandRepository.Get(b => b.Id == id);



            _unitOfWork.brandRepository.Remove(dbBrand);
            await _unitOfWork.SaveAsync();
        }
    }
}
