
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;
using UrlShortnerApp.Models;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class UrlShortnerApiController : ControllerBase
    {
        private readonly IUriRepository _crudOperationDl;

        public UrlShortnerApiController(IUriRepository crudOperationDl)
        {
            _crudOperationDl = crudOperationDl;
        }

        [HttpPost]
        public async Task<IActionResult> InserRecord(UriDetails request)
        {
            request.originalurl = request.originalurl;
            var bitly = new bitly();
            bitly.ACCESS_TOKEN = "c4e4f370e43adc0890dda78d2ec986483186eeb5";
            request.shortnerurl = await bitly.ShortenAsync(request.originalurl);
            InsertRecordResponse response = new InsertRecordResponse();
            try
            {
                response = await _crudOperationDl.InsertRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            try
            {
                response = await _crudOperationDl.GetAllRecord();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetRecordById([FromQuery] string id)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            try
            {
                response = await _crudOperationDl.GetRecordById(id);
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordByUserId([FromQuery] string id)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            try
            {
                response = await _crudOperationDl.GetRecordByUserId(id);
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery] string name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            try
            {
                response = await _crudOperationDl.GetRecordByName(name);
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }


        //Id'ye göre kullanıcı güncelleme
        [HttpPut]
        public async Task<IActionResult> UpdateRecordById(UriDetails request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            try
            {
                response = await _crudOperationDl.UpdateRecordById(request);
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {

            UpdateSalaryByIdResponse response = new UpdateSalaryByIdResponse();
            try
            {
                response = await _crudOperationDl.UpdateSalaryById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletedRecordById(DeleteRecordByIdRequest request)
        {

            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();
            try
            {
                response = await _crudOperationDl.DeletedRecordById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAllRecord()
        {

            DeleteAllRecordResponse response = new DeleteAllRecordResponse();
            try
            {
                response = await _crudOperationDl.DeleteAllRecord();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs = " + ex.Message;
            }

            return Ok(response);
        }

    }
}
