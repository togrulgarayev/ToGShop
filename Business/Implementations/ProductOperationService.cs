using System.Collections.Generic;
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

        public async Task<List<ProductOperation>> GetAllFavouriteProductAsync()
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po=>po.IsFavourite==true);
        }

        public async Task<List<ProductOperation>> GetAllOrderProductAsync()
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == true);
        }

        public async Task<List<ProductOperation>> GetAllCartAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.InCart == true);
        }

        public async Task<List<ProductOperation>> GetAllProductOrderedAsync()
        {
            var returnProductOperations= await _unitOfWork
                .productOperationsRepository
                .GetAllAsync(po => po.IsDeleted == false && po.IsOrdered == true, "ApplicationUser");

            return returnProductOperations;
        }

        public async Task<List<ProductOperation>> GetAllProductSendAsync()
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.IsSend == true, "ApplicationUser");
        }


        public async Task<List<ProductOperation>> GetAllOrderedAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.IsDeleted == false && po.ApplicationUserId == userId && po.IsOrdered == true);
        }

        public async Task<List<ProductOperation>> GetAllOrderedSendAsync(string userId)
        {
            return await _unitOfWork.productOperationsRepository.GetAllAsync(po => po.ApplicationUserId==userId && po.IsFavourite==false && po.InCart==false);
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


        public async Task SetSend(int id)
        {

            var dbOrdered= await _unitOfWork.productOperationsRepository.Get(p=>p.Id==id);

            dbOrdered.IsOrdered = false;
            dbOrdered.IsSend = true;

            _unitOfWork.productOperationsRepository.Update(dbOrdered);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var dbProductOperation = await _unitOfWork.productOperationsRepository.Get(p => p.Id == id);

            dbProductOperation.IsDeleted = true;

            _unitOfWork.productOperationsRepository.Update(dbProductOperation);
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
        
    }
}
