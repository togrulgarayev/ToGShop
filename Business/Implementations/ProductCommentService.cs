using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.ProductCommentViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(ProductCommentViewModel productCommentViewModel)
        {
            var comment = new ProductComment()
            {
                Comment = productCommentViewModel.Comment,
                ProductId = productCommentViewModel.ProductId,
                Username = productCommentViewModel.Username
            };

            await _unitOfWork.productCommentRepository.CreateAsync(comment);
            await _unitOfWork.SaveAsync();
        }



        public async Task<ProductComment> Get(int id)
        {
            return await _unitOfWork.productCommentRepository.Get(c => c.Id == id);
        }



        public async Task<List<ProductComment>> GetAllAsync()
        {
            return await _unitOfWork.productCommentRepository.GetAllAsync();
        }

        public async Task Remove(int id)
        {
            var comment = await _unitOfWork.productCommentRepository.Get(p => p.IsDelete == false && p.Id == id);
            comment.IsDelete = true;
            _unitOfWork.productCommentRepository.Update(comment);
            await _unitOfWork.SaveAsync();
        }



        public async Task<List<ProductComment>> GetProductId(int id)
        {
            var pids = await _unitOfWork
                .productCommentRepository
                .GetAllAsync(p => p.ProductId == id && p.IsDelete == false);
            return pids;
        }
        
    }
}
