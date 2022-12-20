using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<InsertRecordResponse> InsertRecord(Users request);
        Task<GetAllRecordUser> GetAllRecord();
        Task<GetRecordByIdUser> GetRecordById(string id);
        Task<GetRecordByIdUser> GetRecordByName(string name,string email);
        Task<UpdateRecordByIdResponse> UpdateRecordById(Users request);
        Task<UpdateSalaryByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request);
        Task<DeleteRecordByIdResponse> DeletedRecordById(DeleteRecordByIdRequest request);
        Task<DeleteAllRecordResponse> DeleteAllRecord();
    }
}
