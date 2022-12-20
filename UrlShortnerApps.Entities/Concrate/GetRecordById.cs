using System.Collections.Generic;

namespace UrlShortnerApps.Entities.Concrate
{
    public class GetRecordByIdResponse
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public UriDetails data { get; set; }
    }


    public class GetRecordByNameResponse
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public List<UriDetails> data { get; set; }
    }

    public class GetRecordByNameResponseUrl
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public UriDetails data { get; set; }
    }
}
