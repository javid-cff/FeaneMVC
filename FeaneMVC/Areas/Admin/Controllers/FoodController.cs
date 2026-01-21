using Microsoft.AspNetCore.Mvc;

namespace FeaneMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class FoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
