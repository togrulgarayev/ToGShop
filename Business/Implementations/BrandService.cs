using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.BrandViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Business.Implementations
{
    public class BrandService : IBrandService
    {


        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }


        public async Task<List<Brand>> GetAllAsync()
        {
            return await _unitOfWork.brandRepository.GetAllAsync(b=> !b.IsDeleted);
        }

        public async Task<Brand> Get(int id)
        {
            return await _unitOfWork.brandRepository.Get(b => b.Id == id && b.IsDeleted == false);
        }

        public async Task Create(BrandCreateViewModel brandViewModel)
        {
            string logo = await brandViewModel.Logo.SaveFileAsync(_env.WebRootPath, "assets", "img");

            var newBrand = new Brand()
            {
                Name = brandViewModel.Name,
                Logo = logo
            };

            await _unitOfWork.brandRepository.CreateAsync(newBrand);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, BrandUpdateViewModel brandViewModel)
        {
            Brand dbBrand = await _unitOfWork.brandRepository.Get(b => b.Id == id);

            dbBrand.Name = brandViewModel.Name;

            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Brand dbBrand = await _unitOfWork.brandRepository.Get(b => b.Id == id);



            _unitOfWork.brandRepository.Remove(dbBrand);
            await _unitOfWork.SaveAsync();
        }
    }
}
