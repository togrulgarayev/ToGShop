using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.DiscountTimerViewModel;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IDiscountTimerService
    {
        Task<DiscountTimer> Get();
        Task Update(DiscountTimerViewModel discountTimerViewModel);
    }
}
