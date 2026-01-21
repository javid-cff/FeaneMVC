using System.Threading.Tasks;
using FeaneMVC.Contexts;
using FeaneMVC.Helpers;
using FeaneMVC.Models;
using FeaneMVC.ViewModels;
using FeaneMVC.ViewModels.FoodViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FeaneMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class FoodController : Controller
    {
        private readonly FeaneDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _folderPath;

        public FoodController(FeaneDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _context.Foods.Select(x => new FoodGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Price = x.Price,
                CategoryName = x.Category.Name
            }).ToListAsync();

            return View(foods);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _sendCategoriesWithViewBag();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodCreateVM vm)
        {
            await _sendCategoriesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "This Category is not found!");
                return View(vm);
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Image format is invalid!");
                return View(vm);
            }

            string uniqueImagePath = await vm.Image.FileUploadAsync(_folderPath);

            Food food = new Food()
            {
                Name = vm.Name,
                Description = vm.Description,
                ImagePath = uniqueImagePath,
                Price = vm.Price,
                CategoryId = vm.CategoryId
            };

            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
                return BadRequest();

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            string deletedImagePath = Path.Combine(_folderPath, food.ImagePath);
            FileHelper.FileDelete(deletedImagePath);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
                return BadRequest();

            FoodUpdateVM vm = new FoodUpdateVM()
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                CategoryId = food.CategoryId
            };

            await _sendCategoriesWithViewBag();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FoodUpdateVM vm)
        {
            await _sendCategoriesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "This Category is not found!");
                return View(vm);
            }

            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "Image format is invalid!");
                return View(vm);
            }

            var existFood = await _context.Foods.FindAsync(vm.Id);

            if (existFood == null)
                return BadRequest();

            existFood.Id = vm.Id;
            existFood.Name = vm.Name;
            existFood.Description = vm.Description;
            existFood.Price = vm.Price;
            existFood.CategoryId = vm.CategoryId;

            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);

                string deletedImagePath = Path.Combine(_folderPath, existFood.ImagePath);
                FileHelper.FileDelete(deletedImagePath);

                existFood.ImagePath = newImagePath;
            }

            _context.Foods.Update(existFood);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task _sendCategoriesWithViewBag()
        {
            var categories = await _context.Categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();

            ViewBag.Categories = categories;
        }
    }
}
