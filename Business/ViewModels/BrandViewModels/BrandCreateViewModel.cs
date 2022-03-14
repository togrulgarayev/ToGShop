using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Business.ViewModels.BrandViewModels
{
    public class BrandCreateViewModel
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
