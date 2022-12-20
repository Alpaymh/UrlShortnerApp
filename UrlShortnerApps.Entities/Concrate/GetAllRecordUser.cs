using System.Collections.Generic;

namespace UrlShortnerApps.Entities.Concrate

{
    public class GetAllRecordUser
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<Users> data { get; set; }
    }
}
