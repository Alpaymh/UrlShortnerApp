using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrlShortnerApps.DataAccess.Abstract;
using UrlShortnerApps.Entities.Concrate;
using UrlShortnerApp.Models;

namespace UrlShortnerApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {

        private readonly IUserRepository _crudOperationDl;

        public UserApiController(IUserRepository crudOperationDl)
        {
            _crudOperationDl = crudOperationDl;
        }
        [HttpPost]
        public async Task<IActionResult> InserRecord(Users request)
        {
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
            GetAllRecordUser response = new GetAllRecordUser();
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
            GetRecordByIdUser response = new GetRecordByIdUser();
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
        public async Task<IActionResult> GetRecordByName([FromQuery] string name,string password)
        {
            GetRecordByIdUser response = new GetRecordByIdUser();
            try
            {
                response = await _crudOperationDl.GetRecordByName(name,UrlShortner.GetMd5(password));
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
        public async Task<IActionResult> UpdateRecordById(Users request)
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
