using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.ContactAdminViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Business.Implementations
{
    public class ContactAdminService:IContactAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactAdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<ContactAdmin>> GetAllAsync()
        {
            return await _unitOfWork.contactAdminRepository.GetAllAsync();
        }

        public async Task Create(ContactAdminViewModel contactAdminViewModel, ApplicationUser appUser)
        {


            var newMsg = new ContactAdmin()
            {
                Username = appUser.UserName,
                Email = appUser.Email,
                Message = contactAdminViewModel.Message,
                Fullname = appUser.FullName
            };

            await _unitOfWork.contactAdminRepository.CreateAsync(newMsg);
            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            ContactAdmin dbContactAdmin = await _unitOfWork.contactAdminRepository.Get(c => c.Id == id);



            _unitOfWork.contactAdminRepository.Remove(dbContactAdmin);
            await _unitOfWork.SaveAsync();
        }
    }
}
