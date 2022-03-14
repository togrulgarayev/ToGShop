using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class ProductOperationService : IProductOperationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductOperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductOperation>> GetAllAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId==userId);
        }

        public async Task<List<ProductOperation>> GetAllFavouriteAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.IsFavourite == true);
        }

        public async Task<List<ProductOperation>> GetAllCartAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.InCart == true);
        }

        public async Task<List<ProductOperation>> GetAllOrderedAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.IsOrdered == true);
        }

        public async Task<List<ProductOperation>> GetAllSendAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.IsSend == true);
        }

        public async Task<ProductOperation> Get(int id)
        {
            return await _unitOfWork.productOperationsRepository.Get(po => po.Id == id && po.IsDeleted == false);
        }

        public async Task SetFavourite(int productId, string userid)
        {

            var productOperation = new ProductOperation()
            {
                ProductId = productId,
                ApplicationUserId = userid,
                IsFavourite = true
            };

            await _unitOfWork.productOperationsRepository.CreateAsync(productOperation);
            await _unitOfWork.SaveAsync();
        }
        

        public async Task SetCart(int productId, string userid)
        {
            var productOperation = new ProductOperation()
            {
                ProductId = productId,
                ApplicationUserId = userid,
                InCart = true
            };

            await _unitOfWork.productOperationsRepository.CreateAsync(productOperation);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetOrdered(int productId, string userid)
        {
            var productOperation = new ProductOperation()
            {
                ProductId = productId,
                ApplicationUserId = userid,
                IsOrdered = true
            };

            await _unitOfWork.productOperationsRepository.CreateAsync(productOperation);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetSend(int productId, string userid)
        {
            var productOperation = new ProductOperation()
            {
                ProductId = productId,
                ApplicationUserId = userid,
                IsSend = true
            };

            await _unitOfWork.productOperationsRepository.CreateAsync(productOperation);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFavourite(int productId, string userid)
        {
            var dbProductOperation =
                await _unitOfWork.productOperationsRepository.Get(po =>
                    po.ProductId == productId && po.ApplicationUserId == userid && po.IsFavourite==true);

            dbProductOperation.IsFavourite = false;

             _unitOfWork.productOperationsRepository.Update(dbProductOperation);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCart(int productId, string userid)
        {
            var dbProductOperation =
                await _unitOfWork.productOperationsRepository.Get(po =>
                    po.ProductId == productId && po.ApplicationUserId == userid && po.InCart == true);

            dbProductOperation.InCart = false;

            _unitOfWork.productOperationsRepository.Update(dbProductOperation);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteOrdered(int productId, string userid)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSend(int productId, string userid)
        {
            throw new NotImplementedException();
        }
    }
}
