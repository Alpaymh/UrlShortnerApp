using System.ComponentModel.DataAnnotations;

namespace UrlShortnerApps.Entities.Concrate

{
    public class DeleteRecordByIdRequest
    {
        [Required]
        public string Id { get; set; }
    }

    public class DeleteRecordByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
