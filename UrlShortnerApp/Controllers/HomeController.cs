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
using UrlShortnerApp.Models;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.DataAccess.Concrate;
using UrlShortnerApps.Entities.Concrate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UrlShortnerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _crudOperationDl;
        private readonly IUriRepository _crudOperationDlShort;

        public HomeController(IUserRepository crudOperationDl, IUriRepository crudOperationDlShort)
        {
            _crudOperationDl = crudOperationDl;
            _crudOperationDlShort = crudOperationDlShort;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListUris()
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            try
            {
                response = await _crudOperationDlShort.GetRecordByUserId(UrlShortner.UserMail);
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }


            return View(response.data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Users user)
        {
            GetRecordByIdUser response = new GetRecordByIdUser();
            response = await _crudOperationDl.GetRecordByName(user.useremail, UrlShortner.GetMd5(user.userpassword));
            if (response.data != null)
            {
                UrlShortner.CurrentUserName = response.data.username;
                UrlShortner.UserPassword = UrlShortner.GetMd5(user.userpassword);
                UpdateRecordByIdResponse response2 = new UpdateRecordByIdResponse();

                user = response.data;
                UrlShortner.UserMail = response.data.useremail;
                UrlShortner.CurrentUserId = response.data.id;
                response2 = await _crudOperationDl.UpdateRecordById(user);

            }
            else
            {
                ViewBag.myError = "Hatali bir giriş yaptiniz !";
                return View("Index");
            }
            if (response.data.isadmin == true)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("Create");
            }
        }
        // GET: URLShort  

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        bitly bitlyApi = new bitly();
        [HttpPost]
        public async Task<ActionResult> Create(UriDetails Urls)
        {
            Urls.originalurl = Urls.originalurl;
            bitlyApi.ACCESS_TOKEN = "c4e4f370e43adc0890dda78d2ec986483186eeb5";
            Urls.shortnerurl = await bitlyApi.ShortenAsync(Urls.originalurl);
            InsertRecordResponse response = new InsertRecordResponse();
            try
            {
                GetRecordByIdResponse uriResponse = new GetRecordByIdResponse();
                Urls.ticketcount = UrlShortner.UserMail;
                Urls.tiklamasayi = 0;
                response = await _crudOperationDlShort.InsertRecord(Urls);
                UrlShortner.SendMail2(UrlShortner.UserMail, Urls.shortnerurl);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return View(Urls);
        }
        public ActionResult Show()
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
            InsertRecordResponse response = new InsertRecordResponse();
            try
            {
                user.userpassword = UrlShortner.GetMd5(user.userpassword);
                UrlShortner.SendMail(user.useremail);
                response = await _crudOperationDl.InsertRecord(user);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CreateUrl()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        public IActionResult DefaultPage()
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
