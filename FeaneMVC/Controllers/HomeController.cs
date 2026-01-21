using System.Diagnostics;
using System.Threading.Tasks;
using FeaneMVC.Contexts;
using FeaneMVC.Models;
using FeaneMVC.ViewModels.FoodViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeaneMVC.Controllers
{
    public class HomeController(ILogger<HomeController> _logger, FeaneDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var foods = await _context.Foods.Select(x => new FoodGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name
            }).ToListAsync();

            return View(foods);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
