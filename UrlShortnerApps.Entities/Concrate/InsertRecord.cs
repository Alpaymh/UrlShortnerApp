using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace UrlShortnerApps.Entities.Concrate
{

    //Request Api
    //public class InsertRecordRequest
    //{
    //    [BsonId]
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string id { get; set; }
    //    [BsonElement("age")]
    //    public int Age { get; set; }
    //    [BsonElement("contact")]
    //    public string Contact { get; set; }
    //    [BsonElement("createdDate")]
    //    public string CreatedDate { get; set; }
    //    [Required]
    //    [BsonElement("firstName")]
    //    public string FirstName { get; set; }
    //    [BsonElement("lastName")]
    //    public string LastName { get; set; }
    //    [BsonElement("salary")]
    //    public double Salary { get; set; }
    //    [BsonElement("updatedDate")]
    //    public string UpdatedDate { get; set; }
    //}

    //Api'den dönen response
    public class InsertRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
