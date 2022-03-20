using Microsoft.AspNetCore.Http;

namespace Business.ViewModels.BrandViewModels
{
    public class BrandUpdateViewModel
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
