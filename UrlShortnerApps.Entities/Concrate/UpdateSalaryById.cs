using System.ComponentModel.DataAnnotations;

namespace UrlShortnerApps.Entities.Concrate
{
    public class UpdateSalaryByIdRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Salary { get; set; } 
    }

    public class UpdateSalaryByIdResponse
    {
        public bool IsSuccess { get; set; } 
        public string Message { get; set; }
    }
}
