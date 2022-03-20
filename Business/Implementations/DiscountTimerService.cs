using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.DiscountTimerViewModel;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class DiscountTimerService:IDiscountTimerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountTimerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DiscountTimer> Get()
        {
            var dbDiscountTimer = await _unitOfWork.discountTimerRepository.GetAllAsync();

            return dbDiscountTimer.FirstOrDefault();
        }

        public async Task Update(DiscountTimerViewModel discountTimerViewModel)
        {
            var dbDiscountTimer = await _unitOfWork.discountTimerRepository.GetAllAsync();

            var discountTimer = dbDiscountTimer.FirstOrDefault();

            discountTimer.DiscountTime = discountTimerViewModel.DiscountTime;
            discountTimer.DiscountTittle = discountTimerViewModel.DiscountTitle;

            _unitOfWork.discountTimerRepository.Update(discountTimer);
            await _unitOfWork.SaveAsync();

        }
    }
}
