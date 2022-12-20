using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortnerApps.Entities.Concrate;

namespace UrlShortnerApps.DataAccess.Abstract
{
    public interface IShortUrlService
    {

        Task<InsertRecordResponse> Save(UriDetails request);
        Task<GetAllRecordResponse> GetAllRecord();
        Task<GetRecordByIdResponse> GetById(string id);
        Task<GetRecordByIdResponse> GetByPath(string name);
    }
}
