using Microsoft.AspNetCore.Mvc;

namespace MM_Jewellers.Controllers
{
    public class WholesalerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
