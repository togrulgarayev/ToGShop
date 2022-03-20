using System.Collections.Generic;
using System.Threading.Tasks;
using Business.ViewModels.ContactAdminViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IContactAdminService
    {
        Task<List<ContactAdmin>> GetAllAsync();
        Task Create(ContactAdminViewModel contactAdminViewModel, ApplicationUser appUser);
        Task Remove(int id);
    }
}
