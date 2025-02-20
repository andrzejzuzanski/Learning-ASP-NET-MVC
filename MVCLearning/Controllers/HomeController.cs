using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCLearning.ApplicationUser;
using MVCLearning.Models;
using MVCLearning.Persistance;

namespace MVCLearning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserContext _userContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUserContext userContext)
        {
            _logger = logger;
            _dbContext = context;
            _userContext = userContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListItems()
        {
            var allItems = _dbContext.Items.ToList();
            return View(allItems);
        }

        public IActionResult EditItem(int? id)
        {
            var item = _dbContext.Items.FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = _dbContext.Items.FirstOrDefault(x => x.Id == id);
            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ListItems");
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> CreateForm(Item item)
        {
            item.CreatedById = _userContext.GetCurrentUser().Id;
            if (item.Id == null || item.Id == 0)
            {
                _dbContext.Items.Add(item);
            }
            else
            {
                _dbContext.Update(item);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ListItems");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
