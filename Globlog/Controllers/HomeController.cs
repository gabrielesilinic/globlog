using Globlog.Data;
using Globlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Globlog.Controllers
{
    public class HomeController : Controller
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            //_userManager = userManager;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/test")]
        public IActionResult test()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Title"] = "Test";
            return View("test1",ViewData);
        }
        [Route("/makeExampleUsr")]
        public IActionResult makeExampleUsr()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Title"] = "Test";
            var user = new AppUser
            {
                //Id = Guid.NewGuid(),
                UserName = "test88",
                Email = "usr55@example.com",
                DisplayName = "Test User n3",
            };
            var result = _userManager.CreateAsync(user, "Password123!").Result;
            //return ok in plain text
            return Content("ok" + result.ToString());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

    }
}