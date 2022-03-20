using Microsoft.AspNetCore.Mvc;

namespace ToGShop.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Problem()
        {
            return View();
        }
    }
}
