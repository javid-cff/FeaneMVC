using System.Threading.Tasks;
using FeaneMVC.Contexts;
using FeaneMVC.Models;
using FeaneMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeaneMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController(FeaneDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Select(x => new CategoryVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Category category = new Category()
            {
                Id = vm.Id,
                Name = vm.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return BadRequest();

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return BadRequest();

            CategoryVM vm = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existCategory = await _context.Categories.FindAsync(vm.Id);

            if (existCategory == null)
                return BadRequest();

            //existCategory.Id = vm.Id;
            existCategory.Name = vm.Name;

            _context.Categories.Update(existCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
