using Core.Entities;
using Core.Interfaces;
using Data.DAL;

namespace Data.Repositories.Implementations
{
    public class DiscountTimerRepository : Repository<DiscountTimer>, IDiscountTimerRepository
    {
        public DiscountTimerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
    }
}
