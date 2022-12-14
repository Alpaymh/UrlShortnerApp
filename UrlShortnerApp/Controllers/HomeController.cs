using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using UrlShortnerApp.Business.Abstract;
using UrlShortnerApp.Models;
using UrlShortnerApps.Entities.Concrate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UrlShortnerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _customerService;
        public HomeController(IUserService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            if (ModelState.IsValid)
            {
                user.userpassword = UrlShortner.GetMd5(user.userpassword);
                await _customerService.CreateAsync(user).ConfigureAwait(false);
                UrlShortner.SendMail(user.useremail);
                return RedirectToAction("Home", "Login");
            }
            else
            {
                return View(user);
            }

        }
        public IActionResult Login()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
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
