using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Abstract
{
    public interface IUriRepository
    {
         Task<InsertRecordResponse> InsertRecord(UriDetails request);
         Task<GetAllRecordResponse> GetAllRecord();
         Task<GetRecordByIdResponse> GetRecordById(string id);
         Task<GetRecordByNameResponse> GetRecordByName(string name);
        Task<GetRecordByNameResponse> GetRecordByUserId(string name);
        Task<UpdateRecordByIdResponse> UpdateRecordById(UriDetails request);
         Task<UpdateSalaryByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request);
         Task<DeleteRecordByIdResponse> DeletedRecordById(DeleteRecordByIdRequest request);
         Task<DeleteAllRecordResponse> DeleteAllRecord();
    }
}
