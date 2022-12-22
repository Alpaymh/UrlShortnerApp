using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UrlShortnerApp.Models;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _crudOperationDl;
        private readonly IUriRepository _crudOperationDlShort;

        public AdminController(IUserRepository crudOperationDl, IUriRepository crudOperationDlShort)
        {
            _crudOperationDl = crudOperationDl;
            _crudOperationDlShort = crudOperationDlShort;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GetAllRecordUser response = new GetAllRecordUser();
            response = await _crudOperationDl.GetAllRecord();

            return View(response.data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userDet)
        {
            DeleteRecordByIdRequest req = new DeleteRecordByIdRequest();
            req.Id = userDet;
            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();

            response = await _crudOperationDl.DeletedRecordById(req);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ListUri()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();

            response = await _crudOperationDlShort.GetAllRecord();

            return View(response.data);

        }

        [HttpPost]
        public async Task<IActionResult> ListUri(string shortDet)
        {
            GetRecordByIdResponse response2 = new GetRecordByIdResponse();
            response2 = await _crudOperationDlShort.GetRecordById(shortDet);

            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();
            DeleteRecordByIdRequest myTemp = new DeleteRecordByIdRequest();
            myTemp.Id = shortDet;
            response = await _crudOperationDlShort.DeletedRecordById(myTemp);
            UrlShortner.SendMail3(response2.data.ticketcount);
            return RedirectToAction("ListUri", "Admin");

        }
        [HttpPost]
        public async Task<IActionResult> UriGet(string urlStr)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();

            response = await _crudOperationDlShort.GetRecordById(urlStr);
            response.data.tiklamasayi += 1;
            UpdateRecordByIdResponse response2 = new UpdateRecordByIdResponse();
            response2 = await _crudOperationDlShort.UpdateRecordById(response.data);

            return Redirect(urlStr);
        }

    }
}
