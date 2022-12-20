using System.Collections.Generic;

namespace UrlShortnerApps.Entities.Concrate

{
    public class GetAllRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<UriDetails> data { get; set; }
    }
}
