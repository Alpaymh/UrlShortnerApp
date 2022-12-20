using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortnerApps.Entities.Abstract;

namespace UrlShortnerApps.Entities.Concrate
{
    public class UriDetails : IEntities
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("originalurl")]
        public string originalurl { get; set; }

        [BsonElement("shortnerurl")]
        public string shortnerurl { get; set; }

        [BsonElement("createdate")]
        public string createdate { get; set; }
        [BsonElement("updateddate")]
        public string updateddate { get; set; }

        [BsonElement("ticketcount")]
        public string ticketcount { get; set; }

        [BsonElement("randomusernumber")]
        public string randomusernumber { get; set; }

        [BsonElement("tiklamasayi")]
        public int tiklamasayi { get; set; }


    }
}
