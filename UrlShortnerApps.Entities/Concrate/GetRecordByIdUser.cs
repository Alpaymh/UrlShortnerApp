using System.Collections.Generic;

namespace UrlShortnerApps.Entities.Concrate
{
    public class GetRecordByIdUser
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public Users data { get; set; }
    }


    public class GetRecordByNameResponseUser
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public List<Users> data { get; set; }
    }

    public class GetRecordByNameResponseUserEmail
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public Users data { get; set; }
    }
}
