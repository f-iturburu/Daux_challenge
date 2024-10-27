using Microsoft.AspNetCore.Mvc;

namespace Francisco_Iturburu_Daux_Challenge.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}